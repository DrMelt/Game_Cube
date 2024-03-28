using Godot;
using System;


namespace GameKernel
{
	public partial class baseBarrierCube : CubeBase
	{
		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
		}

        public override bool CanEntry(Player player)
        {
			return false;
        }
    }

}