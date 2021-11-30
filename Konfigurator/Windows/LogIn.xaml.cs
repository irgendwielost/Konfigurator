using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Konfigurator.Logic.DataBase;


namespace Konfigurator.Windows
{
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        //Log In
        private string pw;
        private string name;
        private void LogIn_Onclick(object sender, RoutedEventArgs e)
        {
            pw = Password.Password;
            name = Mitarbeiter.Text;

            var db = new DataBase();
            db.Connection.Open();

            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }    
        
        //Only numbers
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}