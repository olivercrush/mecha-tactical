using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDebugCell : MonoBehaviour
{
    public CellType _cellType;

    private void Start()
    {

    }

    private void OnMouseEnter()
    {
        SetCellColor(new Color(1, 0, 1));
        ObjectFinder.GetUIManager().SetCellTypeText(_cellType.GetName());
    }

    private void OnMouseExit()
    {
        SetCellColor(_cellType.GetDebugColor());
        ObjectFinder.GetUIManager().SetCellTypeText("");
    }

    private void SetCellColor(Color cellColor)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = cellColor;
    }

    public void Activate(CellType type)
    {
        _cellType = type;
        SetCellColor(_cellType.GetDebugColor());
    }
}
