using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    public void DrawTexture(Texture2D texture)
    {


        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        Mesh meshInfo = meshData.CreateMesh();
        meshFilter.sharedMesh = meshInfo;
        meshRenderer.sharedMaterial.mainTexture = texture;
        meshCollider.sharedMesh = meshInfo;
    }
}
