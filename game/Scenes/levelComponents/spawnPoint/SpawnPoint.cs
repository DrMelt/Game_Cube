using Godot;
using System;

namespace GameKernel
{
	public partial class SpawnPoint : CubeBase
	{
        public override void _Ready()
        {
            base._Ready();

			Visible = false;
        }

    }
}
