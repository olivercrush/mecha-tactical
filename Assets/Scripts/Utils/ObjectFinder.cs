using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectFinder
{
    public static UIManager GetUIManager() { return GameObject.Find("UI").GetComponent<UIManager>(); }
    public static Cursor GetCursor() { return GameObject.Find("Cursor").GetComponent<Cursor>(); }
    public static Selector GetSelector() { return GameObject.Find("Selector").GetComponent<Selector>(); }
    public static MapGenerator GetMapGenerator() { return GameObject.Find("MapGenerator").GetComponent<MapGenerator>(); }
}
