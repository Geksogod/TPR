using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawTexture(Texture2D texture)
    {       
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    public void DrawMesh(MeshData meshData, Texture2D texture,bool save)
    {
        Mesh mesh = meshData.CreateMesh();
        mesh.name = "Terrain";
        if (save)
        {
            string filePath =
            EditorUtility.SaveFilePanelInProject("Save Procedural Mesh", "Procedural Mesh", "asset", "");
            AssetDatabase.CreateAsset(mesh, filePath);
            filePath =
            EditorUtility.SaveFilePanelInProject("Save Procedural texture", "Procedural texture", "asset", "");
            AssetDatabase.CreateAsset(texture, filePath);
        }
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
