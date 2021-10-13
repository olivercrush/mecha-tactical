using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DebugUtils {

    public static void DumpFloat2DArray(float[,] a, string name) {
        string path = "Logs/2d_dump_" + name + ".txt";

        using (StreamWriter sw = File.CreateText(path)) {
            sw.WriteLine("======================= " + name + " / " + DateTime.Now.ToString() + " =======================");

            for (int y = 0; y < a.GetLength(0); y++) {
                string line = "";
                for (int x = 0; x < a.GetLength(1); x++) {
                    line += a[y, x].ToString("0.00") + " ";
                }
                sw.WriteLine(line);
            }

            sw.WriteLine("\n\n");
        }
    }

}
