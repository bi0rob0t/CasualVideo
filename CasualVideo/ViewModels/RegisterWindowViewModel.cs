using CasualVideo.Models;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CasualVideo.ViewModels
{
    class RegisterWindowViewModel : BaseVM
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        public DelegateCommand<Window> Registration
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    Auth reg = new Auth();
                    reg.Register(Login, Password);
                    w?.Close();
                });
            }
        }

    }
}
