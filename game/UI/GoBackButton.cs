using Godot;
using System;



namespace GameKernel
{
	public partial class GoBackButton : Button
	{
		[Export]
		Control levelSelectRef;
		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
		}

		public override void _Pressed()
		{
			levelSelectRef.Visible = false;
		}
	}
}