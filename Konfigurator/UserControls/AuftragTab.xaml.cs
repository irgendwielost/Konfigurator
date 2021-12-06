using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Order;
using Konfigurator.Logic.Models.Phase;
using Konfigurator.Windows.Auftrag;

namespace Konfigurator.UserControls
{
    public partial class AuftragTab : UserControl
    {
        public AuftragTab()
        {
            InitializeComponent();
            //Fill DataGridView
            
            var dataset = OrderDB.GetDataSetOrder();
            DataGrid.ItemsSource = dataset.Tables["Auftrag"].DefaultView;
            
        }

        

        private void OpenDetailsWindow(object sender, RoutedEventArgs e)
        {
            AuftragDetailsWindow auftragDetails = new AuftragDetailsWindow();
            auftragDetails.Show();
        }
    }
}