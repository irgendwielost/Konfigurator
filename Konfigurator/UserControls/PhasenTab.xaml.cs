using System;
using System.Windows;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Phase;

namespace Konfigurator.UserControls
{
    public partial class PhasenTab : UserControl
    {
        public PhasenTab()
        {
            InitializeComponent();

            UpdateDataGrid();
        }

        
        private void UpdateDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = PhaseDB.GetDataSetPhasen();
                DataGrid.ItemsSource = dataset.Tables["Phasen"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
        }

        private void EditPhase(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = IdText.Text;
            var name = NameText.Text;
            
            PhaseDB.UpdatePhase(new Phase(Int32.Parse(id), name));
            
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }
        private void CreatePhase(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = IdText.Text;
            var name = NameText.Text;
            
            //Create phase Method 
            PhaseDB.CreatePhase(new Phase(Int32.Parse(id), name));
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }
    }
}