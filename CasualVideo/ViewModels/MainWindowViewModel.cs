using CasualVideo.Models;
using CasualVideo.Views;
using DevExpress.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Threading;

namespace CasualVideo.ViewModels
{
    class MainWindowViewModel : BaseVM
    {
        public ListBoxItem Test { get; set; }
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timerUpdateFiles = new DispatcherTimer();
        private double maxTime;
        private double _time = 0;
        public double Time
        {
            get { return _time; }
            set { _time = value; RaisePropertyChanged("Time"); }
        }
        private double _startTime = 0;
        public double StartTime
        {
            get { return _startTime; }
            set { _startTime = value; RaisePropertyChanged("StartTime"); }
        }
        private double _endTime = 0;
        public double EndTime
        {
            get { return _endTime; }
            set { _endTime = value; RaisePropertyChanged("EndTime"); }
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



        public DelegateCommand<MediaElement> Play
        {
            get
            {

                return new DelegateCommand<MediaElement>((me) =>
                {
                    
                    me.Play();
                    Thread.Sleep(1000);
                    Time += 0.1;
                    timer.Start();
                    maxTime = me.NaturalDuration.TimeSpan.TotalSeconds;
                    
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

                    MessageBox.Show(Test.Content + "");


                    me.Pause();
                    timer.Stop();
                });

            }
        }

        public ICommand ConvertToMp4
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    RenderService cs = new RenderService();
                    cs.ToMp4(Filename);

                });

            }
        }
        public ICommand ConvertToGif
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    RenderService cs = new RenderService();
                    cs.ToGif(Filename, 100);

                });

            }
        }
        public ICommand ConvertToWebM
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    RenderService cs = new RenderService();
                    cs.ToWebM(Filename);

                });

            }
        }
        public ICommand ConvertToOgv
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    RenderService cs = new RenderService();
                    cs.ToOgv(Filename);

                });

            }
        }
        public ICommand ConvertToTs
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    
                    RenderService cs = new RenderService();                    
                    cs.ToTs(Filename);

                });

            }
        }
        public ICommand Split
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    RenderService cs = new RenderService();
                    cs.Split(Filename, StartTime, EndTime, maxTime);


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
                    UpdateFiles(currentPath);

                    timer.Interval = TimeSpan.FromSeconds(0.1);
                    timer.Tick += new EventHandler(timer_Tick);
                    
                });

            }
        }


        public ICommand Close
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    Application.Current.Shutdown();

                });

            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {        
            if(Time < maxTime)
                Time += 0.1;
        }
        public ICommand OpenHelp
        {
            get
            {

                return new DelegateCommand(() =>
                {                    
                    HelpWindow helpWindow = new HelpWindow();
                    HelpWindowViewModel vm = new HelpWindowViewModel { };
                    helpWindow.DataContext = vm;
                    helpWindow.ShowDialog();
                });

            }
        }

        public ICommand OpenSettings
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    SettingsWindow settingsWindow = new SettingsWindow();
                    SettingsWindowViewModel vm = new SettingsWindowViewModel { };
                    settingsWindow.DataContext = vm;
                    settingsWindow.ShowDialog();
                });

            }
        }




    }
}



