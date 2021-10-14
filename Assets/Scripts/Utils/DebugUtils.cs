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

    public static void DumpString(string str) {
        string path = "Logs/string_dump.txt";

        using (StreamWriter sw = File.CreateText(path)) {
            sw.WriteLine("======================= STRING DUMP / " + DateTime.Now.ToString() + " =======================");
            sw.WriteLine(str);
        }
    }

    public static void DumpStringList(List<string> a) {
        string path = "Logs/string_list_dump.txt";

        using (StreamWriter sw = File.CreateText(path)) {
            sw.WriteLine("======================= STRING ARRAY DUMP / " + DateTime.Now.ToString() + " =======================");
            foreach (string str in a) {
                sw.WriteLine(str);
            }
        }
    }
}
