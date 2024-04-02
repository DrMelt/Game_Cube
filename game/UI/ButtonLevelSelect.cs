using Godot;
using System;

namespace GameKernel
{

	public partial class ButtonLevelSelect : Button
	{
		public delegate void EntryLevel(LevelName levelName);
		public static event EntryLevel EntryLevelEvenet;


		[Export]
		LevelName levelName;

		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
		}


		public override void _Pressed()
		{
			EntryLevelEvenet.Invoke(levelName);
		}

	}
}
