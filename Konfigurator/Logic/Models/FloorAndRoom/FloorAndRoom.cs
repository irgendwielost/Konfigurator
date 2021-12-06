namespace Konfigurator.Logic.Models.FloorAndRoom
{
    public class FloorAndRoom
    {
        public FloorAndRoom(int orderId, int floorId, int roomId, int packageId, double roomSize)
        {
            Order_ID = orderId;
            Floor_ID = floorId;
            Room_ID = roomId;
            Package_ID = packageId;
            Room_Size = roomSize;
        }

        public int Order_ID { get; set; }
        public int Floor_ID { get; set; }
        public int Room_ID { get; set; }
        public int Package_ID { get; set; }
        public double Room_Size { get; set; }
    }
}