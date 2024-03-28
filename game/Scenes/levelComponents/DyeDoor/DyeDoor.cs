using GameKernel;
using Godot;
using System;

public partial class DyeDoor : CubeBase
{
	[Export]
	CubeColor cubeColor;


	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void EntryCube(Player player)
	{
		player.Color = cubeColor;
	}


	public override void ExitCube(Player player)
	{
		Visible = false;
	}



}
