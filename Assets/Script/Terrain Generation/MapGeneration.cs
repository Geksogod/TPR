using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public enum DrawMode {
        PerlinNoise,
        ColorMap,
        Mesh
    }
    public DrawMode drawMode;
    public int Wight;
    public int Height;
    public float Scale;

    public int octaves;
    [Range (0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;
    public bool GenerationAllTime;
    public TerrainType[] Regions;
    public float heightMultipler;
    public AnimationCurve curve;
    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoise.GenerateNoise(Wight,Height,seed,Scale, octaves,persistance,lacunarity, offset);
        Color[] colorMap = new Color[Wight*Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Wight; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < Regions.Length; i++)
                {
                    if (currentHeight <= Regions[i].height)
                    {
                        colorMap[y * Wight + x] = Regions[i].color;
                        break;
                    }
                }
            }
        }
        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        if(drawMode==DrawMode.PerlinNoise)
            mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        else if(drawMode == DrawMode.ColorMap)
        {
            mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap,Wight,Height));
        }
        else if(drawMode == DrawMode.Mesh)
        {
            mapDisplay.DrawMesh(MeshGeneration.GenerareTerrainMesh(noiseMap, heightMultipler,curve), TextureGenerator.TextureFromColorMap(colorMap, Wight, Height));
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
