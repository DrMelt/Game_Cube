using Godot;
using System;
using System.Collections.Generic;

namespace GameKernel
{
    public partial class PostProcessing3D : MeshInstance3D
    {
        [Export]
        bool isUsingTriangleMesh = false;

        public override void _Ready()
        {
            Visible = true;

            if (isUsingTriangleMesh)
            { InitMesh(); }
        }

        private void InitMesh()
        {
            Mesh = new ArrayMesh();

            var surfaceArray = new Godot.Collections.Array();
            surfaceArray.Resize((int)Mesh.ArrayType.Max);

            // C# arrays cannot be resized or expanded, so use Lists to create geometry.
            var verts = new List<Vector3>
            {
                new Vector3(-1.0f, -1.0f, 0.0f),
                new Vector3(-1.0f, 3.0f, 0.0f),
                new Vector3(3.0f, -1.0f, 0.0f)
            };

            // Convert Lists to arrays and assign to surface array
            surfaceArray[(int)Mesh.ArrayType.Vertex] = verts.ToArray();
            // surfaceArray[(int)Mesh.ArrayType.TexUV] = uvs.ToArray();
            // surfaceArray[(int)Mesh.ArrayType.Normal] = normals.ToArray();
            // surfaceArray[(int)Mesh.ArrayType.Index] = indices.ToArray();

            var arrMesh = Mesh as ArrayMesh;
            // Create mesh surface from mesh array
            // No blendshapes, lods, or compression used.
            arrMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, surfaceArray);
        }
    }
}

