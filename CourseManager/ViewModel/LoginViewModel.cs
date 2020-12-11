using Course.Common;
using Course.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    class LoginViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand<object> CloseWindowCmd { get; set; }

        /// <summary>
        /// 继承自 ViewModelBase, 后台数据发生变化时通知前端
        /// </summary>
        public LoginModel LoginModel { get; set; }

        /// <summary>
        /// 登录命令
        /// </summary>
        public RelayCommand<object> LoginCommand { get; set; }

        /// <summary>
        /// 登录错误提示
        /// </summary>
        public ErrorMessage LoginErrorMsg { get; set; }

        public LoginViewModel()
        {
            /// Close Login Window
            CloseWindowCmd = new RelayCommand<object>(CloseLoginWindow);

            /// Bind Login User Info: name, password, validate
            LoginModel = new LoginModel();
            LoginModel.UserName = "Shledon";
            LoginModel.Password = "sadfasdf";
            LoginModel.ValidateCode = "JgYU";

            LoginCommand = new RelayCommand<object>(LoginMainWindow);

            LoginErrorMsg = new ErrorMessage();
        }

        private void CloseLoginWindow(object o)
        {
            (o as Window).Close();
        }

        private void LoginMainWindow(object o)
        {
            LoginErrorMsg.Msg = "";

            if(string.IsNullOrEmpty(LoginModel.UserName))
            {
                LoginErrorMsg.Msg = "Please input the user name!";
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.Password))
            {
                LoginErrorMsg.Msg = "Please input the password!";
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.ValidateCode))
            {
                LoginErrorMsg.Msg = "Please input the validate code!";
                return;
            }

        }
    }
}
