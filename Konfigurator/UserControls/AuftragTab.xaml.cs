using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            
            GetPackageById();
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
        
        //Add Package to Database
        private void AddOrder(object sender, RoutedEventArgs e)
        {
            var id = IDText.Text; ;
            var customerID = customerIdText.Text;
            var housing = housingCombo.SelectedValue;
            var phase = phaseCombo.SelectedValue;
            var phaseId = PhaseDB.PhaseIdByName(phase.ToString());
            
            var selectedDate = this.dateText.SelectedDate;
            if (selectedDate != null)
            {
                var selectedOrderDate = selectedDate.Value.ToShortDateString();
            
                OrderDB.CreateOrder(new Order(Int32.Parse(id), DateTime.Parse(selectedOrderDate), true, 1, Int32.Parse(customerID), Int32.Parse(phaseId.ToString()), 1, 1));
            
                System.Threading.Thread.Sleep(1000);
                UpdateDataGrid();
            }
            else
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Das Datum wurde nicht eingegeben\n" +
                                "========");;
            }
            
            
            
        }

        public void GetPhaseIDByName(object sender, RoutedEventArgs e)
        {
            var phase = phaseCombo.SelectedValue;
            var phaseId = PhaseDB.PhaseIdByName(phase.ToString());

            MessageBox.Show(phaseId.ToString());
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


        public class Language
        {
            
        }

    }
}