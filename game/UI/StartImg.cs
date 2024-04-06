using Godot;
using System;

namespace GameKernel
{
	public partial class StartImg : Control
	{
		[Export]
		Control mainControlRef;
		[Export]
		float time = 0.5f;

		float timer = 0.0f;

		public override void _Ready()
		{
			Visible = true;
		}

		public override void _Process(double delta)
		{
			if (!Visible)
			{
				return;
			
			}
			timer += (float)delta;
			if (timer >= time)
			{
				Visible = false;
				mainControlRef.Visible = true;
			}
		}
	}
}

