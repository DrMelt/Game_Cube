using Godot;
using System;

public partial class SettingPanel : Control
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_esc"))
		{
			Visible = !Visible;
		}
	}
}
