using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Model
{
    // 这里既然是Model层, 为啥要从ViewModelBase 集成呢
    // 可能 ViewModelBase 只是一个名称而已
    // 真正要的是实现 通知到前端 的这个功能
    // ViewModel and Model 都可以有这个通知属性
    public class UserModel : ViewModelBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set
            {
                avatar = value;
                RaisePropertyChanged();
            }
        }

        private int gender;

        public int Gender
        {
            get { return gender; }
            set
            {
                gender = value; RaisePropertyChanged();
            }
        }



    }
}
