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
                cellType = ScriptableObject.CreateInstance<CellPlain>();
                break;

            case 2:
                cellType = ScriptableObject.CreateInstance<CellForest>();
                break;

            case 3:
                cellType = ScriptableObject.CreateInstance<CellWater>();
                break;

            default:
                cellType = ScriptableObject.CreateInstance<CellPlain>();
                break;
        }

        GameObject cell = GameObject.Instantiate(prefab, position, Quaternion.identity, parent);
        cell.GetComponent<MapDebugCell>().Activate(cellType);
        return cell;
    }
}
