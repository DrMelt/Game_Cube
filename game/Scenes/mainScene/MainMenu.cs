using GameKernel;
using Godot;
using System;

public partial class MainMenu : Control
{

	public override void _Ready()
	{
		ButtonLevelSelect.EntryLevelEvenet += (levelName) =>
		{
			Visible = false;
		};

		ButtonBackMenu.BackMenuEvenet += () =>
		{
			Visible = true;
		};
	}


	public override void _Process(double delta)
	{
	}
}
