// description: Player script

using Godot;
using System;
using System.Text;
using System.Text.Json;

namespace GameKernel
{
	public partial class Player : Node3D
	{
		public static string nodePath = null;


		[ExportGroup("Reference Data")]
		[Export]
		Camera3D camera;
		[Export]
		Node3D playerInstance;


		[ExportGroup("Player Data")]
		[ExportSubgroup("System Data")]
		[Export]
		string playerName = "";

		[Export]
		float moveSpeed = 10.0f;

		[Export]
		float cameraXMin = -(float)Math.PI * 60 / 180f;
		[Export]
		float cameraXMax = (float)Math.PI * 80 / 180f;

		[ExportSubgroup("Level Data")]
		[Export]
		CubeColor color = CubeColor.WHITE;
        public CubeColor Color { get => color; set => color = value; }




		Vector2 mouseMoved = new Vector2();

		GlobalConfigurations globalConfigurations;


		bool isTransforming = false;


        public override void _Ready()
		{
			nodePath = GetPath();
			globalConfigurations = GetNode<GlobalConfigurations>(GlobalConfigurations.NodePath);
		}

		public override void _EnterTree()
		{
		}

		public override void _Process(double delta)
		{
			if (!globalConfigurations.IsGamePause)
			{
				MovePlayer(delta);
				RotateView(delta);

			}
			else
			{
				mouseMoved = Vector2.Zero;
			}
		}

		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventMouseMotion eventMouse)
			{
				mouseMoved += eventMouse.Relative;
			}
		}

		void MovePlayer(double delta)
		{
			// 获取输入
			float inputX = Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right");
			float inputZ = Input.GetActionStrength("ui_up") - Input.GetActionStrength("ui_down");

			Vector3 dirForwardVec = GetClosestAxisOfCamera();

			Quaternion quaternionToDir = Quaternion.FromEuler(new Vector3(0, (float)Math.PI * 0.5f, 0));
			Vector3 dirLeftVec = quaternionToDir * dirForwardVec;

			Vector3 viewVec = inputX * dirLeftVec + inputZ * dirForwardVec;
			if (viewVec.LengthSquared() > 0.001f)
			{
				viewVec = viewVec.Normalized();
			}
			else
			{
				viewVec = Vector3.Zero;
			}

			playerInstance.Translate(viewVec * moveSpeed * (float)delta);
		}

		void RotateView(double delta)
		{
			//可选：如果你需要垂直旋转（比如上下看），可以添加类似处理
			// 注意，通常会限制垂直旋转范围以防止过度翻转
			float rotationOfX = -mouseMoved.Y * globalConfigurations.Sensitivity * (float)delta;
			float rotationOfY = -mouseMoved.X * globalConfigurations.Sensitivity * (float)delta;

			RotateCamera(new Vector2(rotationOfX, rotationOfY));

			// 恢复初始鼠标位置以便下一次计算
			mouseMoved = Vector2.Zero;
		}

		void RotateCamera(Vector2 rotateRadianVec)
		{
			float newXRadian = camera.Rotation.X + rotateRadianVec.X;
			float clampedX = Math.Clamp(newXRadian, cameraXMin, cameraXMax);

			float newYRadian = camera.Rotation.Y + rotateRadianVec.Y;

			camera.Rotation = new Vector3(clampedX, newYRadian, camera.Rotation.Z);
		}

		Vector3 GetClosestAxisOfCamera()
		{
			Vector3 cameraLookAtDir = camera.Transform.Basis * Vector3.Forward;

			int maxInd = Math.Abs(cameraLookAtDir.X) > Math.Abs(cameraLookAtDir.Y) ? 0 : 2;

			int signOfMax = Math.Sign(cameraLookAtDir[maxInd]);

			Vector3 result = Vector3.Zero;
			result[maxInd] = signOfMax;

			return result;
		}

	}
}
