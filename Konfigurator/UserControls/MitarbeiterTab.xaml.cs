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
    }
}