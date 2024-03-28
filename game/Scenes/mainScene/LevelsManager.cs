using Godot;
using System;
using System.Collections.Generic;

namespace GameKernel
{
	public enum LevelName
	{
		MAIN_MENU,
		LEVEL00_01,
		LEVEL00_02,
		LEVEL00_03,
		MAX_LEVEL_NUM
	}


	public partial class LevelContent
	{
		Vector3I startPos;
		Vector3I size;



		Vector3I GetWordPos(Vector3I index)
		{
			return index + startPos;
		}


	}


	public partial class LevelsManager : Node
	{
		public static LevelName currentLevel = LevelName.MAIN_MENU;
		public static string nodePath = null;

		static List<LevelContent> levels = new List<LevelContent>((int)LevelName.MAX_LEVEL_NUM);



		public override void _Ready()
		{
			nodePath = GetPath();
		}

		public override void _Process(double delta)
		{

		}
	}
}

