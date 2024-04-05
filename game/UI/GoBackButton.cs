using Godot;
using System;



namespace GameKernel
{
	public partial class GoBackButton : Button
	{
		[Export]
		Control levelSelectRef;
		[Export]
		Control mainMenuRef;
		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
		}

		public override void _Pressed()
		{
			levelSelectRef.Visible = false;
			mainMenuRef.Visible = true;
		}
	}
}