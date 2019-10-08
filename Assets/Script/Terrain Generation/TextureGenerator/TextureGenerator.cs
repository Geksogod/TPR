using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class TextureGenerator  
{
    public static Texture2D TextureFromColorMap(Color[] colorMap,int mapChankSize)
    {
        Texture2D texture = new Texture2D(mapChankSize, mapChankSize);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] HeightMap)
    {
        int mapChankSize = HeightMap.GetLength(0);
        int height = HeightMap.GetLength(1);

        Texture2D texture2D = new Texture2D(mapChankSize, height);
        Color[] colors = new Color[mapChankSize * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < mapChankSize; x++)
            {
                colors[y * mapChankSize + x] = Color.Lerp(Color.black, Color.white, HeightMap[x, y]);
            }
        }
        return TextureFromColorMap(colors,mapChankSize);
    }
}
