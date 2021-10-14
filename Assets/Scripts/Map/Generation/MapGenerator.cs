using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    private float _xOrg = 0.0f;
    private float _yOrg = 0.0f;
    public float _scale = 1.0f;

    public float _riverFilter = 0.4f;
    public float _forestFilter = 0.5f;

    public int[,] GenerateMap(Vector2 size) {
        _xOrg = Random.Range(0.0f, 100.0f);
        _yOrg = Random.Range(0.0f, 100.0f);

        int[,] map = new int[(int)size.y, (int)size.x];
        float[,] noise = GeneratePerlinNoise(size);

        // DebugUtils.DumpFloat2DArray(noise, "Perlin Noise");
        
        for (int y = 0; y < size.y; y++) {
            for (int x = 0; x < size.x; x++) {
                if (map[y,x] != 3) {
                    if (noise[y, x] > _forestFilter) {
                        map[y, x] = 2;
                    }
                    else if (noise[y,x] > _riverFilter) {
                        map[y, x] = 0;
                    }
                    else {
                        map[y, x] = 3;
                    }
                }
            }
        }

        RiverGeneration.Debug(map);

        return map;
    }

    /*private int[,] GenerateBaseRivers(Vector2 size) {
        int[,] riverMap = new int[(int)size.y, (int)size.x];

        for (int y = 0; y < size.y; y++) {
            for (int x = 0; x < size.x; x++) {
                riverMap[y, x] = 0;
            }
        }
        
        for (int i = 0; i < Random.Range(1, 4); i++) {
            int direction = Random.Range(0, 2);
            Vector2 current;
            int directionChange;
            switch (direction) {
                // SOUTH
                case 0:
                    current = new Vector2(Random.Range(0, (int)size.x), 0);
                    riverMap[(int)current.y, (int)current.x] = 3;
                    while (current.y < size.y - 1 && current.x > 0 && current.x < size.x) {
                        directionChange = Random.Range(-1, 2);
                        current += new Vector2(directionChange, 1);
                        if (current.x >= size.x || current.x < 0) { current -= new Vector2(directionChange, 0); }
                        riverMap[(int)current.y, (int)current.x] = 3;
                        if (directionChange != 0) { riverMap[(int)current.y, (int)current.x - directionChange] = 3; }
                    }
                    break;

                // EAST
                case 1:
                    current = new Vector2(0, Random.Range(0, (int)size.y));
                    riverMap[(int)current.y, (int)current.x] = 3;
                    while (current.x < size.x - 1 && current.y > 0 && current.y < size.y) {
                        directionChange = Random.Range(-1, 2);
                        current += new Vector2(1, directionChange);
                        if (current.y >= size.y || current.y < 0) { current -= new Vector2(0, directionChange); }
                        riverMap[(int)current.y, (int)current.x] = 3;
                        if (directionChange != 0) { riverMap[(int)current.y - directionChange, (int)current.x] = 3; }
                    }
                    break;
            }
        }
        
        return riverMap;
    }*/

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
