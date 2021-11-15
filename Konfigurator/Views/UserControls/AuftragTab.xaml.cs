using System.Windows;
using System.Windows.Controls;
using Konfigurator.Windows.Auftrag;

namespace Konfigurator.Views.UserControls
{
    public partial class AuftragTab : UserControl
    {
        public AuftragTab()
        {
            InitializeComponent();
        }

        public void OpenDetailsWindow(object sender, RoutedEventArgs e)
        {
            AuftragDetailsWindow auftragDetails = new AuftragDetailsWindow();
            auftragDetails.Show();
        }
    }
}