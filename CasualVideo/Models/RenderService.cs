using CasualVideo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Xabe.FFmpeg.Model;

namespace CasualVideo.Models
{
    class RenderService : ILog
    {
        public string pathLog { get; set; }
        private string currentOutput = Settings.ParseFile()[0];
        private void SetOutputPath()
        {
            currentOutput = Settings.ParseFile()[0];         
        }
        public async  void ToMp4(string input)
        {
            SetOutputPath();
            await Xabe.FFmpeg.Conversion.ToMp4(input, currentOutput + GetName(input) + ".mp4").Start();
            addAction(String.Format("{0} - {1} - Конвертация в {1}", DateTime.Now, input, "mp4"));
            MessageBox.Show("Конвертация в mp4 завершена");
        }
        public  void ToOgv(string input)
        {
            SetOutputPath();
            Xabe.FFmpeg.Conversion.ToOgv(input, currentOutput + GetName(input) + ".ogv").Start();
            addAction(String.Format("{0} - {1} - Конвертация в {1}", DateTime.Now, input, "ogv"));
            MessageBox.Show("Конвертация в ogv завершена");
        }
        public  void ToTs(string input)
        {
            SetOutputPath();
            Xabe.FFmpeg.Conversion.ToTs(input, currentOutput + GetName(input) + ".ts").Start();
            addAction(String.Format("{0} - {1} - Конвертация в {1}", DateTime.Now, input, "ts"));
            MessageBox.Show("Конвертация в ts завершена");
        }
        public  void ToWebM(string input)
        {
            SetOutputPath();            
            Xabe.FFmpeg.Conversion.ToWebM(input, currentOutput +  GetName(input) + ".webm").Start();
            addAction(String.Format("{0} - {1} - Конвертация в {1}", DateTime.Now, input, "webm"));
            MessageBox.Show("Конвертация в webm завершена");
        }
        public void ToGif(string input, int countLoops)
        {
            SetOutputPath();
            Xabe.FFmpeg.Conversion.ToGif(input, currentOutput + GetName(input) + ".gif", countLoops).Start();
            addAction(String.Format("{0} - {1} - Конвертация в {1}", DateTime.Now, input, "gif"));
            MessageBox.Show("Конвертация в gif завершена");
        }

        public void Split(string input, double startTime, double endTime, double duration)
        {
            SetOutputPath();
            if(endTime > 0)
            {
                Xabe.FFmpeg.Conversion.Split(input, currentOutput + "(s)" + input.Substring(input.LastIndexOf('\\') + 1), TimeSpan.FromSeconds(startTime), TimeSpan.FromSeconds(endTime)).Start();
                addAction(String.Format("{0} - {1} - Разделение видео от {2} с продолжительностью {3}", DateTime.Now, input, startTime, endTime));
            }
            else
            {
                Xabe.FFmpeg.Conversion.Split(input, currentOutput + "(s)" + input.Substring(input.LastIndexOf('\\') + 1), TimeSpan.FromSeconds(startTime), TimeSpan.FromSeconds(duration - startTime)).Start();
                addAction(String.Format("{0} - {1} - Разделение видео от {2} с продолжительностью {3}", DateTime.Now, input, startTime, duration - startTime));
            }


            
            
        }

        public async void Concatenate(string Filename, string SelectedListItem)
        {
            SetOutputPath();
            //MessageBox.Show(Filename);
           // MessageBox.Show(Filename.Substring(0, Filename.LastIndexOf('\\') + 1)  + SelectedListItem);

            var result = await Xabe.FFmpeg.Conversion.Concatenate(currentOutput + "out.mp4", Filename, Filename.Substring(0, Filename.LastIndexOf('\\') + 1) + SelectedListItem);
        }


        private string GetName(string _filename)
        {
            string filename = _filename.Substring(_filename.LastIndexOf('\\') + 1);
            return filename.Substring(0, filename.LastIndexOf('.'));
        }
        public void addAction(string action)
        {
            pathLog = @"log.txt";
            using (StreamWriter sw = new StreamWriter(pathLog, true))
            {
                sw.WriteLine(action) ;
            }
        }
    }
}