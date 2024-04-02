using Godot;
using System;

namespace GameKernel
{
	public partial class ButtonBackMenu : Button
	{
		public delegate void BackMenu();
		public static event BackMenu BackMenuEvenet;

		
		public override void _Ready()
		{

		}

		public override void _Process(double delta)
		{

		}

        public override void _Pressed()
        {
			BackMenuEvenet.Invoke();
        }
    }
}

