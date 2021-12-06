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
    }
}