using System.Windows;
using System.Windows.Controls;
using Konfigurator.Windows.Auftrag;

namespace Konfigurator.UserControls
{
    public partial class AuftragTab : UserControl
    {
        public AuftragTab()
        {
            InitializeComponent();
        }

        private void OpenDetailsWindow(object sender, RoutedEventArgs e)
        {
            
            AuftragDetailsWindow auftragDetails = new AuftragDetailsWindow();
            auftragDetails.Show();
        }
    }
}