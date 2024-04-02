using Godot;
using System;

namespace GameKernel
{
	public partial class GlobalConfigurations : Node
	{
		public static string NodePath = "";

		[ExportGroup("Player Settings")]
		[Export]
		float sensitivity = 0.1f;
		[Export]
		float moveSpeed = 0.5f;


		[ExportGroup("System Status")]
		[Export]
		bool isAnyUIOn = false;

		public bool IsAnyUIOn { get => isAnyUIOn; set => isAnyUIOn = value; }



		public float Sensitivity { get => sensitivity; set => sensitivity = value; }
		public float MoveTime { get => moveSpeed; set => moveSpeed = value; }


		public bool IsGamePause { get => IsAnyUIOn; set => IsAnyUIOn = value; }

		public override void _Ready()
		{
			NodePath = GetPath();
		}

		public override void _Process(double delta)
		{

		}
	}
}

