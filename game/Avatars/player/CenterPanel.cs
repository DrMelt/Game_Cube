using Godot;
using System;

namespace GameKernel
{
	public partial class CenterPanel : Panel
	{
		CubeColor color = CubeColor.WHITE;

		public CubeColor Color
		{
			get => color;
			set
			{
				color = value;
				SetColor(value);
			}
		}

		StyleBoxFlat styleBoxFlat;

		void SetColor(CubeColor color)
		{

			Vector4 colorVec4 = Colors.GetColorVec4(color);
			styleBoxFlat.Set("bg_color", new Godot.Color(colorVec4.X, colorVec4.Y, colorVec4.Z, colorVec4.W));
			// RemoveThemeStyleboxOverride("CenterPanel");
			// AddThemeStyleboxOverride("panel", styleBoxFlat);
		}

		public override void _Ready()
		{
			styleBoxFlat = GetThemeStylebox("panel") as StyleBoxFlat;

			SetColor(color);
		}

		public override void _Process(double delta)
		{

		}
	}
}

