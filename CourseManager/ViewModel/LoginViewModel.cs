using Course.Common;
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
        public CommandBase CloseWindowCmd { get; set; }

        public LoginViewModel()
        {
            this.CloseWindowCmd = new CommandBase();
            this.CloseWindowCmd.DoExecute = new Action<object>((o) =>
            {
                (o as Window).Close();
            });

            this.CloseWindowCmd.DoCanExecute = new Func<object, bool>((o)=> { return true; });
        }
    }
}
