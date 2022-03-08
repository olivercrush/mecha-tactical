using System.Collections;
using UnityEngine;

public class GraphicsManager : MonoBehaviour {

    public GameCore _gameCore;
    private CoreUpdateReceiver _coreUpdateReceiver;

    private MapGraphics _mapGraphics;

    public Vector2 _mapDimensions;

    private void Start() {
        _coreUpdateReceiver = new CoreUpdateReceiver(_gameCore);
        _coreUpdateReceiver.ReceiveGameUpdate(CoreUpdateFactory.CreateGenerateMapUpdate((int) _mapDimensions.x, (int) _mapDimensions.y));
        _coreUpdateReceiver.ReceiveGameUpdate(CoreUpdateFactory.CreateSpawnBuildingUpdate(10, 10, 0, 2));

        GameObject mapGraphicsGameObject = new GameObject("MapGraphics");
        mapGraphicsGameObject.transform.SetParent(transform);
        _mapGraphics = mapGraphicsGameObject.AddComponent<MapGraphics>();
        _mapGraphics.Load();
    }
}