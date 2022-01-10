using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Konfigurator.Logic;
using Konfigurator.Logic.DataBase;
using Konfigurator.Logic.Models.Employee;
using Konfigurator.UserControls;
using Konfigurator.Windows;
using MaterialDesignColors.Recommended;

namespace Konfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //Current Time and Date variables
        DateTime localDate = DateTime.Now;
        public MainWindow()
        {
            InitializeComponent();
            CurrentDate.Text = localDate.ToString("d") ;
            GetNameByID();
        }


        private void GetNameByID()
        {
            var id = LogIn.name;
            
            var db = new DataBase();

            db.Connection.Open();
            try
            {
                var cmd = new OleDbCommand(
                    $"Select Mitarbeiter_Name from Mitarbeiter where Mitarbeiter_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UsernameText.Text = reader.GetString(reader.GetOrdinal("Mitarbeiter_Name"));
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
       
       
        private void Closebutton1_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sind sie sicher das Sie die Anwendung beenden möchten?", "Anwendung beenden", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.Show();
        }
        
        

        //Tab Buttons

        private AuftragTab _auftragTab = new AuftragTab();              // Auftrag
        private ArtikelTab _artikelTab = new ArtikelTab();              // Artikel 
        private FaktorTab _faktorTab = new FaktorTab();                 // Faktor
        private HausTab _hausTab = new HausTab();                       // Haus
        private KundeTab _kundeTab = new KundeTab();                    // Kunde
        private MitarbeiterTab _mitarbeiterTab = new MitarbeiterTab();  //Mitarbeiter
        private PaketTab _paketTab = new PaketTab();                    //Paket
        private PhasenTab _phasenTab = new PhasenTab();                 //Phasen

        
        //Create SolidColorBrush for Pressed Button
         public static string PressedColor = "#6C6CB8";
        // ReSharper disable once PossibleNullReferenceException
        static Color c1 = (Color)ColorConverter.ConvertFromString(PressedColor);
        SolidColorBrush colorFromString_Pressed = new SolidColorBrush(c1);
        
       

        private void AuftragButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = _auftragTab;
            TabHeaderBlock.Text = "Auftrag";

        }
        private void ArtikelButton_OnClick(object sender, RoutedEventArgs e)
        {
           
            ContentControl.Content = _artikelTab;
            TabHeaderBlock.Text = "Artikel";
        }
        private void FaktorButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = _faktorTab;
            TabHeaderBlock.Text = "Faktor";
        }
        private void HausButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = _hausTab;
            TabHeaderBlock.Text = "Haus";
        }
        private void KundeButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = _kundeTab;
            TabHeaderBlock.Text = "Kunde";
        }
        
        private void MitarbeiterButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = _mitarbeiterTab;
            TabHeaderBlock.Text = "Mitarbeiter";
        }
        
        private void PaketButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = _paketTab;
            TabHeaderBlock.Text = "Paket";
        }

        private void PhasenButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = _phasenTab;
            TabHeaderBlock.Text = "Phasen";
        }
        
        public void Reset()
        {
            string color = "#586096";
            // ReSharper disable once PossibleNullReferenceException
            Color c = (Color)ColorConverter.ConvertFromString(color);
            SolidColorBrush colorFromString = new SolidColorBrush(c);

            Auftrag.Background = colorFromString;
            Kunde.Background = colorFromString;
            Paket.Background = colorFromString;
            Artikel.Background = colorFromString;
            Faktor.Background = colorFromString;
            Mitarbeiter.Background = colorFromString;
            Haus.Background = colorFromString;
            Phasen.Background = colorFromString;
        }
    }
    
}