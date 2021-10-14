using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class RiverGeneration {

    public static List<Vector2> ConnectWaterBodies(int[,] map, float[,] noise) {
        List<Vector2> path = new List<Vector2>();



        return path;
    }

    public static void Debug(int[,] map) {
        List<List<Vector2>> waterBodies = GetWaterBodies(map);
        // DebugUtils.DumpString("Water bodies count : " + waterBodies.Count);

        List<string> a = new List<string>();
        int count = 0;
        foreach (List<Vector2> body in waterBodies) {
            Vector2 center = GetWaterBodyCenter(body);
            a.Add("Body of water center #" + count + " -> center at (" + center.x + ";" + center.y + ")");
            count++;
        }
        DebugUtils.DumpStringList(a);
    }

    private static List<List<Vector2>> GetWaterBodies(int[,] map) {
        List<List<Vector2>> waterBodies = new List<List<Vector2>>();

        for (int y = 0; y < map.GetLength(0); y++) {
            for (int x = 0; x < map.GetLength(1); x++) {
                if (map[y, x] == 3) {
                    Vector2 cell = new Vector2(x, y);
                    if (!IsPartOfWaterBodies(waterBodies, cell)) {
                        waterBodies.Add(GetWaterBodyFromCell(map, cell));
                    }
                }
            }
        }

        return waterBodies;
    }

    private static List<Vector2> GetWaterBodyFromCell(int[,] map, Vector2 waterCell) {
        if (map[(int)waterCell.y, (int)waterCell.x] != 3) return null;

        List<Vector2> waterBody = new List<Vector2>();
        List<Vector2> queue = new List<Vector2>();
        queue.Add(waterCell);

        while (queue.Count > 0) {
            foreach (Vector2 n in GetNeighbours(queue[0])) {
                if (!IsPartOfWaterBody(waterBody, n)) {
                    if (IsCellValid(map, n) && map[(int)n.y, (int)n.x] == 3 && !IsPartOfWaterBody(queue, n)) {
                        queue.Add(n);
                    }
                }
            }

            waterBody.Add(queue[0]);
            queue.Remove(queue[0]);
        }

        return waterBody;
    }

    private static Vector2 GetWaterBodyCenter(List<Vector2> waterBody) {
        float xSum = 0.0f;
        float ySum = 0.0f;

        foreach (Vector2 v in waterBody) {
            xSum += v.x;
            ySum += v.y;
        }

        return new Vector2((int)Mathf.Round(xSum / waterBody.Count), (int)Mathf.Round(ySum / waterBody.Count));
    }

    private static bool IsPartOfWaterBody(List<Vector2> waterBody, Vector2 waterCell) {
        foreach (Vector2 w in waterBody) {
            if (waterCell == w) return true;
        }
        return false;
    }

    private static bool IsPartOfWaterBodies(List<List<Vector2>> waterBodies, Vector2 waterCell) {
        foreach (List<Vector2> wb in waterBodies) {
            if (IsPartOfWaterBody(wb, waterCell)) return true;
        }
        return false;
    }

    private static List<Vector2> GetNeighbours(Vector2 cell) {
        List<Vector2> neighbours = new List<Vector2>();

        neighbours.Add(new Vector2(cell.x - 1, cell.y));
        neighbours.Add(new Vector2(cell.x + 1, cell.y));
        neighbours.Add(new Vector2(cell.x, cell.y - 1));
        neighbours.Add(new Vector2(cell.x, cell.y + 1));

        return neighbours;
    }

    private static bool IsCellValid(int[,] map, Vector2 cell) {
        return (cell.x >= 0 && cell.x < map.GetLength(1) && cell.y >= 0 && cell.y < map.GetLength(0));
    }
}

