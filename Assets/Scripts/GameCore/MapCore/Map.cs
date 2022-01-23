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

    public int[,] GetCellGridPart(int xStart, int yStart, int xEnd, int yEnd) {
        if (xStart >= xEnd || yStart >= yEnd) throw new Exception("Map.cs - GetCellGridPart() : xStart / yStart cannot be greater than xEnd / yEnd");
        if (xStart < 0 || yStart < 0 || xEnd >= _cellGrid.GetLength(1) || yEnd >= _cellGrid.GetLength(0)) throw new Exception("Map.cs - GetCellGridPart() : Trying to get an out of bond part");

        int[,] cellGridPart = new int[yEnd - yStart + 1, xEnd - xStart + 1];
        for (int y = 0; y <= yEnd - yStart; y++) {
            for (int x = 0; x <= xEnd - xStart; x++) {
                cellGridPart[y, x] = _cellGrid[yStart + y, xStart + x];
            }
        }

        return cellGridPart;
    }

    public int[,] GetCellGrid() { return _cellGrid; }

}
