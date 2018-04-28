using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CasualVideo.Models
{
    class Auth
    {
        private string path = Settings.ParseFile()[1];

        public bool AuthProcess(string login, string password)
        {
            if (login == null || password == null)
                return false;
            Dictionary<string, string> logins = ParseFile();
            if (logins.ContainsKey(login))
                if (logins[login] == password)
                    return true;
            return false;
            
        }
        private Dictionary<string,string> ParseFile()
        {
            List<string> stringsOfFile = new List<string>();
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        stringsOfFile.Add(s);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            Dictionary<string, string> logins = new Dictionary<string, string>();
            foreach (string s in stringsOfFile)
            {
                
                string[] currentString = new string[2];
                currentString = s.Split(';');                
                logins.Add(currentString[0], currentString[1]);                
            }
            return logins;
        }
        public void Register(string login, string password)
        {            
            string output = login + ";" + password;
            try
            {
                var tmp = ParseFile();
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Вы уже зарегистрированы");
                return;
            }
            
                
                
            using (StreamWriter sr = new StreamWriter(path, true))
            {
                sr.WriteLine(output);
            }
        }
    }
}
