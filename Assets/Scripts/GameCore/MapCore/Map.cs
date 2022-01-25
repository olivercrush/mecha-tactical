using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Map {

    private int[,] _cellGrid;

    public Map(int[,] cellGrid) {
        _cellGrid = cellGrid;
    }

    public int[,] GetCellGridPart(int x1, int y1, int x2, int y2) {
        if (x1 >= x2 || y1 >= y2) throw new Exception("Map.cs - GetCellGridPart() : xStart / yStart cannot be greater than xEnd / yEnd");
        if (x1 < 0 || y1 < 0 || x2 >= _cellGrid.GetLength(1) || y2 >= _cellGrid.GetLength(0)) throw new Exception("Map.cs - GetCellGridPart() : Trying to get an out of bond part");

        int[,] cellGridPart = new int[y2 - y1, x2 - x1];
        for (int y = 0; y < cellGridPart.GetLength(0); y++) {
            for (int x = 0; x < cellGridPart.GetLength(1); x++) {
                cellGridPart[y, x] = _cellGrid[y1 + y, x1 + x];
            }
        }

        return cellGridPart;
    }

    public int[,] GetCellGrid() { return _cellGrid; }

}
