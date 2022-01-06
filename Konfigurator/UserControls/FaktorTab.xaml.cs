using System;
using System.Windows;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Factor;

namespace Konfigurator.UserControls
{
    public partial class FaktorTab : UserControl
    {
        public FaktorTab()
        {
            InitializeComponent();
            UpdateDataGrid();   
        }


        private void UpdateDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = FactorDB.GetDataSetFactor();
                DataGrid.ItemsSource = dataset.Tables["Faktor"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
            if (mult != null) MultText.Text = mult;
            if (size != null) SizeText.Text = size;
        }

        //Edit factor
        private void EditFactor(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = IdText.Text;
            var name = NameText.Text;
            var mult = MultText.Text;
            var size = SizeText.Text;
            
            FactorDB.UpdateFaktor(new Factor(Int32.Parse(id), name, Double.Parse(mult), Double.Parse(size), true ));
            
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }

        //Set factor as not used 
        private void KillFactor(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            
            FactorDB.KillFactor(Int32.Parse(id));
            
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }
        private void AddFactor(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            var mult = MultText.Text;
            var size = SizeText.Text;
            
            FactorDB.CreateFactor(new Factor(Int32.Parse(id), name, Double.Parse(mult), Double.Parse(size), true));
            System.Threading.Thread.Sleep(1000);
            UpdateDataGrid();
            
        } 
    }
}