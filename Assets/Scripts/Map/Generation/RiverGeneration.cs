using System.Collections.Generic;
using UnityEngine;

// https://brilliant.org/wiki/a-star-search/
// https://www.geeksforgeeks.org/a-search-algorithm/
// https://mat.uab.cat/~alseda/MasterOpt/AStar-Algorithm.pdf

public static class RiverGeneration {

    public static List<Vector2> ConnectWaterBodies(int[,] map, float[,] noise) {
        List<Vector2> waterCells = new List<Vector2>();

        List<List<Vector2>> waterBodies = GetWaterBodies(map);
        List<Vector2> waterBodiesCenter = new List<Vector2>();
        foreach (List<Vector2> body in waterBodies) {
            waterBodiesCenter.Add(GetWaterBodyCenter(body));
        }

        int riverCount = Random.Range(2, waterBodiesCenter.Count - 1);
        for (int i = 0; i < riverCount; i++) {
            int start = Random.Range(0, waterBodies.Count - 1);
            Vector2 end = GetNearestPoint(waterBodiesCenter, waterBodiesCenter[start]);

            List<Vector2> path = AStar.GeneratePath(map, noise, waterBodiesCenter[start], end);
            foreach (Vector2 cell in path) {
                waterCells.Add(cell);
            }
        }

        return waterCells;
    }

    public static void Debug(int[,] map) {
        List<List<Vector2>> waterBodies = GetWaterBodies(map);

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
            foreach (Vector2 n in GetNeighbours(map, queue[0])) {
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

    // DOUBLE IN AStar.cs
    private static List<Vector2> GetNeighbours(int[,] map, Vector2 cell) {
        List<Vector2> neighbours = new List<Vector2>();

        if (cell.x - 1 >= 0) neighbours.Add(new Vector2(cell.x - 1, cell.y));
        if (cell.x + 1 < map.GetLength(1)) neighbours.Add(new Vector2(cell.x + 1, cell.y));
        if (cell.y - 1 >= 0) neighbours.Add(new Vector2(cell.x, cell.y - 1));
        if (cell.y + 1 < map.GetLength(0)) neighbours.Add(new Vector2(cell.x, cell.y + 1));

        return neighbours;
    }

    private static Vector2 GetNearestPoint(List<Vector2> list, Vector2 point) {
        float min = 999999f;
        Vector2 nearest = point;
        foreach (Vector2 v in list) {
            float distance = Vector2.Distance(v, point);
            if (distance < min && v != point) {
                min = distance;
                nearest = v;
            }
        }
        return nearest;
    }

    private static bool IsCellValid(int[,] map, Vector2 cell) {
        return (cell.x >= 0 && cell.x < map.GetLength(1) && cell.y >= 0 && cell.y < map.GetLength(0));
    }
}

