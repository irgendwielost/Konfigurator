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
    }
}