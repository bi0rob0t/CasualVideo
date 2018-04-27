using CasualVideo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace CasualVideo.Models
{
    class ConvertService : ILog
    {
        private string currentOutput = "";
        private void SetOutputPath()
        {
            string[] lines = Settings.ParseFile();
            currentOutput = lines[0];
        }
        public  void ToMp4(string input, string output)
        {
            SetOutputPath();
            Xabe.FFmpeg.Conversion.ToMp4(input, currentOutput + GetName(input) + ".mp4").Start();            
            addAction();
            MessageBox.Show("Конвертация в mp4 завершена");
        }
        public  void ToOgv(string input, string output)
        {
            SetOutputPath();
            Xabe.FFmpeg.Conversion.ToOgv(input, currentOutput + GetName(input) + ".ogv").Start();
            addAction();
            MessageBox.Show("Конвертация в ogv завершена");
        }
        public  void ToTs(string input, string output)
        {
            SetOutputPath();
            Xabe.FFmpeg.Conversion.ToTs(input, currentOutput + GetName(input) + ".ts").Start();
            addAction();
            MessageBox.Show("Конвертация в ts завершена");
        }
        public  void ToWebM(string input)
        {
            SetOutputPath();            
            Xabe.FFmpeg.Conversion.ToWebM(input, currentOutput +  GetName(input) + ".webm").Start();
            addAction();
            MessageBox.Show("Конвертация в webm завершена");
        }
        public void ToGif(string input, string output, int countLoops)
        {
            SetOutputPath();
            Xabe.FFmpeg.Conversion.ToGif(input, currentOutput + GetName(input) + ".gif", countLoops).Start();
            addAction();
            MessageBox.Show("Конвертация в gif завершена");
        }
        private string GetName(string _filename)
        {
            string filename = _filename.Substring(_filename.LastIndexOf('\\') + 1);
            return filename.Substring(0, filename.LastIndexOf('.'));
        }
        public void addAction()
        {
            DateTime currentTime = DateTime.Now;
        }
    }
}