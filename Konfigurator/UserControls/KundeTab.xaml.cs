using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Konfigurator.Logic.Models.Customer;

namespace Konfigurator.UserControls
{
    public partial class KundeTab : UserControl
    {
        public KundeTab()
        {
            InitializeComponent();
            UpdateDataGrid();
        }


        private void UpdateDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = CustomerDB.GetDataSetCustomer();
                DataGrid.ItemsSource = dataset.Tables["Kunde"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        //Mark Customer as not current (old)
        private void KillCustomer(object sender, RoutedEventArgs e)
        {
            CustomerDB.KillCustomer(Int32.Parse(IdText.Text));
            
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }

        //Marks Customer as current
        private void ReviveCustomer(object sender, RoutedEventArgs e)
        {
            CustomerDB.ReviveCustomer((Int32.Parse(IdText.Text)));
            
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }
        private void EditCustomer(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = IdText.Text;
            var name = NameText.Text;
            var plz = PlzText.Text;
            var region = PlaceText.Text;
            var street = StreetText.Text;
            var tel = TelText.Text;
            var email = EmailText.Text;
            bool current =  currentCheck.IsChecked != null && (bool)currentCheck.IsChecked;
            
            CustomerDB.UpdateCustomer(new Customer(Int32.Parse(id), name, Int32.Parse(plz),
                region, street, tel, email, current) );
            
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
            
        }
        //Add new Customer to DataBase
        public void AddCustomer(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            var plz = PlzText.Text;
            var region = PlaceText.Text;
            var street = StreetText.Text;
            var tel = TelText.Text;
            var email = EmailText.Text;
            bool current =  currentCheck.IsChecked != null && (bool)currentCheck.IsChecked;

            CustomerDB.CreateCustomer(new Customer(Int32.Parse(id), name, Int32.Parse(plz), region, street,
                tel, email, current));
            
            System.Threading.Thread.Sleep(1000);
            UpdateDataGrid();
            
        }
        
        //On Selected Datagrid Row
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGrid.SelectedItem; 
            
            //Selected Item | id
            var id = (DataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            var name = (DataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | postalcode
            var plz = (DataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | place
            var place = (DataGrid.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | street
            var street = (DataGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | telephone
            var tel = (DataGrid.SelectedCells[5].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | email
            var email = (DataGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | Current
            bool? isCurrent = (DataGrid.SelectedCells[7].Column.GetCellContent(item) as CheckBox)?.IsChecked;

            //Display Items in Textbox
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
            if (plz != null) PlzText.Text = plz;
            if (place != null) PlaceText.Text = place;
            if (street != null) StreetText.Text = street;
            if (tel != null) TelText.Text = tel;
            if (email != null) EmailText.Text = email;
            currentCheck.IsChecked = isCurrent;
            
            IsCurrent();
        }

        public void IsCurrent()
        {
            bool available = currentCheck.IsChecked != null && (bool)currentCheck.IsChecked;

            if (available)
            {
                currentText.Text = "Aktuell";
                currentText.Foreground = Brushes.Green;
            }
            else
            {
                currentText.Text = "Nicht Aktuell";
                currentText.Foreground = Brushes.Red;
            }
        }
       
    }
}