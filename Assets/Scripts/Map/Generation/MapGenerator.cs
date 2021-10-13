using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public float _xOrg = 0.0f;
    public float _yOrg = 0.0f;
    public float _scale = 1.0f;

    public float _riverFilter = 0.4f;
    public float _forestFilter = 0.5f;

    public int[,] GenerateMap(Vector2 size) {
        int[,] map = new int[(int) size.y, (int) size.x];
        float[,] noise = GeneratePerlinNoise(size);
        DebugUtils.DumpFloat2DArray(noise, "Perlin Noise");

        for (int y = 0; y < size.y; y++) {
            for (int x = 0; x < size.x; x++) {
                if (noise[y,x] > _forestFilter) {
                    map[y, x] = 2;
                }
                else if (noise[y, x] > _riverFilter) {
                    map[y, x] = 0;
                }
                else {
                    map[y, x] = 3;
                }
            }
        }

        return map;
    }

    private float[,] GeneratePerlinNoise(Vector2 size) {
        float[,] noise = new float[(int)size.y, (int)size.x];

        for (int y = 0; y < size.y; y++) {
            for (int x = 0; x < size.x; x++) {
                float xCoord = _xOrg + x / size.x * _scale;
                float yCoord = _yOrg + y / size.y * _scale;
                noise[y, x] = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }

        return noise;
    }
}
