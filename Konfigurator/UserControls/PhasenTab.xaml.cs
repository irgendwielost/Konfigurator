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

        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGrid.SelectedItem; 
            
            //Selected Item | id
            string id = (DataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            string name = (DataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
           
            //Display Items in Textbox
            IdText.Text = id;
            NameText.Text = name;
           
        }
    }
}