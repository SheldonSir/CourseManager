using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Course.Common
{
    public class PasswordHelper
    {
        static bool isUpdating = false;

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper), new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Model 数据发生变化时, 更新到View
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordbox = d as PasswordBox;
            passwordbox.PasswordChanged -= Passwordbox_PasswordChanged;
            if(!isUpdating)
                passwordbox.Password = e.NewValue?.ToString();
            passwordbox.PasswordChanged += Passwordbox_PasswordChanged;
        }

        public static string GetPassword(DependencyObject d)
        {
            return d.GetValue(PasswordProperty).ToString();
        }

        public static void SetPassword(DependencyObject d, string v)
        {
            d.SetValue(PasswordProperty, v);
        }


        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper), new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));

        private static void OnAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordbox = d as PasswordBox;
            passwordbox.PasswordChanged += Passwordbox_PasswordChanged;
        }

        public static bool GetAttach(DependencyObject d)
        {
            return (bool)d.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject d, bool v)
        {
            d.SetValue(AttachProperty, v);
        }

        /// <summary>
        /// View发生变化时, 更新到 Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordbox = sender as PasswordBox;

            isUpdating = true;
            SetPassword(passwordbox, passwordbox.Password);
            isUpdating = false;
        }
    }
}
