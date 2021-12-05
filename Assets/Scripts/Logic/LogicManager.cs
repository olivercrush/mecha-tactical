using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public Map _map;
    private GameObject _building;

    private void Start() {
        _map.Load();
        _building = EntityFactory.GetInstance().CreateDebugBuilding(new Vector2(0, 0), PlayerColor.RED);
    }
}
