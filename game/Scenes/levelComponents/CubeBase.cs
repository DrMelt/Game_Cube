using Godot;
using System;


namespace GameKernel
{

	public enum CubeType
	{
		BARRIER,
		DOOR,
	}


	public enum CubeColor{
		BLACK = 0x0,
		RED = 0x1,
		GREEN = 0x2,
		BLUE = 0x4,
		WHITE = 0x8,
	}

	public partial class CubeBase : Node3D
	{
		[Export]
		CubeType cubeType;
		[Export]
		LevelName inLevel;


		public virtual void EntryCube(Player player)
		{

		}

		public virtual void ExitCube(Player player)
		{

		}

		public virtual bool CanEntry(Player player)
		{
			return false;
		}
	}

}
