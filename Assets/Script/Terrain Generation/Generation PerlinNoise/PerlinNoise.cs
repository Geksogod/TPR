using UnityEngine;

public static class PerlinNoise
{

    public static float[,] GenerateNoise(int mapChankSize,int seed, float scale, int octaves, float persistance, float lacunarity,Vector2 offset)
    {
        if (octaves > 30)
        {
            octaves = 30;
        }
        else if (octaves<=0)
        {
            octaves = 1;
        }
        System.Random prng = new System.Random(seed);
        Vector2[] octavesOffset = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000)+offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octavesOffset[i] = new Vector2(offsetX, offsetY);
        }
        float[,] perlinNoise = new float[mapChankSize, mapChankSize];
        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        float halfmapChankSize = mapChankSize / 2;

        float halfHeight = mapChankSize / 2;

        float maxValueHeight = float.MinValue;
        float minValueHeight = float.MaxValue;
        for (int y = 0; y < mapChankSize; y++)
        {
            for (int x = 0; x < mapChankSize; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x- halfmapChankSize) / scale * frequency+octavesOffset[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octavesOffset[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                    //perlinNoise[x, y] = perlinValue;
                }
                if (noiseHeight > maxValueHeight)
                {
                    maxValueHeight = noiseHeight;
                }
                else if (noiseHeight < minValueHeight)
                {
                    minValueHeight = noiseHeight;
                }
                perlinNoise[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < mapChankSize; y++)
        {
            for (int x = 0; x < mapChankSize; x++)
            {
                perlinNoise[x, y] = Mathf.InverseLerp(minValueHeight, maxValueHeight, perlinNoise[x, y]);
            }
        }

        return perlinNoise;
    }
}
