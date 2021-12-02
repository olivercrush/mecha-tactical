using System.Collections.Generic;
using UnityEngine;

public static class AStar {
    public static List<Vector2> GeneratePath(int[,] map, float[,] noise, Vector2 start, Vector2 goal) {

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

    // DOUBLE IN RiverGeneration.cs
    private static List<Vector2> GetNeighbours(int[,] map, Vector2 cell) {
        List<Vector2> neighbours = new List<Vector2>();

        if (cell.x - 1 >= 0) neighbours.Add(new Vector2(cell.x - 1, cell.y));
        if (cell.x + 1 < map.GetLength(1)) neighbours.Add(new Vector2(cell.x + 1, cell.y));
        if (cell.y - 1 >= 0) neighbours.Add(new Vector2(cell.x, cell.y - 1));
        if (cell.y + 1 < map.GetLength(0)) neighbours.Add(new Vector2(cell.x, cell.y + 1));

        return neighbours;
    }

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
        public float GetF() { return g + h; }
    }
}
