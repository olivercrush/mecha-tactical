using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    private MapCore _mapCore;

    private void Start() {
        _mapCore = new MapCore();
    }
}
