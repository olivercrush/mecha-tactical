using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGraphicsPrefabFactory {
    // SINGLETON PART
    private CellGraphicsPrefabFactory() {
        _cellPlain = ScriptableObject.CreateInstance<CellTypePlain>();
        _cellForest = ScriptableObject.CreateInstance<CellTypeForest>();
        _cellWater = ScriptableObject.CreateInstance<CellTypeWater>();
    }

    private static CellGraphicsPrefabFactory _instance;

    public static CellGraphicsPrefabFactory GetInstance() {
        if (_instance == null) {
            _instance = new CellGraphicsPrefabFactory();
        }
        return _instance;
    }

    // LOGIC PART
    private CellTypePlain _cellPlain;
    private CellTypeForest _cellForest;
    private CellTypeWater _cellWater;

    public GameObject CreateMapCell(Transform parent) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/MapCell");
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
