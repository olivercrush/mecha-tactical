using System.Collections;
using UnityEngine;

public class GraphicsManager : MonoBehaviour {

    public GameCore _gameCore;
    private CoreUpdateReceiver _updateReceiver;

    private MapGraphics _mapGraphics;

    public Vector2 _mapDimensions;

    private void Start() {
        _updateReceiver = new CoreUpdateReceiver(_gameCore);
        _updateReceiver.ReceiveGameUpdate(CoreUpdateFactory.CreateGenerateMapUpdate((int) _mapDimensions.x, (int) _mapDimensions.y));

        GameObject mapGraphicsGameObject = new GameObject("MapGraphics");
        mapGraphicsGameObject.transform.SetParent(transform);
        _mapGraphics = mapGraphicsGameObject.AddComponent<MapGraphics>();
        _mapGraphics.Load();
    }
}