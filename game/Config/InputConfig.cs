using Godot;
using System;

namespace GameKernel
{
	public partial class InputConfig : Node
	{
		GlobalConfigurations globalConfigurations;

		[Export]
		float mouseHideTime = 5.0f;
		float mouseMovedTime = 0.0f;
		Vector2 preMousePosition = Vector2.Zero;


		public override void _Ready()
		{
			globalConfigurations = GetNode<GlobalConfigurations>(GlobalConfigurations.NodePath);
		}

		public override void _Process(double delta)
		{
			if (globalConfigurations.IsGamePause)
			{
                Vector2 newPos = GetViewport().GetMousePosition();

				if (newPos != preMousePosition)
				{
					mouseMovedTime = 0.0f;
					preMousePosition = newPos;
				}
				else
				{
					mouseMovedTime += (float)delta;
				}


				if (mouseMovedTime > mouseHideTime)
				{
					Input.MouseMode = Input.MouseModeEnum.Hidden;
				}
				else
				{
					Input.MouseMode = Input.MouseModeEnum.Visible;
				}
			}
			else
			{
				Input.MouseMode = Input.MouseModeEnum.Captured;
			}

		}
	}
}

