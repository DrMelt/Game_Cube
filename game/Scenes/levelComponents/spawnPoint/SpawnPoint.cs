using Godot;
using System;

namespace GameKernel
{
    public partial class SpawnPoint : CubeBase
    {
        [Export]
        CubeColor playerStartColor = CubeColor.WHITE;

        public CubeColor PlayerStartColor { get => playerStartColor; set => playerStartColor = value; }


        public override void _Ready()
        {
            base._Ready();

            Visible = false;
        }

    }
}
