using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorDebugPrefabFactory
{
    public static GameObject GetMapCell(int type)
    {
        GameObject cell = Resources.Load<GameObject>("Prefabs/ColorDebugMapCell");

        Color cellColor;
        switch (type)
        {
            case 0:
                cellColor = new Color(1, 1, 1);
                break;

            case 1:
                cellColor = new Color(1, 0, 0);
                break;

            case 2:
                cellColor = new Color(0, 1, 0);
                break;

            case 3:
                cellColor = new Color(0, 0, 1);
                break;

            default:
                cellColor = new Color(0, 0, 0);
                break;
        }

        SpriteRenderer sr = cell.GetComponent<SpriteRenderer>();
        sr.color = cellColor;

        return cell;
    }
}
