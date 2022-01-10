using System;
using System.Windows;
using Konfigurator.Logic.Models.Employee;

namespace Konfigurator.Windows
{
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePasswordButton(object sender, RoutedEventArgs e)
        {
            var id = idText.Text;
            var oldPw = oldPassword.Password;
            var newPw = newPassword.Password;

            if (EmployeeDB.ChangeEmployeePassword(Int32.Parse(id), oldPw, newPw))
            {
                this.Close();
            }
        }
    }
}