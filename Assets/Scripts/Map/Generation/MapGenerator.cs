using System.Collections;
using UnityEngine;

public static class MapGenerator {

    public static int[,] GenerateMap(Vector2 size) {
        int[,] map = new int[(int) size.y, (int) size.x];

        for (int y = 0; y < size.y; y++) {
            for (int x = 0; x < size.x; x++) {
                map[y, x] = 0;
            }
        }

        return map;
    }

}
