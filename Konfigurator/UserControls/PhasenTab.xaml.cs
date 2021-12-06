using System.Windows.Controls;
using Konfigurator.Logic.Models.Phase;

namespace Konfigurator.UserControls
{
    public partial class PhasenTab : UserControl
    {
        public PhasenTab()
        {
            InitializeComponent();
            //Fill DataGridView
            
            var dataset = PhaseDB.GetDataSetPhasen();
            DataGrid.ItemsSource = dataset.Tables["Phasen"].DefaultView;
            
        }
    }
}