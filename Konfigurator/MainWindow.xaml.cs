using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Konfigurator.UserControls;
using Konfigurator.Windows;

namespace Konfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
            PasswortAendern pw = new PasswortAendern();
            pw.Show();
        }
        
        
        //Tab Buttons
        private void Button_Enter(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Button Hover");
        }
    }
}