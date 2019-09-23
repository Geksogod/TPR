using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGeneration 
{
   public static MeshData GenerareTerrainMesh(float [,] heightMap, float heightMultiplier,AnimationCurve heightCurve)
   {
        int wight = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        MeshData meshData = new MeshData(wight, height);
        float topLeftX = (wight - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;
        int verticesIndes = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < wight; x++)
            {
                meshData.vertices[verticesIndes] = new Vector3((topLeftX +x)*3, heightCurve.Evaluate(heightMap[x, y])*heightMultiplier,(topLeftZ - y)*3);
                meshData.uvs[verticesIndes] = new Vector2(x / (float)wight, y / (float)height);
                if (x < wight - 1 && y < height - 1)
                {
                    meshData.AddTriangle(verticesIndes, verticesIndes + wight + 1, verticesIndes + wight);
                    meshData.AddTriangle(verticesIndes +wight+1, verticesIndes,verticesIndes + 1);
                }
                verticesIndes++;
            }
        }
        return meshData;
   }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public int trienglesIndex;
    public Vector2[] uvs;

    public MeshData(int wight, int height)
    {
        vertices = new Vector3[wight * height];
        uvs = new Vector2[wight * height];
        triangles = new int[(wight - 1) * (height - 1)*6];
    }

    public void AddTriangle(int a,int b,int c)
    {
        triangles[trienglesIndex] = a;
        triangles[trienglesIndex + 1] = b;
        triangles[trienglesIndex + 2] = c;
        trienglesIndex += 3;
    }
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
