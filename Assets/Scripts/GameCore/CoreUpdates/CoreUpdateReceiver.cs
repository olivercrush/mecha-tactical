using UnityEditor;
using UnityEngine;

public class CoreUpdateReceiver {

    GameCore _gameCore;
    
    public CoreUpdateReceiver(GameCore gameCore) {
        _gameCore = gameCore;
    } 

    public void ReceiveGameUpdate(Update update) {
        _gameCore.PropagateUpdate(update);
    }
}