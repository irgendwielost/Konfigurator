namespace Konfigurator.Models.FloorAndRoom
{
    public class FloorAndRoom
    {
        public FloorAndRoom(int orderId, int floorId, int roomId, int paketId, double roomSize)
        {
            Order_ID = orderId;
            Floor_ID = floorId;
            Room_ID = roomId;
            Paket_ID = paketId;
            Room_Size = roomSize;
        }

        public int Order_ID { get; set; }
        public int Floor_ID { get; set; }
        public int Room_ID { get; set; }
        public int Paket_ID { get; set; }
        public double Room_Size { get; set; }
    }
}