using Godot;
using System;

namespace GameKernel
{
	public partial class RetryButton : Button
	{
		public delegate void RetryButtonPressed();
		public static event RetryButtonPressed RetryButtonPressedEvenet;

		public override void _Ready()
		{

		}

		public override void _Process(double delta)
		{
			if (LevelsManager.CurrentLevel == LevelName.MAIN_MENU)
			{
				Visible = false;
			}
			else
			{
				Visible = true;
				
				if (Input.IsActionPressed("ui_restart"))
				{
					RetryButtonPressedEvenet.Invoke();
				}
			}

		}

		public override void _Pressed()
		{
			RetryButtonPressedEvenet.Invoke();
		}

	}
}

