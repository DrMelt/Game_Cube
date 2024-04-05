using Godot;
using System;


namespace GameKernel
{
	public partial class LevelEndUI : Control
	{

		[Export]
		bool visibleInit = false;

		[Export]
		Button focusInitButton;

		public override void _Ready()
		{
			Visible = false;

			EndPoint.EnteredEndPointEvent += () =>
			{
				Visible = true;
			};

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
					focusInitButton.GrabFocus();
				}
			};
		}

		public override void _Process(double delta)
		{
		}

	}
}
