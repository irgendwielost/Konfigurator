using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Konfigurator.Logic.Models.FloorAndRoom;
using Konfigurator.Logic.Models.Order;
using Konfigurator.Logic.Models.Phase;
using Konfigurator.Windows.Auftrag;

namespace Konfigurator.UserControls
{
    public partial class AuftragTab : UserControl
    {
        public AuftragTab()
        {
            InitializeComponent();
            //Fill DataGridView
            
            //Orders
            var dataset = OrderDB.GetDataSetOrder();
            DataGridOrder.ItemsSource = dataset.Tables["Auftrag"].DefaultView;
            
            GetPackageById();
        }

        //On Selected Datagrid Row
        private void DataGridOrder_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Make sure row was clicked
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;
            
            //Fill Textboxes
            var Id = IDText.Text; // ID
            
            

        }

        //Show Package by selected ID
        public void GetPackageById()
        {
            //Selected Order Id
            string order = IDText.Text;
            if (string.IsNullOrEmpty(order))
            {
                return;
            }
            int orderID = Int32.Parse(order);

            
            var datasetPackage = FloorAndRoomDB.GetDataSetFloorAndRoomDetailsByOrder(orderID);
            DataGridOrder.ItemsSource = datasetPackage.Tables["EtageUndRaum"].DefaultView;
            
        }
        
        
        
        //Hide unnecessary Objects in DataGrid
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Auftrag_Grosse")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Kunde_ID")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Gebaude_ID")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Faktor_Mult")
            {
                e.Cancel = true;
            }
           
        }

        private void OpenDetailsWindow(object sender, RoutedEventArgs e)
        {
            AuftragDetailsWindow auftragDetails = new AuftragDetailsWindow();
            auftragDetails.Show();
        }

        
    }
}