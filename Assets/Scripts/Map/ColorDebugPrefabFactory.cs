using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorDebugPrefabFactory
{
    public static GameObject CreateMapCell(int type, Vector3 position, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/ColorDebugMapCell");

        CellType cellType;
        switch (type)
        {
            case 0:
                cellType = new CellPlain();
                break;

            case 2:
                cellType = new CellForest();
                break;

            case 3:
                cellType = new CellWater();
                break;

            default:
                cellType = new CellPlain();
                break;
        }

        GameObject cell = GameObject.Instantiate(prefab, position, Quaternion.identity, parent);
        cell.GetComponent<MapDebugCell>().Activate(cellType);
        return cell;
    }
}
