﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGeneration 
{
   public static MeshData GenerareTerrainMesh(float [,] heightMap, float heightMultiplier,AnimationCurve heightCurve)
   {
        int mapChankSize = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        MeshData meshData = new MeshData(mapChankSize, height);
        float topLeftX = (mapChankSize - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;
        int verticesIndes = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < mapChankSize; x++)
            {
                meshData.vertices[verticesIndes] = new Vector3((topLeftX +x)*3, heightCurve.Evaluate(heightMap[x, y])*heightMultiplier,(topLeftZ - y)*3);
                meshData.uvs[verticesIndes] = new Vector2(x / (float)mapChankSize, y / (float)height);
                if (x < mapChankSize - 1 && y < height - 1)
                {
                    meshData.AddTriangle(verticesIndes, verticesIndes + mapChankSize + 1, verticesIndes + mapChankSize);
                    meshData.AddTriangle(verticesIndes +mapChankSize+1, verticesIndes,verticesIndes + 1);
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

    public MeshData(int mapChankSize, int height)
    {
        vertices = new Vector3[mapChankSize * height];
        uvs = new Vector2[mapChankSize * height];
        triangles = new int[(mapChankSize - 1) * (height - 1)*6];
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
