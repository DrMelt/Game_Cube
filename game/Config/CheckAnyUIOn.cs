using Godot;
using System;

namespace GameKernel
{
	public partial class CheckAnyUIOn : Node
	{
		[Export]
		Godot.Collections.Array<Control> uiList;

		GlobalConfigurations globalConfigurations;

		public void RegisterNewUI(Control ui)
		{
			uiList.Add(ui);
			ui.TreeExiting += () => { uiList.Remove(ui); };
		}

		public override void _Ready()
		{
			globalConfigurations = GetNode<GlobalConfigurations>(GlobalConfigurations.NodePath);

			foreach (var ui in uiList)
			{
				ui.TreeExiting += () => { uiList.Remove(ui); };
			}
		}

		public override void _Process(double delta)
		{
			bool flag = false;
			foreach (Control ui in uiList)
			{
				if (ui.Visible)
				{
					flag = true;
					break;
				}
			}

			globalConfigurations.IsAnyUIOn = flag;
		}
	}
}

