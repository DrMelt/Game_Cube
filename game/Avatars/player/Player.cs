// description: Player script

using Godot;
using System;

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
		[Export]
		MeshInstance3D postProcessInstance;
		ShaderMaterial postProcessShader;


		[ExportGroup("Player Data")]
		[ExportSubgroup("System Data")]
		[Export]
		string playerName = "";

		[Export]
		float moveSpeed = 10.0f;

		[Export]
		float cameraXMin = -(float)Math.PI * 80 / 180f;
		[Export]
		float cameraXMax = (float)Math.PI * 80 / 180f;

		[ExportSubgroup("Level Data")]
		[Export]
		CubeColor color = CubeColor.WHITE;
		public CubeColor Color
		{
			get => color;
			set
			{
				color = value;
				SetPostPorcessShaderColor(value);
			}
		}
		public Vector3 TargetPos { get => targetPos; set => targetPos = value; }
		public Vector3 PrePos { get => prePos; set => prePos = value; }

		Vector2 mouseMoved = new Vector2();

		GlobalConfigurations globalConfigurations;

		bool isTransforming = false;
		double transformedTime = 0.0f;

		Vector3 targetPos, prePos, tryVec;
		public Vector3 TryVec { get => tryVec; }

		void Init()
		{
			targetPos = playerInstance.GlobalPosition;
			prePos = playerInstance.GlobalPosition;
			tryVec = Vector3.Zero;
			isTransforming = false;
		}

		void StartLevelInitPlayer(LevelName levelName)
		{
			playerInstance.GlobalPosition = LevelsManager.GetLevel(levelName).SpawnTrans.Origin;
			camera.GlobalBasis = LevelsManager.GetLevel(levelName).SpawnTrans.Basis;

			color = LevelsManager.GetLevel(levelName).StartColor;

			Init();
		}

		void SetPostPorcessShaderColor(CubeColor color)
		{
			postProcessShader.SetShaderParameter("cubeColor", Colors.GetColorVec4(color));
		}

		public override void _Ready()
		{
			nodePath = GetPath();
			globalConfigurations = GetNode<GlobalConfigurations>(GlobalConfigurations.NodePath);

			postProcessShader = postProcessInstance.MaterialOverride as ShaderMaterial;

			Init();


			ButtonLevelSelect.EntryLevelEvenet += (levelName) =>
			{
				StartLevelInitPlayer(levelName);
			};

			RetryButton.RetryButtonPressedEvenet += () =>
			{
				StartLevelInitPlayer(LevelsManager.CurrentLevel);
			};
		}



		public override void _Process(double delta)
		{
			if (!globalConfigurations.IsGamePause && LevelsManager.CurrentLevel != LevelName.MAIN_MENU)
			{
				RotateView(delta);

				if (!isTransforming)
				{
					CheckDown();
					TryMove();
				}
				else
				{
					MoveToTargetCube(delta);
				}

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
				mouseMoved += eventMouse.Relative * GetWindow().Size.Y / 648;
			}
		}


		void TryTarget(Vector3 tryPos)
		{
			tryVec = tryPos - prePos;
			if (tryVec.Length() > 0.001f)
			{
				bool canEntry = LevelsManager.CheckCanEntry(this, CubeBase.Vec3ConvertToVec3I(tryPos));
				if (canEntry)
				{
					targetPos = tryPos;
					isTransforming = true;


					LevelsManager.SignalExit(this, CubeBase.Vec3ConvertToVec3I(prePos));
					LevelsManager.SignalEnter(this, CubeBase.Vec3ConvertToVec3I(targetPos));
				}
			}
		}

		void CheckDown()
		{
			if (!isTransforming)
			{
				Vector3 tryPos = prePos + Vector3.Down;
				TryTarget(tryPos);
			}
		}

		void TryMove()
		{
			if (!isTransforming)
			{
				float inputX = Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right");
				float inputUP = Input.GetActionStrength("jump");
				float inputZ = Input.GetActionStrength("ui_up") - Input.GetActionStrength("ui_down");

				Vector3 inputVec3 = new Vector3(inputX, inputUP, inputZ);
				Vector3.Axis axis = inputVec3.Abs().MaxAxisIndex();

				Vector3 moveVecLocal = Vector3.Zero;
				moveVecLocal[(int)axis] = Math.Sign(inputVec3[(int)axis]);


				Vector3 dirForwardVec = GetClosestAxisOfCamera();
				Quaternion quaternionToDir = Quaternion.FromEuler(new Vector3(0, (float)Math.PI * 0.5f, 0));
				Vector3 dirLeftVec = quaternionToDir * dirForwardVec;

				Vector3 moveVec = moveVecLocal.X * dirLeftVec + moveVecLocal.Y * Vector3.Up + moveVecLocal.Z * dirForwardVec;

				Vector3 tryPos = prePos + moveVec;
				TryTarget(tryPos);
			}
		}

		void RotateView(double delta)
		{
			mouseMoved.X += (Input.GetActionStrength("ui_right_joystick_right") - Input.GetActionStrength("ui_right_joystick_left")) * 20.0f;
			mouseMoved.Y += (Input.GetActionStrength("ui_right_joystick_down") - Input.GetActionStrength("ui_right_joystick_up")) * 20.0f;

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

			int maxInd = Math.Abs(cameraLookAtDir.X) > Math.Abs(cameraLookAtDir.Z) ? 0 : 2;

			int signOfMax = Math.Sign(cameraLookAtDir[maxInd]);

			Vector3 result = Vector3.Zero;
			result[maxInd] = signOfMax;

			return result;
		}

		void MoveToTargetCube(double delta)
		{
			if (isTransforming)
			{
				transformedTime += delta;
				transformedTime = Math.Clamp(transformedTime, 0, globalConfigurations.MoveTime);

				Vector3 moveVec3 = targetPos - prePos;
				playerInstance.GlobalPosition = prePos + moveVec3 * (float)transformedTime / globalConfigurations.MoveTime;

				if (transformedTime >= globalConfigurations.MoveTime)
				{
					isTransforming = false;
					transformedTime = 0;


					LevelsManager.SignalExited(this, CubeBase.Vec3ConvertToVec3I(prePos));
					LevelsManager.SignalEntered(this, CubeBase.Vec3ConvertToVec3I(targetPos));

					playerInstance.GlobalPosition = playerInstance.GlobalPosition.Round();
					prePos = playerInstance.GlobalPosition;
				}
			}
		}

	}
}
