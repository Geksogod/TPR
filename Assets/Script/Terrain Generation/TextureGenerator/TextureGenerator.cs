using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator  
{
    public static Texture2D TextureFromColorMap(Color[] colorMap,int Wight,int Height)
    {
        Texture2D texture = new Texture2D(Wight, Height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] HeightMap)
    {
        int wight = HeightMap.GetLength(0);
        int height = HeightMap.GetLength(1);

        Texture2D texture2D = new Texture2D(wight, height);
        Color[] colors = new Color[wight * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < wight; x++)
            {
                colors[y * wight + x] = Color.Lerp(Color.black, Color.white, HeightMap[x, y]);
            }
        }
        return TextureFromColorMap(colors,wight,height);
    }
}
