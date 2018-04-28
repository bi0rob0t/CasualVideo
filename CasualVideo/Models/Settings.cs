
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CasualVideo.Models
{
    class Settings
    {
        private static string pathSettings = @"settings.ini";
        public static string[] ParseFile()
        {
            try
            {
                string[] tmp = File.ReadAllLines(pathSettings);
                if (tmp.Length < 2)
                    throw new IndexOutOfRangeException();
                return tmp;
            }
            catch
            {
                using (StreamWriter sw = new StreamWriter(pathSettings))
                {
                    sw.WriteLine();
                    sw.WriteLine();
                }
                return new string[2];
            }
        }
        public static void RewriteFile(string output, string reg)
        {
            using (StreamWriter sw = new StreamWriter(pathSettings))
            {
                if (output.Length != 0)
                {
                    if (output[output.Length - 1] != '\\')
                        sw.WriteLine(output + '\\');
                    else
                        sw.WriteLine(output);
                }


                sw.WriteLine(reg);

            }
        }
    }
}