using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Konfigurator.Logic.DataBase;
using Konfigurator.UserControls;


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
        public static string name;
        private void LogIn_Onclick(object sender, RoutedEventArgs e)
        {
            pw = Password.Password;
            name = Mitarbeiter.Text;

            var db = new DataBase();

            db.Connection.Open();
            try
            {
                var cmd = new OleDbCommand(
                    $"Select Mitarbeiter_Passwort from Mitarbeiter where Mitarbeiter_ID = {name}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Id oder Passwort Falsch\n" +
                                    "========");

                if (reader.Read())
                {
                    if (pw == reader.GetString(0))
                    {
                        this.Hide();
                        MainWindow main = new MainWindow();
                        main.Show();
                    }
                    else
                        MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                        "Id oder Passwort ist falsch\n" +
                                        "========");
                }
            }
            catch (Exception)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Ein Unbekannter Fehler ist Aufgetreten\n" +
                                "========");
            }
        }

        //Only numbers
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}