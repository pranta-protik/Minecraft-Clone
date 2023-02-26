using System;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private BlockType[] _blockTypes;
    
    public BlockType[] BlockTypes => _blockTypes;
}

[Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;

    [Header("Texture Values")] 
    public int backFaceTexture;
    public int frontFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;
    
    // Back, Front, Top, Bottom, Left, Right
    
    public int GetTextureId(int faceIndex)
    {
        switch (faceIndex)
        {
            case 0:
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return topFaceTexture;
            case 3:
                return bottomFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;
            default:
                Debug.LogError("Error in GetTextureID; Invalid face index");
                return 0;
        }    
    }
}
