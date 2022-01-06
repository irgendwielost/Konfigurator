using System;
using System.Windows;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Floor;
using Konfigurator.Logic.Models.Housing;
using Konfigurator.Logic.Models.Room;

namespace Konfigurator.UserControls
{
    public partial class HausTab : UserControl
    {
        public HausTab()
        {
            InitializeComponent();
            //Fill DataGridView
            
            //Housing
            UpdateHousingDataGrid();
            
            //Floor
            UpdateFloorDataGrid();
            
            //Room
            UpdateRoomDataGrid();
        }

        private void UpdateHousingDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataSetHousing = HousingDB.GetDataSetHousing();
                DataGridHousing.ItemsSource = dataSetHousing.Tables["Gebaude"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateFloorDataGrid()
        {
            //Fill DataGridView
            try
            {
                var datasetFloor = FloorDB.GetDataSetFloor();
                DataGridFloor.ItemsSource = datasetFloor.Tables["Etage"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateRoomDataGrid()
        {
            //Fill DataGridView
            try
            {
                var datasetRoom = RoomDB.GetDataSetRoom();
                DataGridRoom.ItemsSource = datasetRoom.Tables["Raum"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        
        //On Selected Datagrid Row | Room
        private void DataGridRoom_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGridRoom.SelectedItem; 
            
            //Selected Item | id
            string id = (DataGridRoom.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            string name = (DataGridRoom.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
           
            //Display Items in Textbox
            if (id != null) RoomIdText.Text = id;
            if (name != null) RoomNameText.Text = name;
        }
        
        
        //On Selected Datagrid Row | Floor
        private void DataGridFloor_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGridFloor.SelectedItem; 
            
            //Selected Item | id
            string id = (DataGridFloor.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            string name = (DataGridFloor.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
           
            //Display Items in Textbox
            FloorIdText.Text = id;
            FloorNameText.Text = name;

        }
        
        //On Selected Datagrid Row | Housing
        private void DataGridHousing_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = DataGridHousing.SelectedItem; 
            
            //Selected Item | id
            string id = (DataGridHousing.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            string name = (DataGridHousing.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
           
            //Display Items in Textbox
            if (id != null) HousingIdText.Text = id;
            if (name != null) HousingNameText.Text = name;
        }

        //Add to DataBase Functions
        
        
        //Add Housing to DataBase 
        private void AddHousing(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = HousingIdText.Text;
            var name = HousingNameText.Text;
            
            //Create Housing Method
            HousingDB.CreateHousing(new Housing(Int32.Parse(id), name));
            System.Threading.Thread.Sleep(1000);
            UpdateHousingDataGrid();
        }
        
        //Add Floor to DataBase
        private void AddFloor(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = FloorIdText.Text;
            var name = FloorNameText.Text;
            
            //Create Floor Method
            FloorDB.CreateFloor(new Floor(Int32.Parse(id), name));
            System.Threading.Thread.Sleep(1000);
            UpdateFloorDataGrid();
        }

        private void AddRoom(object sender, RoutedEventArgs e)
        {
            //Variables
            var id = RoomIdText.Text;
            var name = RoomNameText.Text;
            
            //Create Room Method
            RoomDB.CreateRaum(new Room(Int32.Parse(id), name));
            System.Threading.Thread.Sleep(1000);
            UpdateRoomDataGrid();
        }
        
    }
    
}