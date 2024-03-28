using Godot;
using System;

namespace GameKernel

{
	public partial class SensitivityHSlider : HSlider
	{

		[Export]
		Label label;

		public override void _Ready()
		{
			GlobalConfigurations globalConfigurations = GetNode<GlobalConfigurations>(GlobalConfigurations.NodePath);

			Value = globalConfigurations.Sensitivity;
			label.Text = Value.ToString("N3");


			ValueChanged += (value) =>
            {
                globalConfigurations.Sensitivity = (float)value;
                label.Text = value.ToString("N3");
			};

		}

		public override void _Process(double delta)
		{

		}
	}
}

