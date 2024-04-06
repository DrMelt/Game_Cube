using Godot;
using System;

namespace GameKernel
{
	public partial class Cross : Node3D
	{
		[Export]
		float lifeTime = 0.5f;

		[Export]
		MeshInstance3D meshRef;
		ShaderMaterial shaderMaterial;

		float existedTime = 0.0f;

		public override void _Ready()
		{
			shaderMaterial = meshRef.MaterialOverride as ShaderMaterial;
		}

		public override void _Process(double delta)
		{
			existedTime += (float)delta;

			shaderMaterial.SetShaderParameter("CrossAlpha", (lifeTime - existedTime) / lifeTime);

			if (existedTime > lifeTime)
			{
				QueueFree();
			}
		}
	}
}

