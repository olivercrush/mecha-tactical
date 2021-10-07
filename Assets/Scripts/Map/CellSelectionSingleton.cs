using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelectionSingleton
{
    // SINGLETON PART
    private CellSelectionSingleton() { _currentSelectedCell = null; }

    private static CellSelectionSingleton _instance;

    public static CellSelectionSingleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new CellSelectionSingleton();
        }
        return _instance;
    }

    // LOGIC PART
    private Cell _currentSelectedCell;

    public void SelectCell(Cell cell)
    {
        DeselectCell();
        if (_currentSelectedCell != cell)
        {
            _currentSelectedCell = cell;
            ObjectFinder.GetUIManager().SetCellTypeText(_currentSelectedCell.GetCellType().GetName());
        }
    }

    public void DeselectCell()
    {
        if (_currentSelectedCell != null)
        {
            _currentSelectedCell.DeselectThisCell();
        }
        ObjectFinder.GetUIManager().SetCellTypeText("");
    }

    public Cell GetCurrentSelectedCell() { return _currentSelectedCell; }
}
