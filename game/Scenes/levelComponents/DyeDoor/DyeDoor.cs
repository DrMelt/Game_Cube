using Godot;
using System;

namespace GameKernel
{

	public partial class DyeDoor : CubeBase
	{
		[Export]
		CubeColor cubeColor;

		[Export]
		MeshInstance3D cubeRef;

		ShaderMaterial material;


		public override void _Ready()
		{
			material = cubeRef.MaterialOverride as ShaderMaterial;

			material.SetShaderParameter("Color", Colors.GetColorVec4(cubeColor));
		}

		public override void _Process(double delta)
		{
		}

		public override void EnterCube(Player player)
		{
			player.Color = cubeColor;
		}


		public override void ExitedCube(Player player)
		{
			Visible = false;
			isActive = false;
		}

	}
}
