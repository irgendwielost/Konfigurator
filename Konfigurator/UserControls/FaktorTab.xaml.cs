using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
           
            //Selected Item | Available
            bool? isInUse = (DataGrid.SelectedCells[4].Column.GetCellContent(item) as CheckBox)?.IsChecked;
            
            //Display Items in Textbox
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
            if (mult != null) MultText.Text = mult;
            if (size != null) SizeText.Text = size;
            InUseCheck.IsChecked = isInUse;
            IsInUse();
        }

        //Edit factor
        private void EditFactor(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = IdText.Text;
            var name = NameText.Text;
            var mult = MultText.Text;
            var size = SizeText.Text;
            bool inUse = InUseCheck.IsChecked != null && (bool)InUseCheck.IsChecked;
            
            FactorDB.UpdateFaktor(new Factor(Int32.Parse(id), name, double.Parse(mult), double.Parse(size), inUse ));
            
            System.Threading.Thread.Sleep(1000);
            UpdateDataGrid();
        }

        //Set factor as not used 
        private void KillFactor(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            
            FactorDB.KillFactor(Int32.Parse(id));
            
            System.Threading.Thread.Sleep(1000);
            UpdateDataGrid();
        }
        private void AddFactor(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            var mult = MultText.Text;
            var size = SizeText.Text;
            bool inUse = InUseCheck.IsChecked != null && (bool)InUseCheck.IsChecked;
            
            FactorDB.CreateFactor(new Factor(Int32.Parse(id), name, double.Parse(mult), double.Parse(size), inUse));
            
            System.Threading.Thread.Sleep(1000);
            UpdateDataGrid();
            
        } 
        
        //Set InUseText to "In Benutzung" or "Unbenutzt"
        private void IsInUse()
        {
            bool available = InUseCheck.IsChecked != null && (bool)InUseCheck.IsChecked;

            if (available)
            {
                InUseText.Text = "In Benutzung";
                InUseText.Foreground = Brushes.Green;
            }
            else
            {
                InUseText.Text = "Unbenutzt";
                InUseText.Foreground = Brushes.Red;
            }
        }
    }
}