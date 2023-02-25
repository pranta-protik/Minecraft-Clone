using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _meshFilter;

    private void Start()
    {
        var vertexIndex = 0;
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();

        for (var p = 0; p < 6; p++)
        {
            for (var i = 0; i < 6; i++)
            {
                var triangleIndex = VoxelData.voxelTris[p, i];
                vertices.Add(VoxelData.voxelVerts[triangleIndex]);
                triangles.Add(vertexIndex);

                uvs.Add(VoxelData.voxelUvs[i]);

                vertexIndex++;
            }
        }

        var mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            uv = uvs.ToArray()
        };
        
        mesh.RecalculateNormals();

        _meshFilter.mesh = mesh;
    }
}
