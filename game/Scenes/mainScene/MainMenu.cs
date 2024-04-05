using GameKernel;
using Godot;
using System;

namespace GameKernel
{
	public partial class MainMenu : Control
	{
		[Export]
		Button startButtonRef;

		public override void _Ready()
		{
			startButtonRef.GrabFocus();

			ButtonLevelSelect.EntryLevelEvenet += (levelName) =>
				{
					Visible = false;
				};

			ButtonBackMenu.BackMenuEvenet += () =>
					{
						Visible = true;
					};

			VisibilityChanged += () =>
			{
				if (Visible)
				{
					startButtonRef.GrabFocus();
				}
			};
		}



		public override void _Process(double delta)
		{
		}

	}
}
