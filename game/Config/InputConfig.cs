using Godot;
using System;

namespace GameKernel
{
	public partial class InputConfig : Node
	{
		GlobalConfigurations globalConfigurations;

		public override void _Ready()
		{
			globalConfigurations = GetNode<GlobalConfigurations>(GlobalConfigurations.NodePath);
		}

		public override void _Process(double delta)
		{
			if (!globalConfigurations.IsGamePause)
			{ Input.MouseMode = Input.MouseModeEnum.Captured; }
			else
			{ Input.MouseMode = Input.MouseModeEnum.Visible; }
		}
	}
}

