using UnityEngine;

public static class VoxelData
{
    public static readonly Vector3[] voxelVerts = {
        new(0f, 0f, 0f),
        new(1f, 0f, 0f),
        new(1f, 1f, 0f),
        new(0f, 1f, 0f),
        new(0f, 0f, 1f),
        new(1f, 0f, 1f),
        new(1f, 1f, 1f),
        new(0f, 1f, 1f),
    };

    public static readonly int[,] voxelTris = {
        {0, 3, 1, 1, 3, 2}, // Back Face
        {5, 6, 4, 4, 6, 7}, // Front Face
        {3, 7, 2, 2, 7, 6}, // Top Face
        {1, 5, 0, 0, 5, 4}, // Bottom Face
        {4, 7, 0, 0, 7, 3}, // Left Face
        {1, 2, 5, 5, 2, 6}, // Right Face
    };

    public static readonly Vector2[] voxelUvs =
    {
        new(0, 0),
        new(0, 1),
        new(1, 0),
        new(1, 0),
        new(0, 1),
        new(1, 1),
    };
}
