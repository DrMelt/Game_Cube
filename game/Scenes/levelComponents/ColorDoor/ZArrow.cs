using Godot;
using System;

namespace GameKernel
{
	public partial class ZArrow : MeshInstance3D
	{
		public override void _Ready()
		{
			Visible = false;
		}

		public override void _Process(double delta)
		{

		}
	}
}

