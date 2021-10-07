using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectFinder
{
    public static UIManager GetUIManager() { return GameObject.Find("UI").GetComponent<UIManager>(); }
}
