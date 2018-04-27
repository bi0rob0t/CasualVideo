using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CasualVideo.Models;
using CasualVideo.ViewModels;
using CasualVideo.Views;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Xabe.FFmpeg.Model;
using System.IO;
using CasualVideo.Properties;

namespace CasualVideo.ViewModels
{
    class StartWindowViewModel : BaseVM
    {
        private bool _isAutorized;
        public bool isAutorized
        {
            get { return _isAutorized; }
            set { _isAutorized = value; RaisePropertyChanged("isAutorized"); }
        }
        public string Login { get; set; }
        public string Password { get; set; }


        public DelegateCommand<Window> CreateProject
        {
            get
            {

                return new DelegateCommand<Window>((w) =>
                {
                    
                    MainWindow mainWindow = new MainWindow();
                    MainWindowViewModel vm = new MainWindowViewModel ();
                    mainWindow.DataContext = vm;
                    w.Close();
                    mainWindow.Show();


                });

            }
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
        public ICommand OpenRegister
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    RegisterWindow registerWindow = new RegisterWindow();
                    RegisterWindowViewModel vm = new RegisterWindowViewModel { };
                    registerWindow.DataContext = vm;
                    registerWindow.ShowDialog();
                });

            }
        }

        public ICommand CheckAuthorization
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    Auth auth = new Auth();
                    if (auth.AuthProcess(Login, Password))
                    {
                        isAutorized = true;
                    }
                    else
                    {
                        isAutorized = false;
                        MessageBox.Show("Логин или пароль введены неверно");
                    }
                });

            }
        }
        

    }
}
