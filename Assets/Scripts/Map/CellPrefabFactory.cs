using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPrefabFactory {
    // SINGLETON PART
    private CellPrefabFactory() {
        _cellPlain = ScriptableObject.CreateInstance<CellPlain>();
        _cellForest = ScriptableObject.CreateInstance<CellForest>();
        _cellWater = ScriptableObject.CreateInstance<CellWater>();
    }

    private static CellPrefabFactory _instance;

    public static CellPrefabFactory GetInstance() {
        if (_instance == null) {
            _instance = new CellPrefabFactory();
        }
        return _instance;
    }

    // LOGIC PART
    private CellPlain _cellPlain;
    private CellForest _cellForest;
    private CellWater _cellWater;

    public GameObject CreateMapCell(int type, Vector3 position, Transform parent, DisplayType displayType) {
        GameObject prefab;
        if (displayType == DisplayType.DEBUG) prefab = Resources.Load<GameObject>("Prefabs/ColorDebugMapCell");
        else prefab = Resources.Load<GameObject>("Prefabs/MapCell");

        GameObject cell = GameObject.Instantiate(prefab, position, Quaternion.identity, parent);

        switch (type) {
            case 0:
                cell.GetComponent<Cell>().Activate(_cellPlain);
                break;

            case 2:
                cell.GetComponent<Cell>().Activate(_cellForest);
                break;

            case 3:
                cell.GetComponent<Cell>().Activate(_cellWater);
                break;

            default:
                cell.GetComponent<Cell>().Activate(_cellPlain);
                break;
        }

        return cell;
    }
}
