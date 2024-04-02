using Godot;
using System;

namespace GameKernel
{
	public partial class LevelSelect : Control
	{
		[Export]
		bool visibleInit = false;
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
		}

		public override void _Process(double delta)
		{
		}
	}
}
