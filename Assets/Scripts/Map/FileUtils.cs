using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileUtils
{
    public static int[,] ReadLevelFromFile(string name)
    {
        List<string> fileLines = new List<string>();

        StreamReader reader = new StreamReader("Assets/Resources/Levels/" + name + ".txt");
        string currentLine;
        while ((currentLine = reader.ReadLine()) != null)
        {
            fileLines.Add(currentLine);
            // Debug.Log(currentLine);
        }

        if (fileLines.Count < 10) throw new System.Exception("Level is invalid : The height must at least be 10");
        if (fileLines[0].Length < 10) throw new System.Exception("Level is invalid : The width must at least be 10");

        int[,] mapData = new int[fileLines.Count, fileLines[0].Length];
        for (int y = 0; y < mapData.GetLength(0); y++)
        {
            for (int x = 0; x < mapData.GetLength(1); x++)
            {
                mapData[y, x] = fileLines[y][x] - '0';
            }
        }

        return mapData;
    }
}
