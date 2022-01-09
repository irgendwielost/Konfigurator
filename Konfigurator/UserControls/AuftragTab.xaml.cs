using System;
using System.Collections;
using System.Collections.Generic;
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
        //this is the new file 
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
        private void DataGridOrder_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGridOrder.SelectedItem; 
            
            //Selected Item | id
            string id = (DataGridOrder.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | date
            string date = (DataGridOrder.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | new or stock
            bool? newOrStock = (DataGridOrder.SelectedCells[2].Column.GetCellContent(item) as RadioButton)?.IsChecked;
            
            //Selected Item | phase
            string customerId = (DataGridOrder.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | housing
            string housingId = (DataGridOrder.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | phase
            string phaseId = (DataGridOrder.SelectedCells[5].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | total
            string total = (DataGridOrder.SelectedCells[5].Column.GetCellContent(item) as TextBlock)?.Text;
            
            
            
            //Display Items in Textbox
            IDText.Text = id;
            dateText.Text = date;
            customerIdText.Text = customerId;
            housingCombo.SelectedValue = housingId;
            phaseCombo.SelectedValue = phaseId;
            totalText.Text = total;
            newRadio.IsChecked = newOrStock;
            NewOrStock();
        }

        private void NewOrStock()
        {
            if (newRadio.IsChecked == true)
            {
                stockRadio.IsChecked = false;
            }
            else
            {
                stockRadio.IsChecked = true;
            }
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