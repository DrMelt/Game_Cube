using Godot;
using System;
using System.Collections.Generic;

namespace GameKernel
{
	public enum LevelName
	{
		NULL,
		MAIN_MENU,
		LEVEL00_01,
		LEVEL00_02,
		LEVEL00_03,
		LEVEL00_04,
		LEVEL00_05,
		LEVEL00_06,
		LEVEL01_01,
		LEVEL01_02,
		LEVEL01_03,
		LEVEL01_END,
		MAX_LEVEL_NUM
	}


	public partial class LevelContent
	{
		LevelName levelName = LevelName.NULL;
		public LevelName GetLevelName { get => levelName; }

		Vector3I startPos = Vector3I.Zero;
		Vector3I size = Vector3I.Zero;

		List<CubeBase> cubes = null;


		SpawnPoint spawnPoint = null;
		public SpawnPoint SpawnPointInstance { get => spawnPoint; set => spawnPoint = value; }


		public Transform3D SpawnTrans { get => spawnPoint.GlobalTransform; }

		public CubeColor StartColor { get => spawnPoint.PlayerStartColor; }

		public LevelContent(Vector3I startPos, Vector3I size, LevelName levelName)
		{
			this.levelName = levelName;
			this.startPos = startPos;
			this.size = size;
			cubes = new List<CubeBase>(size.X * size.Y * size.Z);
			for (int i = 0; i < size.X * size.Y * size.Z; i++)
			{
				cubes.Add(null);
			}
		}

		Vector3I GetWordPos(Vector3I index)
		{
			return index + startPos;
		}


		bool IsOutOfRange(Vector3I cubeWordPos)
		{
			if ((
				cubeWordPos.X < startPos.X || cubeWordPos.Y < startPos.Y || cubeWordPos.Z < startPos.Z
			) || (
				cubeWordPos.X > startPos.X + size.X - 1 || cubeWordPos.Y > startPos.Y + size.Y - 1 || cubeWordPos.Z > startPos.Z + size.Z - 1
			))
			{
				// GD.PrintErr($"Out of LevelContent {levelName} range");
				return true;
			}
			return false;
		}

		int CubeIndex(Vector3I ind)
		{
			return ind.X + ind.Y * size.X + ind.Z * size.X * size.Y;
		}


		public CubeBase GetCube(Vector3I cubeWordPos)
		{
			if (IsOutOfRange(cubeWordPos))
			{ return null; }
			else
			{ return cubes[CubeIndex(cubeWordPos - startPos)]; }
		}

		public void SetCube(CubeBase cube)
		{
			Vector3I cubeWordPos = CubeBase.Vec3ConvertToVec3I(cube.GlobalPosition);
			if (IsOutOfRange(cubeWordPos))
			{ return; }
			cubes[CubeIndex(cubeWordPos - startPos)] = cube;
		}


		public void ReSet()
		{
			foreach (CubeBase cube in cubes)
			{
				if (cube != null)
				{
					cube.ReSet();
				}
			}
		}

	}


	public partial class LevelsManager : Node
	{
		static LevelName currentLevel = LevelName.MAIN_MENU;

		public static LevelName CurrentLevel
		{
			get => currentLevel;
			set
			{
				GD.Print("set CurrentLevel: " + value);
				currentLevel = value;
			}
		}

		public static string nodePath = null;

		static List<LevelContent> levels;

		public override void _Ready()
		{
			nodePath = GetPath();
			levels = new List<LevelContent>((int)LevelName.MAX_LEVEL_NUM);
			for (var i = 0; i < (int)LevelName.MAX_LEVEL_NUM; i++)
			{
				levels.Add(null);
			}

			ButtonLevelSelect.EntryLevelEvenet += EntryLevel;

			RetryButton.RetryButtonPressedEvenet += () => { EntryLevel(currentLevel); };
		}

		void EntryLevel(LevelName levelName)
		{
			currentLevel = levelName;
			ReSetAllLevels();
		}


		void ReSetLevel(LevelName levelName)
		{
			LevelContent levelContent = levels[(int)levelName];
			if (levelContent == null)
			{
				GD.PrintErr($"LevelContent {currentLevel} is null");
			}
			else
			{
				levelContent.ReSet();
			}
		}
		void ReSetLevel()
		{
			ReSetLevel(currentLevel);
		}

		void ReSetAllLevels()
		{
			foreach (var level in levels)
			{
				if (level == null)
				{
					continue;
				}

				level.ReSet();
			}
		}


		public static void SetLevel(LevelContent levelContent)
		{
			levels[(int)levelContent.GetLevelName] = levelContent;
		}

		public static LevelContent GetLevel(LevelName levelName)
		{
			return levels[(int)levelName];
		}

		static LevelContent GetCurrentLevel()
		{
			LevelContent level = levels[(int)currentLevel];
			if (level == null)
			{
				GD.PrintErr("CurrentLevel is null");
			}
			return level;
		}

		public static bool CheckFallDown(Player player, Vector3I cubeWordPos)
		{
			return CheckCanEntry(player, cubeWordPos + Vector3I.Down);
		}

		public static bool CheckJump(Player player, Vector3I cubeWordPos)
		{
			return CheckCanEntry(player, cubeWordPos + Vector3I.Up);
		}



		public static bool CheckCanEntry(Player player, Vector3I cubeWordPos)
		{
			LevelContent level = GetCurrentLevel();
			if (level == null)
			{
				return true;
			}

			CubeBase cube = level.GetCube(cubeWordPos);
			if (cube == null)
			{
				foreach (LevelContent levelContent in levels)
				{
					if (levelContent == null)
					{
						continue;
					}

					cube = levelContent.GetCube(cubeWordPos);
					if (cube == null)
					{
						continue;
					}

					if (!cube.CanEntry(player))
					{
						return false;
					}
				}

				return true;
			}
			else
			{
				if (cube.CanEntry(player))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public static void SignalEnter(Player player, Vector3I cubeWordPos)
		{
			SingalCube(cubeWordPos, (CubeBase cube) => cube.EnterCube(player));
		}

		public static void SignalEntered(Player player, Vector3I cubeWordPos)
		{
			SingalCube(cubeWordPos, (CubeBase cube) => cube.EnteredCube(player));
		}

		public static void SignalExit(Player player, Vector3I cubeWordPos)
		{
			SingalCube(cubeWordPos, (CubeBase cube) => cube.ExitCube(player));
		}


		public static void SignalExited(Player player, Vector3I cubeWordPos)
		{
			SingalCube(cubeWordPos, (CubeBase cube) => cube.ExitedCube(player));
		}


		static void SingalCube(Vector3I cubeWordPos, Action<CubeBase> action)
		{
			foreach (LevelContent levelContent in levels)
			{
				if (levelContent != null)
				{
					CubeBase cube = levelContent.GetCube(cubeWordPos);
					if (cube != null)
					{
						action(cube);
					}
				}
			}
		}

	}
}

