using System.Collections;
using UnityEngine;

public class Noise : MonoBehaviour
{
	public static float[, ] GenerateNoiseMap (int mapWidth_, int mapHeight_, int seed_, float scale_, int octaves_, float persistance_, float lacunarity_, Vector2 offset_)
	{
		float[, ] noiseMap = new float[mapWidth_, mapHeight_];

		System.Random prng = new System.Random (seed_);
		Vector2[] octaveOffsets = new Vector2[octaves_];

		for (int i = 0; i < octaves_; i++)
		{
			float offsetX = prng.Next (-100000, 10000) + offset_.x;
			float offsetY = prng.Next (-100000, 10000) + offset_.y;
			octaveOffsets[i] = new Vector2 (offsetX, offsetY);
		}

		float halfWidth = mapWidth_ / 2f;
		float halfHeight = mapHeight_ / 2f;

		if (scale_ <= 0)
			scale_ = 0.001f;

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		for (int y = 0; y < mapHeight_; y++)
		{
			for (int x = 0; x < mapWidth_; x++)
			{

				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves_; i++)
				{
					float sampleX = (x - halfWidth) / scale_ * frequency + octaveOffsets[i].x;
					float sampleY = (y - halfHeight) / scale_ * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;
					amplitude *= persistance_;
					frequency *= lacunarity_;
				}

				if (noiseHeight > maxNoiseHeight)
					maxNoiseHeight = noiseHeight;
				else if (noiseHeight < minNoiseHeight)
					minNoiseHeight = noiseHeight;

				noiseMap[x, y] = noiseHeight;

			}
		}

		for (int y = 0; y < mapHeight_; y++)
		{
			for (int x = 0; x < mapWidth_; x++)
			{
				noiseMap[x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
			}
		}

		return noiseMap;
	}
}