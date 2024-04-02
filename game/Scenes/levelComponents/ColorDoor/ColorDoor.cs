using Godot;
using System;

namespace GameKernel
{

	public partial class ColorDoor : CubeBase
	{
		[Export]
		bool isOutDoor = false;

		[Export]
		LevelName nextLevel = LevelName.NULL;

		[Export]
		CubeColor doorColor = CubeColor.WHITE;

		CubeColor originColor;

		[Export]
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
			if (isOutDoor)
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

			CubeColor doorColorNOT = ~doorColor;

			bool hasNotColor = (doorColorNOT & player.Color) > 0;
			return !hasNotColor;
		}

		public override void ReSet()
		{
			base.ReSet();

			if (LevelsManager.CurrentLevel == nextLevel && isOutDoor)
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
