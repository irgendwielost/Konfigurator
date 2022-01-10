using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Konfigurator.Logic.Models.Factor;
using Konfigurator.Logic.Models.FloorAndRoom;
using Konfigurator.Logic.Models.Housing;
using Konfigurator.Logic.Models.Order;
using Konfigurator.Logic.Models.Package;
using Konfigurator.Logic.Models.Phase;
using Konfigurator.Windows.Auftrag;

namespace Konfigurator.UserControls
{
    public partial class AuftragTab : UserControl
    {
        public AuftragTab()
        {
            InitializeComponent();
            UpdateDataGrid();
            UpdatePhaseCombobox();
            UpdateHousingCombobox();
        }

        private void UpdateDataGrid()
        {
            try
            {
                //Fill DataGridView
                //Orders
                var dataset = OrderDB.GetDataSetOrder();
                DataGridOrder.ItemsSource = dataset.Tables["Auftrag"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateFloorAndRoomDataGrid()
        {
            try
            {
                //Fill DataGridView
                //Orders
                var dataset = FloorAndRoomDB.GetDataSetFloorAndRoomDetailsByOrder(Int32.Parse(IDText.Text));
                DataGridPackage.ItemsSource = dataset.Tables["EtageUndRaum"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        //Update Phase Combobox
        private void UpdateHousingCombobox()
        {
            try
            {
                var dataset = HousingDB.GetDataSetHousing();
                housingCombo.ItemsSource = dataset.Tables["Gebaude"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        
        //Update Phase Combobox
        private void UpdatePhaseCombobox()
        {
            try
            {
                var dataset = PhaseDB.GetDataSetPhasen();
                this.phaseCombo.ItemsSource = dataset.Tables["Phasen"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
            string total = (DataGridOrder.SelectedCells[6].Column.GetCellContent(item) as TextBlock)?.Text;
            
            
            
            //Display Items in Textbox
            try
            {
                if (id != null) IDText.Text = id;
                if (date != null) dateText.Text = date;
                if (customerId != null) customerIdText.Text = customerId;
                if (housingId != null) housingCombo.SelectedValue = housingId;
                if (phaseId != null) phaseCombo.SelectedValue = phaseId;
                var Total = total + "€";
                if (total != null) totalText.Text = Total;
                if (newOrStock == true)
                {
                    newCheck.IsChecked = true;
                }
                else
                {
                    currentCheck.IsChecked = true;
                }
                
                UpdateFloorAndRoomDataGrid();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
        }

        private void NewOrStock(object sender, RoutedEventArgs e)
        {
            if (newCheck.IsChecked == true)
            {
                currentCheck.IsChecked = false;
            }
            else
            {
                currentCheck.IsChecked = true;
            }
        }
        
        
        //Add Package to Database
        private void AddOrder(object sender, RoutedEventArgs e)
        {
            var id = IDText.Text; ;
            var customerID = customerIdText.Text;
            var housing = housingCombo.SelectedValue;
            var phase = phaseCombo.SelectedValue;

            var selectedDate = this.dateText.SelectedDate;
            try
            {
                if (selectedDate != null)
                {
                    var selectedOrderDate = selectedDate.Value.ToShortDateString();
            
                    OrderDB.CreateOrder(new Order(Int32.Parse(id), DateTime.Parse(selectedOrderDate), true, Int32.Parse(customerID), Int32.Parse(housing.ToString()), Int32.Parse(phase.ToString()), 0, 0));
            
                    System.Threading.Thread.Sleep(500);
                    OpenDetailsWindow(Int32.Parse(IDText.Text));
                    UpdateDataGrid();
                }
                else
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Das Datum wurde nicht eingegeben\n" +
                                    "========");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Es gab einen Fehler bei dem einfügen des Datensatzes");
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

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            OpenDetailsWindow(Int32.Parse(IDText.Text));
        }
        
        //Hide unnecessary Objects in OrderDataGrid
        private void OnAutoGeneratingColumnOrder(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

        //Hides unnecessary Objects in PackageDatagrid
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Auftrag_ID")
            {
                e.Cancel = true;
            }
            
        }
        
        private void OpenDetailsWindow(int id)
        {
            try
            {
                AuftragDetailsWindow auftragDetails = new AuftragDetailsWindow(id);
                auftragDetails.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Es muss eine Auftrags ID ausgewählt werden");
            }
            
        }

       
        
        private void Refresh(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            UpdatePhaseCombobox();
            UpdateHousingCombobox();
        }

    }
}