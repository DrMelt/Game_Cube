using GameKernel;
using Godot;
using System;

namespace GameKernel
{
	public partial class SettingPanel : Control
	{
		[Export]
		bool visibleInit = false;

		public override void _Ready()
		{
			Visible = visibleInit;

			ButtonBackMenu.BackMenuEvenet += () =>
			{
				Visible = false;
			};

		}

		public override void _Process(double delta)
		{
			if (LevelsManager.CurrentLevel != LevelName.MAIN_MENU)
			{
				if (Input.IsActionJustPressed("ui_esc"))
				{
					Visible = !Visible;
				}
			}
			else
			{
				Visible = false;
			}
		}
	}
}
