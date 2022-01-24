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

    public GameObject CreateMapCell(Transform parent, DisplayType displayType) {
        GameObject prefab;
        if (displayType == DisplayType.DEBUG) prefab = Resources.Load<GameObject>("Prefabs/ColorDebugMapCell");
        else prefab = Resources.Load<GameObject>("Prefabs/MapCell");

        GameObject cell = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, parent);
        return cell;
    }

    public CellType GetCellTypeInstance(int cellType) {
        switch (cellType) {
            case 0:
                return _cellPlain;

            case 2:
                return _cellForest;

            case 3:
                return _cellWater;

            default:
                return _cellPlain;
        }
    }
}
