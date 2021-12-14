using System;
using System.ComponentModel;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Customer;

namespace Konfigurator.UserControls
{
    public partial class KundeTab : UserControl
    {
        public KundeTab()
        {
            InitializeComponent();
            
            //Fill DataGridView
            var dataset = CustomerDB.GetDataSetCustomer();
            DataGrid.ItemsSource = dataset.Tables["Kunde"].DefaultView;
            
        }

        /* Maybe dont use?
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Kunde_PLZ")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Kunde_Ort")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Kunde_Strasse")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Kunde_Tel")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Kunde_Email")
            {
                e.Cancel = true;
            }
        }
        */
        
        //On Selected Datagrid Row
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGrid.SelectedItem; 
            
            //Selected Item | id
            string id = (DataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            string name = (DataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | postalcode
            string plz = (DataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | place
            string place = (DataGrid.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | street
            string street = (DataGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | telephone
            string tel = (DataGrid.SelectedCells[5].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | email
            string email = (DataGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock)?.Text;
            
            
            //Display Items in Textbox
            IdText.Text = id;
            NameText.Text = name;
            PlzText.Text = plz;
            PlaceText.Text = place;
            StreetText.Text = street;
            TelText.Text = tel;
            EmailText.Text = email;
            
            //isAvailableText.SelectedText = isAvailable;
        }
    }
}