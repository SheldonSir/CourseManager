using Course.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        public RelayCommand<object> CloseMainWindowCmd { get; set; }

        public RelayCommand<object> MinimizedMainWindowCmd { get; set; }

        public RelayCommand<object> MaximizedMainWindowCmd { get; set; }

        public UserModel UserInfo { get; set; }

        private string search;

        public string SearchText
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private FrameworkElement content;

        public FrameworkElement MainContent
        {
            get { return content; }
            set
            {
                content = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<object> NavChangedCommand { get; set; }

        public MainViewModel()
        {
            CloseMainWindowCmd = new RelayCommand<object>((o) => { (o as Window).Close(); });
            MinimizedMainWindowCmd = new RelayCommand<object>((o) => { (o as Window).WindowState = WindowState.Minimized; });
            MaximizedMainWindowCmd = new RelayCommand<object>((o)=> {
                Window w = o as Window;
                w.WindowState = (w.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
            });

            SearchText = "";
            UserInfo = new UserModel();
            UserInfo.Name = "Strongly Sheldon";
            UserInfo.Gender = 1;
            UserInfo.Avatar = "../Assets/images/sheldon.jpg";

            NavChangedCommand = new RelayCommand<object>(OnNavgationChanged);

            OnNavgationChanged((object)"MainPageView");
        }

        private void OnNavgationChanged(object o)
        {
            Type type = Type.GetType("Course.View." + o.ToString());
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);

            MainContent = (FrameworkElement)ctor.Invoke(null);
        }
    }
}
