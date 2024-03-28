using GameKernel;
using Godot;
using System;

namespace GameKernel
{

	public partial class ColorDoor : CubeBase
	{
		[Export]
		CubeColor cubeColor;

		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
		}


		public override void EntryCube(Player player)
		{

		}

        public override void ExitCube(Player player)
        {
			cubeColor = CubeColor.BLACK;
        }

        public override bool CanEntry(Player player)
		{
			return cubeColor == player.Color;
		}
	}
}
