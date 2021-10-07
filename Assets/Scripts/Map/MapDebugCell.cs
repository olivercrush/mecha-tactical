using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDebugCell : Cell
{
    private void OnMouseEnter()
    {
        if (CellSelectionSingleton.GetInstance().GetCurrentSelectedCell() != this)
        {
            SetCellColor(new Color(1, 0, 1));
        }
    }

    private void OnMouseExit()
    {
        if (CellSelectionSingleton.GetInstance().GetCurrentSelectedCell() != this)
        {
            SetCellColor(_type.GetDebugColor());
        }
    }

    private void OnMouseDown()
    {
        SelectThisCell();
    }

    public override void Activate(CellType type)
    {
        _type = type;
        SetCellColor(_type.GetDebugColor());
    }

    protected override void SelectThisCell()
    {
        SetCellColor(new Color(0, 1, 1));
        CellSelectionSingleton.GetInstance().SelectCell(this);
    }

    public override void DeselectThisCell()
    {
        SetCellColor(_type.GetDebugColor());
    }

    private void SetCellColor(Color cellColor)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = cellColor;
    }

    
}
