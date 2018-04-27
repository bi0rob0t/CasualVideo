using CasualVideo.Models;
using DevExpress.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CasualVideo.ViewModels
{
    class SettingsWindowViewModel : BaseVM
    {
        public SettingsWindowViewModel()
        {
            string[] tmp = Settings.ParseFile();
            OutPath = tmp[0];
            RegPath = tmp[1];
        }
        private string _outPath = "";
        public string OutPath
        {
            get { return _outPath; }
            set { _outPath = value; RaisePropertyChanged("OutPath"); }
        }
        private string _regPath = "";
        public string RegPath
        {
            get { return _regPath; }
            set { _regPath = value; RaisePropertyChanged("RegPath"); }
        }

        
        public ICommand ChangeOutPath
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    Nullable<bool> result = ofd.ShowDialog();
                    if (result == true)
                    {
                        OutPath = ofd.FileName;
                        OutPath = OutPath.Substring(0, OutPath.LastIndexOf('\\') + 1);
                    }
                    Settings.RewriteFile(OutPath, RegPath);
                });

            }
        }
        public ICommand ChangeRegPath
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    Nullable<bool> result = ofd.ShowDialog();
                    if (result == true)
                    {
                        RegPath = ofd.FileName;
                        
                    }
                });

            }
        }
    }
}
