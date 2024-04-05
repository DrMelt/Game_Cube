using Godot;
using System;

namespace GameKernel
{
	public partial class LevelSelect : Control
	{
		[Export]
		bool visibleInit = false;

		[Export]
		Button buttonBackMenu;

		public override void _Ready()
		{
			Visible = visibleInit;


			ButtonLevelSelect.EntryLevelEvenet += (levelName) =>
			{
				Visible = false;
			};

			ButtonBackMenu.BackMenuEvenet += () =>
			{
				Visible = false;
			};

			VisibilityChanged += () =>
			{
				if (Visible)
				{
					buttonBackMenu.GrabFocus();
				}
			};
		}

		public override void _Process(double delta)
		{
		}
	}
}
