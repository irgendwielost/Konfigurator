using System.Windows.Controls;
using Konfigurator.Logic.Models.Employee;

namespace Konfigurator.UserControls
{
    public partial class MitarbeiterTab : UserControl
    {
        public MitarbeiterTab()
        {
            InitializeComponent();
            //Fill DataGridView
            var dataset = EmployeeDB.GetDataSetEmployee();
            DataGrid.ItemsSource = dataset.Tables["Mitarbeiter"].DefaultView;
            
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
            
           
            //Display Items in Textbox
            IdText.Text = id;
            NameText.Text = name;
            //isEmployed.SelectedText = isEmployed;
        }
    }
}