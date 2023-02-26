using UnityEngine;

public static class VoxelData
{
    public static readonly int ChunkWidth = 5;
    public static readonly int ChunkHeight = 15;
    
    public static readonly int TextureAtlasSizeInBlocks = 4;
    public static float NormalizedBlockTextureSize => 1f / TextureAtlasSizeInBlocks;
    
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

    public static readonly Vector3[] faceChecks =
    {
        new(0f, 0f, -1f),
        new(0f, 0f, 1f),
        new(0f, 1f, 0f),
        new(0f, -1f, 0f),
        new(-1f, 0f, 0f),
        new(1f, 0f, 0f),
    };
        
    public static readonly int[,] voxelTris = {
        // Back, Front, Top, Bottom, Left, Right
        // 0 1 2 2 1 3
        
        {0, 3, 1, 2}, // Back Face
        {5, 6, 4, 7}, // Front Face
        {3, 7, 2, 6}, // Top Face
        {1, 5, 0, 4}, // Bottom Face
        {4, 7, 0, 3}, // Left Face
        {1, 2, 5, 6}, // Right Face
    };

    public static readonly Vector2[] voxelUvs =
    {
        new(0, 0),
        new(0, 1),
        new(1, 0),
        new(1, 1),
    };
}
