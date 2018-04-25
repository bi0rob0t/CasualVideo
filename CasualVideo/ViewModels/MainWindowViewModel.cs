using CasualVideo.Models;
using CasualVideo.Views;
using DevExpress.Mvvm;
using System.Windows.Input;

namespace CasualVideo.ViewModels
{
    class MainWindowViewModel : BaseVM
    {
        public ICommand OpenHelp
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    HelpWindow helpWindow = new HelpWindow();
                    HelpWindowViewModel vm = new HelpWindowViewModel { };
                    helpWindow.DataContext = vm;
                    helpWindow.Show();
                });

            }
        }
    }
}