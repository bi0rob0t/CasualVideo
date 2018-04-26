using CasualVideo.Models;
using CasualVideo.Views;
using DevExpress.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CasualVideo.ViewModels
{
    class MainWindowViewModel : BaseVM
    {
        DispatcherTimer timer = new DispatcherTimer();
        
        private double _time = 0;
        public double Time
        {
            get { return _time; }
            set { _time = value; RaisePropertyChanged("Time"); }
        }


        public string currentPath = @"D:\";
        
        private string _filename = "";
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; RaisePropertyChanged("Filename"); }
        }
        public MainWindowViewModel()
        {
            
            //UpdateFiles(currentPath);
 


        }
        private double _volume = 1;
        public double Volume
        {
            get { return _volume; }
            set { _volume = value; RaisePropertyChanged("Volume"); }
        }
        

        private ObservableCollection<string> _files = new ObservableCollection<string>();        
        public ObservableCollection<string> Files
        {
            get { return _files; }
            set { _files = value; RaisePropertyChanged("Files"); }
        }
        private void UpdateFiles(string path)
        {
            Files.Clear();
            var test = Directory.GetFiles(path);

            foreach (var t in test)
                Files.Add(t.Substring(t.LastIndexOf('\\') + 1));

        }

        
        public DelegateCommand MediaEnded()
        {
            
                return new DelegateCommand(() =>
                {
                    var tt = new FileInfo(Filename);
                    
                    
                    Time = 0;
                    timer.Stop();
                });

            
        }

        public DelegateCommand<MediaElement> Play
        {
            get
            {

                return new DelegateCommand<MediaElement>((me) =>
                {
                    Xabe.FFmpeg.Conversion.ToWebM(Filename, @"D:\out1").Start();
                    me.Play();
                    timer.Start();
                });

            }
        }
        public DelegateCommand<MediaElement> Stop
        {
            get
            {

                return new DelegateCommand<MediaElement>((me) =>
                {
                    me.Stop();
                    timer.Stop();
                    Time = 0;
                });

            }
        }
        public DelegateCommand<MediaElement> Pause
        {
            get
            {

                return new DelegateCommand<MediaElement>((me) =>
                {
                    me.Pause();
                    timer.Stop();
                });

            }
        }



        public ICommand OpenFile
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    
                    OpenFileDialog ofd = new OpenFileDialog();
                    Nullable<bool> result = ofd.ShowDialog();
                    if (result == true)
                    {
                        currentPath = ofd.FileName;
                    }
                    Filename = ofd.FileName;
                    currentPath = currentPath.Substring(0, currentPath.LastIndexOf('\\') + 1);
                    MessageBox.Show(Filename);
                    UpdateFiles(currentPath);

                    timer.Interval = TimeSpan.FromSeconds(0.1);
                    timer.Tick += new EventHandler(timer_Tick);
                    
                });

            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {           
            Time += 0.1;
        }
        public ICommand OpenHelp
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    UpdateFiles(@"D:\");
                    HelpWindow helpWindow = new HelpWindow();
                    HelpWindowViewModel vm = new HelpWindowViewModel { };
                    helpWindow.DataContext = vm;
                    helpWindow.Show();
                });

            }
        }

        public ICommand ConvertTest
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    Xabe.FFmpeg.Conversion.ToMp4(Filename, @"D:\testResult.mp4").Start();
                });

            }
        }
        private async void test()
        {
            await Xabe.FFmpeg.Conversion.ToMp4(Filename, @"D:\testResult.mp4" ).Start();
        }

    }
}



