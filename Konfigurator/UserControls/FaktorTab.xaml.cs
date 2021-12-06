using System.Windows.Controls;
using Konfigurator.Logic.Models.Factor;

namespace Konfigurator.UserControls
{
    public partial class FaktorTab : UserControl
    {
        public FaktorTab()
        {
            InitializeComponent();
            //Fill DataGridView
            var dataset = FactorDB.GetDataSetFactor();
            DataGrid.ItemsSource = dataset.Tables["Faktor"].DefaultView;
        }
    }
}