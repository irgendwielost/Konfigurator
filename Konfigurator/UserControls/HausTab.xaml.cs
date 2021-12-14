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
            var dataSetHousing = HousingDB.GetDataSetHousing();
            DataGridHousing.ItemsSource = dataSetHousing.Tables["Gebaude"].DefaultView;
            
            //Floor
            var datasetFloor = FloorDB.GetDataSetFloor();
            DataGridFloor.ItemsSource = datasetFloor.Tables["Etage"].DefaultView;
            
            //Room
            var datasetRoom = RoomDB.GetDataSetRoom();
            DataGridRoom.ItemsSource = datasetRoom.Tables["Raum"].DefaultView;
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
            RoomIdText.Text = id;
            RoomNameText.Text = name;

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
            HousingIdText.Text = id;
            HousingNameText.Text = name;

        }
    }
}