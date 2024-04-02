using Godot;
using System;


namespace GameKernel
{

	public enum CubeType
	{
		AIR,
		BARRIER,
		DOOR,
		SPAWN_POINT,
		DYE_DOOR,
	}


	public enum CubeColor
	{
		BLACK = 0x0,
		RED = 0x1,
		GREEN = 0x2,
		BLUE = 0x4,
		WHITE = 0x8,
	}

	public static class Colors
	{
		const float BASE_VALUE = 0.2f;
		public readonly static Vector4 BLACK = new Vector4(0, 0, 0, 1);

		public readonly static Vector4 RED = new Vector4(1, BASE_VALUE, BASE_VALUE, 1);
		public readonly static Vector4 GREEN = new Vector4(BASE_VALUE, 1, BASE_VALUE, 1);
		public readonly static Vector4 BLUE = new Vector4(BASE_VALUE, BASE_VALUE, 1, 1);
		public readonly static Vector4 WHITE = new Vector4(1, 1, 1, 1);

		public static Vector4 GetColorVec4(CubeColor cubeColor)
		{
			switch (cubeColor)
			{
				case CubeColor.BLACK:
					return BLACK;
				case CubeColor.RED:
					return RED;
				case CubeColor.GREEN:
					return GREEN;
				case CubeColor.WHITE:
					return WHITE;
			}

			return Vector4.Zero;
		}
	}

	public partial class CubeBase : Node3D
	{

		[Export]
		CubeType cubeType = CubeType.AIR;
		[Export]
		LevelName inLevel = LevelName.NULL;
		public LevelName InLevel { get => inLevel; set => inLevel = value; }
		[Export]
		protected bool isActive = true;


		public virtual void EnterCube(Player player)
		{

		}

		public virtual void EnteredCube(Player player)
		{

		}

		public virtual void ExitCube(Player player)
		{

		}

		public virtual void ExitedCube(Player player)
		{

		}

		public static Vector3I Vec3ConvertToVec3I(Vector3 vec3)
		{
			return (Vector3I)vec3.Round();
		}

		public virtual bool CanEntry(Player player)
		{
			return true;
		}

		public virtual void ReSet()
		{
			isActive = true;
		}
	}

}
