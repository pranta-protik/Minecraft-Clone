using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _meshFilter;

    private int _vertexIndex;
    private List<Vector3> _vertices = new();
    private List<int> _triangles = new();
    private List<Vector2> _uvs = new();
    private byte[,,] _voxelMap = new byte[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];
    private World _world;
    
    private void Start()
    {
        _world = GameObject.Find("World").GetComponent<World>();
        
        PopulateVoxelMap();
        CreateMeshData();
        CreateMesh();
    }

    private void PopulateVoxelMap()
    {
        for (var y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (var x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (var z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    if (y < 1)
                    {
                        _voxelMap[x, y, z] = 0;
                    }
                    else if (y == VoxelData.ChunkHeight - 1)
                    {
                        _voxelMap[x, y, z] = 2;
                    }
                    else
                    {
                        _voxelMap[x, y, z] = 1;
                    }
                }
            }
        }
    }

    private void CreateMeshData()
    {
        for (var y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (var x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (var z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    AddVoxelDataToChunk(new Vector3(x, y, z));
                }
            }
        }
    }

    private bool CheckVoxel(Vector3 pos)
    {
        var x = Mathf.FloorToInt(pos.x);
        var y = Mathf.FloorToInt(pos.y);
        var z = Mathf.FloorToInt(pos.z);

        if (x < 0 || x > VoxelData.ChunkWidth - 1 || y < 0 || y > VoxelData.ChunkHeight - 1 || z < 0 || z > VoxelData.ChunkWidth - 1)
        {
            return false;
        }

        return _world.BlockTypes[_voxelMap[x, y, z]].isSolid;
    }

    private void AddVoxelDataToChunk(Vector3 pos)
    {
        for (var p = 0; p < 6; p++)
        {
            if (!CheckVoxel(pos + VoxelData.faceChecks[p]))
            {
                var blockID = _voxelMap[(int)pos.x, (int)pos.y, (int)pos.z];
                    
                _vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[p, 0]]);
                _vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[p, 1]]);
                _vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[p, 2]]);
                _vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[p, 3]]);
                
                AddTexture(_world.BlockTypes[blockID].GetTextureId(p));
                
                _triangles.Add(_vertexIndex);
                _triangles.Add(_vertexIndex + 1);
                _triangles.Add(_vertexIndex + 2);
                _triangles.Add(_vertexIndex + 2);
                _triangles.Add(_vertexIndex + 1);
                _triangles.Add(_vertexIndex + 3);

                _vertexIndex += 4;

                // for (var i = 0; i < 6; i++)
                // {
                //     var triangleIndex = VoxelData.voxelTris[p, i];
                //     _vertices.Add(VoxelData.voxelVerts[triangleIndex] + pos);
                //     _triangles.Add(_vertexIndex);
                //
                //     _uvs.Add(VoxelData.voxelUvs[i]);
                //
                //     _vertexIndex++;
                // }
            }
        }
    }

    private void CreateMesh()
    {
        var mesh = new Mesh
        {
            vertices = _vertices.ToArray(),
            triangles = _triangles.ToArray(),
            uv = _uvs.ToArray()
        };
        
        mesh.RecalculateNormals();

        _meshFilter.mesh = mesh;
    }

    private void AddTexture(int textureID)
    {
        var y = Mathf.Floor((float)textureID / VoxelData.TextureAtlasSizeInBlocks);
        var x = textureID - (y * VoxelData.TextureAtlasSizeInBlocks);

        x *= VoxelData.NormalizedBlockTextureSize;
        y *= VoxelData.NormalizedBlockTextureSize;

        y = 1f - y - VoxelData.NormalizedBlockTextureSize;

        _uvs.Add(new Vector2(x, y));
        _uvs.Add(new Vector2(x, y + VoxelData.NormalizedBlockTextureSize));
        _uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y));
        _uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y + VoxelData.NormalizedBlockTextureSize));
    }
}
