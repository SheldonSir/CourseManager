using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Model
{
    public class LoginModel : ViewModelBase
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        private string passWord;

        public string Password
        {
            get { return passWord; }
            set {
                passWord = value;
                RaisePropertyChanged();
            }
        }

        private string validateCode;

        public string ValidateCode
        {
            get { return validateCode; }
            set { validateCode = value; RaisePropertyChanged(); }
        }
    }

    public class ErrorMessage : ViewModelBase
    {
        private string msg;

        public string Msg
        {
            get { return msg; }
            set { msg = value; RaisePropertyChanged(); }
        }
    }
}
