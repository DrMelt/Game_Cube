using Godot;
using Godot.Collections;
using System;

namespace GameKernel
{

	public partial class ColorDoor : CubeBase
	{
		[Export]
		bool isOutDoor = false;
		[Export]
		Dir outDir = Dir.FORWORD;

		[Export]
		Array<LevelName> lockWhen = new Array<LevelName>();


		[Export]
		LevelName nextLevel = LevelName.NULL;

		[Export]
		CubeColor doorColor = CubeColor.WHITE;

		CubeColor originColor;

		MeshInstance3D cubeRef;

		ShaderMaterial material;

		void SetColor(CubeColor color)
		{
			material.SetShaderParameter("emitionColor", Colors.GetColorVec4(color));
			material.SetShaderParameter("bgColor", Colors.GetColorVec4(color));
		}

		void SetColorBlack()
		{
			doorColor = CubeColor.BLACK;
			SetColor(doorColor);
		}


		public override void _Ready()
		{
			originColor = doorColor;
			cubeRef = GetNode<MeshInstance3D>("./Cube");
			material = cubeRef.MaterialOverride as ShaderMaterial;
			SetColor(doorColor);

			Random random = new Random();
			material.SetShaderParameter("noiseOffset", new Vector3(0, random.Next(1000), 0));
		}


		public override void _Process(double delta)
		{
		}


		public override void EnterCube(Player player)
		{

		}

		public override void ExitCube(Player player)
		{
			Vector3 outDirVec = GlobalBasis * GetDirVec(outDir);

			if (isOutDoor && outDirVec.Dot(player.TryVec) > 0.5f)
			{
				LevelsManager.CurrentLevel = nextLevel;
			}
		}

		public override void ExitedCube(Player player)
		{
			SetColorBlack();
		}

		public override bool CanEntry(Player player)
		{
			if (!isActive)
			{
				return true;
			}

			CubeColor doorColorNOT = (CubeColor)(CubeColor.WHITE - doorColor);

			var temp = doorColorNOT & player.Color;

			bool hasNotColor = (doorColorNOT & player.Color) > 0;
			return !hasNotColor;
		}

		public override void ReSet()
		{
			base.ReSet();

			if (lockWhen.IndexOf(LevelsManager.CurrentLevel) > -1)
			{
				SetColorBlack();
			}
			else
			{
				doorColor = originColor;
				SetColor(doorColor);
			}

		}
	}
}
