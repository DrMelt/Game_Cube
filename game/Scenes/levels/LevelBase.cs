using Godot;
using System;
using System.Collections.Generic;

namespace GameKernel
{
	public partial class LevelBase : Node3D
	{
		[Export]
		LevelName levelName = LevelName.NULL;


		public override void _Ready()
		{
			List<Node> childrenAll = new List<Node>();
			Stack<Node> nodesStack = new Stack<Node>(GetChildren());

			while (nodesStack.Count > 0)
			{
				Node nodeTmp = nodesStack.Pop();
				childrenAll.Add(nodeTmp);

				foreach (Node childNode in nodeTmp.GetChildren())
				{
					nodesStack.Push(childNode);
				}
			}


			// GD.Print(levelName + ": " + childrenAll.ToString() + ", " + childrenAll.Count);

			List<CubeBase> cubes = new List<CubeBase>();
			List<Vector3I> cubeCoords = new List<Vector3I>();
			SpawnPoint spawnPoint = null;
			
			foreach (Node child in childrenAll)
			{
				CubeBase cubeBase = child as CubeBase;
				if (cubeBase == null)
				{
					continue;
				}
				cubeBase.InLevel = levelName;

				cubes.Add(cubeBase);
				cubeCoords.Add(CubeBase.Vec3ConvertToVec3I(cubeBase.GlobalPosition));

				SpawnPoint spawnPointTry = cubeBase as SpawnPoint;
				if (spawnPointTry != null)
				{
					spawnPoint = spawnPointTry;
				}
			}


			Vector3I minCoord = Vector3I.MaxValue, maxCoord = Vector3I.MinValue;
			foreach (Vector3I cubeCoord in cubeCoords)
			{

				for (int i = 0; i < 3; i++)
				{
					if (minCoord[i] > cubeCoord[i])
					{
						minCoord[i] = cubeCoord[i];
					}
					if (maxCoord[i] < cubeCoord[i])
					{
						maxCoord[i] = cubeCoord[i];
					}
				}
			}

			Vector3I size = maxCoord - minCoord + Vector3I.One;
			LevelContent newLevelContent = new LevelContent(minCoord, size, levelName);

			// GD.Print(levelName + ": " + cubes.ToString() + ", " + cubes.Count);


			foreach (CubeBase cube in cubes)
			{
				newLevelContent.SetCube(cube);
			}

			newLevelContent.SpawnPointInstance = spawnPoint;

			LevelsManager.SetLevel(newLevelContent);
		}
	}
}