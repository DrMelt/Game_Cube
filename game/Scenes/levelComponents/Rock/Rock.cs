using Godot;
using System;

namespace GameKernel
{
	public partial class Rock : CubeBase
	{
		public override bool CanEntry(Player player)
		{
			if (player.TryVec.Y < -0.5f)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public override void EnterCube(Player player)
		{
			Visible = false;
		}
	}
}
