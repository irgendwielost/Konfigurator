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
        
        //On Selected Datagrid Row
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGrid.SelectedItem; 
            
            //Selected Item | id
            string id = (DataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            string name = (DataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | mult
            string mult = (DataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;
           
            //Selected Item | size
            string size = (DataGrid.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
           
            //Display Items in Textbox
            IdText.Text = id;
            NameText.Text = name;
            MultText.Text = mult;
            SizeText.Text = size;
            
        }
    }
}