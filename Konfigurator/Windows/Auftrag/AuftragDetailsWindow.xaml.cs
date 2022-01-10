using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Floor;
using Konfigurator.Logic.Models.FloorAndRoom;
using Konfigurator.Logic.Models.Order;
using Konfigurator.Logic.Models.Package;
using Konfigurator.Logic.Models.PackageDetails;
using Konfigurator.Logic.Models.Room;

namespace Konfigurator.Windows.Auftrag
{
    public partial class AuftragDetailsWindow : Window
    {
        public AuftragDetailsWindow(int orderid)
        {
            OrderId = orderid;
            InitializeComponent();
            UpdatePackageDataGrid();
            UpdateFloorCombobox();
            UpdateRoomCombobox();
        }
        
        
        public int OrderId;
        
        //On Selected Datagrid Row
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = PackageDataGrid.SelectedItem; 
            
            //Selected Item | id
            string packageid = (PackageDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
           
            //Display Items in Textbox
            if (packageid != null) PackageIdText.Text = packageid;
            UpdateArticleInPackageDataGrid();
        }
        public void UpdatePackageDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = PackageDB.GetDataSetPackage();
                PackageDataGrid.ItemsSource = dataset.Tables["Paket"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        //Update Floor Combobox
        private void UpdateFloorCombobox()
        {
            try
            {
                var dataset = FloorDB.GetDataSetFloor();
                FloorCombo.ItemsSource = dataset.Tables["Etage"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        //Update Room Combobox
        private void UpdateRoomCombobox()
        {
            try
            {
                var dataset = RoomDB.GetDataSetRoom();
                RoomCombo.ItemsSource = dataset.Tables["Raum"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void UpdateArticleInPackageDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = PackageDetailsDB.GetDataSetPackageDetails(Int32.Parse(PackageIdText.Text));
                ArticlesDataGrid.ItemsSource = dataset.Tables["PaketDetails"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AddFloorAndRoomToOrder(object sender, RoutedEventArgs e)
        {
            var floor = FloorCombo.SelectedValue;
            var room = RoomCombo.SelectedValue;
            var packageID = PackageIdText.Text;
            var roomSize = RoomSize.Text;
            var orderid = OrderId;

            try
            {
                FloorAndRoomDB.CreateFloorAndRoom(new FloorAndRoom(orderid, Int32.Parse(floor.ToString()), Int32.Parse(room.ToString()), Int32.Parse(packageID), double.Parse(roomSize)));
                System.Threading.Thread.Sleep(1000);
                UpdateOrderPriceData();
                System.Threading.Thread.Sleep(500);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Es wurden nicht alle Daten richtig eingegeben");
            }
            
        }

        public void UpdateOrderPriceData()
        {
            try
            {
                var price = OrderDB.GetPriceForOrder(OrderId);
                
                var overallPrice = OrderDB.GetFullOrderPrice(OrderId);
                
                
                OrderDB.UpdateOrderPrice(OrderId, price, overallPrice);
                Console.WriteLine(overallPrice + " "  + price);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
           
        }
        //Hides unnecessary Objects in PackageDatagrid
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Paket_ID")
            {
                e.Cancel = true;
            }
            
        }
        
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}