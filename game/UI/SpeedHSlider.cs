using Godot;
using System;

namespace GameKernel
{
	public partial class SpeedHSlider : HSlider
	{
		[Export]
		Label labelRef;

		public override void _Ready()
		{
			GlobalConfigurations globalConfigurations = GetNode<GlobalConfigurations>(GlobalConfigurations.NodePath);

			Value = globalConfigurations.MoveTime;
			labelRef.Text = Value.ToString("N3");


			ValueChanged += (value) =>
			{
				globalConfigurations.MoveTime = (float)value;
				labelRef.Text = value.ToString("N3");
			};
		}

		public override void _Process(double delta)
		{

		}
	}
}

