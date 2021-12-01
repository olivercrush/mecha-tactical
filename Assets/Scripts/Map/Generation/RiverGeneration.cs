﻿using System.Collections.Generic;
using UnityEngine;

// https://brilliant.org/wiki/a-star-search/
// https://www.geeksforgeeks.org/a-search-algorithm/
// https://www.educative.io/edpresso/what-is-the-a-star-algorithm
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

            //DebugUtils.DumpString("River from point (" + waterBodiesCenter[start].x + ";" + waterBodiesCenter[start].y + ") to point (" + end.x + ";" + end.y + ")");

            List<Vector2> path = AStarRiver(map, noise, waterBodiesCenter[start], end);
            foreach (Vector2 cell in path) {
                waterCells.Add(cell);
            }
        }

        /*for (int i = 0; i < waterBodiesCenter.Count - 1; i++) {
            List<Vector2> path = AStarRiver(map, noise, waterBodiesCenter[i], GetNearestPoint(waterBodiesCenter, waterBodiesCenter[i]));
            foreach (Vector2 cell in path) {
                waterCells.Add(cell);
            }
        }*/

        return waterCells;
    }

    private static List<Vector2> AStarRiver(int[,] map, float[,] noise, Vector2 start, Vector2 goal) {

        AStarNode goalNode = null;

        // Create open and closed lists
        List<AStarNode> open = new List<AStarNode>();
        List<AStarNode> closed = new List<AStarNode>();

        // Put node_start in the open list
        open.Add(new AStarNode(null, start, goal, 0));

        // While the open list is not empty
        while (open.Count > 0) {
            // Take the node with the lowest f from the open list
            AStarNode current = GetLowestF(open);
            open.Remove(current);

            DebugUtils.DumpString("current : " + current.GetPosition().ToString() + " / goal : " + goal.ToString());

            // If the current node is the goal, we found the solution
            if (current.GetPosition() == goal) {
                goalNode = current;
                break;
            }

            // We generate all neighbours from the current node
            foreach (Vector2 neighbourPos in GetNeighbours(map, current.GetPosition())) {

                DebugUtils.DumpString("checking neighbour : " + neighbourPos.ToString());

                // We create the neighbour node and set its f
                float cellNoise = noise[(int)neighbourPos.y, (int)neighbourPos.x];

                float mapDivider;
                switch (map[(int)neighbourPos.y, (int)neighbourPos.x]) {
                    case 2:
                        mapDivider = 0.0000000000000000001f;
                        break;

                    case 3:
                        mapDivider = 100000000000000000000f;
                        break;

                    default:
                        mapDivider = 1f;
                        break;
                }

                float neighbourCost = current.GetG() + cellNoise / mapDivider;


                if (IsInList(open, neighbourPos)) {
                    AStarNode tmp = GetNodeFromList(open, neighbourPos);
                    DebugUtils.DumpString("neighbour is in open list");
                    if (tmp.GetG() <= neighbourCost) continue;
                    DebugUtils.DumpString("with higer g value");
                }
                else if (IsInList(closed, neighbourPos)) {
                    AStarNode tmp = GetNodeFromList(closed, neighbourPos);
                    DebugUtils.DumpString("neighbour is in closed list");
                    if (tmp.GetG() <= neighbourCost) continue;
                    DebugUtils.DumpString("with higer g value, sending it to open list");
                    open.Add(tmp);
                    RemoveFromList(closed, tmp);
                }
                else {
                    DebugUtils.DumpString("neighbour is in no lists, sending it to open list");
                    AStarNode neighbourNode = new AStarNode(current, neighbourPos, goal, neighbourCost);
                    open.Add(neighbourNode);
                }
            }

            closed.Add(current);
        }

        List<Vector2> path = new List<Vector2>();
        AStarNode buffer = goalNode;
        while (buffer != null && buffer.GetPosition() != start) {
            path.Add(buffer.GetPosition());
            buffer = buffer.GetParent();
        }

        return path;
    }

    /* private static List<Vector2> AStarRiver(int[,] map, float[,] noise, Vector2 start, Vector2 goal) {
        List<AStarNode> closed = new List<AStarNode>();

        List<AStarNode> open = new List<AStarNode>();
        open.Add(new AStarNode(null, start, start, 0));

        AStarNode goalNode = null;

        bool searching = true;
        while (searching && open.Count > 0) {

            // DEBUG
            //DebugUtils.DumpString("open count : " + open.Count + " / closed count : " + closed.Count);

            AStarNode q = GetLowestF(open);
            open.Remove(q);

            DebugUtils.DumpString("current : " + q.GetPosition().ToString() + " / goal : " + goal.ToString());

            if (q.GetPosition() == goal) {
                goalNode = q;
                break;
            }

            foreach (Vector2 neighbour in GetNeighbours(map, q.GetPosition())) {
                if (IsCellValid(map, neighbour)) {

                    /*if (neighbour == goal) {
                        goalNode = new AStarNode(q, neighbour, goal, q.GetG() + 0);
                        searching = false;
                        break; 
                    }

                    float mapDivider;
                    switch (map[(int)neighbour.y, (int)neighbour.x]) {
                        case 2:
                            mapDivider = 0.01f;
                            break;

                        case 3:
                            mapDivider = 1000f;
                            break;

                        default:
                            mapDivider = 1f;
                            break;
                    }


                    float cellNoise = noise[(int)neighbour.y, (int)neighbour.x];
                    AStarNode neighbourNode = new AStarNode(q, neighbour, goal, q.GetG() + cellNoise / mapDivider);

                    if (IsInList(open, neighbourNode)) {
                        if (GetNodeFromList(open, neighbourNode).GetG() <= neighbourNode.GetG()) break;
                    }
                    else if (IsInList(closed, neighbourNode)) {
                        if (GetNodeFromList(closed, neighbourNode).GetG() <= neighbourNode.GetG()) break;
                        RemoveFromList(closed, neighbourNode);
                        open.Add(neighbourNode);
                    }
                    else {
                        open.Add(neighbourNode);
                    }



                    /*if (!closed.Contains(neighbourNode)) {
                        if (!IsNodeInListWithLowerF(open, neighbourNode)) {
                            open.Add(neighbourNode);
                        }
                    }*/

                    /*if (!IsNodeInListWithLowerF(open, neighbourNode)) {
                        if (!IsNodeInListWithLowerF(closed, neighbourNode)) {
                            open.Add(neighbourNode);
                        }
                    }
                }
            }

            closed.Add(q);
        }
        DebugUtils.DumpString("A* over");

        List<Vector2> path = new List<Vector2>();
        AStarNode buffer = goalNode;
        while (buffer != null && buffer.GetPosition() != start) {
            path.Add(buffer.GetPosition());
            buffer = buffer.GetParent();
        }

        return path;
    } */

    private static void RemoveFromList(List<AStarNode> list, AStarNode node) {
        AStarNode toRemove = null;
        foreach (AStarNode n in list) {
            if (n.GetPosition() == node.GetPosition()) toRemove = n;
        }
        if (toRemove != null) list.Remove(toRemove);
    }

    private static AStarNode GetNodeFromList(List<AStarNode> list, Vector2 pos) {
        foreach (AStarNode n in list) {
            if (n.GetPosition() == pos) return n;
        }
        return null;
    }

    private static bool IsInList(List<AStarNode> list, Vector2 pos) {
        foreach (AStarNode n in list) {
            if (n.GetPosition() == pos) return true;
        }
        return false;
    }

    private static bool IsNodeInListWithLowerF(List<AStarNode> list, AStarNode node) {
        foreach (AStarNode n in list) {
            if (node.GetPosition() == n.GetPosition() && node.GetF() > n.GetF()) return true;
        }
        return false;
    }
    
    private static AStarNode GetLowestF(List<AStarNode> nodes) {
        float minF = 9999999999;
        AStarNode minNode = nodes[0];
        foreach (AStarNode node in nodes) {
            if (node.GetF() < minF) {
                minF = node.GetF();
                minNode = node;
            }
        }
        return minNode;
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

    protected class AStarNode {
        Vector2 pos;
        float g;
        float h;
        AStarNode parent;

        public AStarNode(AStarNode parent, Vector2 pos, Vector2 goal, float g) {
            this.parent = parent;
            this.pos = pos;
            this.g = g;
            this.h = (Mathf.Abs(pos.x - goal.x) + Mathf.Abs(pos.y - goal.y)) * 0.5f;
        }

        public void SetG(float g) {
            this.g = g;
        }

        public AStarNode GetParent() { return parent; }
        public Vector2 GetPosition() { return pos; }
        public float GetG() { return g; }
        public float GetF() { return g+h; }
    }
}

