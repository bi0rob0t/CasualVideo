
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
            return File.ReadAllLines(pathSettings);
        }
        public static void RewriteFile(string output, string reg)
        {
            using (StreamWriter sw = new StreamWriter(pathSettings))
            {
                sw.WriteLine(output);
                sw.WriteLine(reg);
            }
        }
    }
}