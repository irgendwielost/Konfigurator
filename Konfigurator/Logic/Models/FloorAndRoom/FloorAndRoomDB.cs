using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

// Needs DeleteMethod 

namespace Konfigurator.Logic.Models.FloorAndRoom
{
    public class FloorAndRoomDB
    {
        /* DataSet to fill DataGrids */
        public static DataSet GetDataSetFloorAndRoomDetails()
        {
            /* Database Connection being opened */
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                /* in case the Table is not found "try" is used */
                try
                {
                    /* in the EtageUndRaum all entries will be selected and added to the DataSet (consider changing this for not all but just all recently) */
                    var cmd = new OleDbDataAdapter("select * from EtageUndRaum"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "EtageUndRaum");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die EtageUndRaum-Tabelle konnte nicht gefunden werden\n" +
                                    "========");
                }
            }
            return null;
        }
        
        /* DataSet to fill DataGrids With all "EtageUndRaum" where this Order is in */
        public static DataSet GetDataSetFloorAndRoomDetailsByOrder(int id)
        {
            /* Database Connection being opened */
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                /* in case the Table is not found "try" is used */
                try
                {
                    /* in the EtageUndRaum all entries will be selected and added to the DataSet (consider changing this for not all but just all recently) */
                    var cmd = new OleDbDataAdapter($"Select * from EtageUndRaum where Auftrag_ID = {id}"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "EtageUndRaum");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die EtageUndRaum-Tabelle konnte nicht gefunden werden\n" +
                                    "========");
                }
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        /// <summary>
        /// Returns a FloorAndRoom by looking for the different ID's
        /// </summary>
        /// <param name="ID('s)"></param>
        /// <returns>FloorAndRoom</returns>
        
        /* Gives back a specific FloorAndRoom with the Auftrag_ID */
        public static FloorAndRoom GiveFloorAndRoomBackByOrder(int ID)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this Auftrag_ID */
                var cmd = new OleDbCommand(
                    $"Select * from EtageUndRaum where Auftrag_ID = {ID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FloorAndRoom(ID, reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDouble(4));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: EtageUndRaum konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }
            return null;
        }

        /* Gives back a specific FloorAndRoom with the Auftrag_ID and Etage_ID */
        public static FloorAndRoom GiveFloorAndRoomBackByFloor(int OrderID, int FloorID)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this Auftrag_ID and Etage_ID */
                var cmd = new OleDbCommand(
                    $"Select * from EtageUndRaum where Auftrag_ID = {OrderID} and Etage_ID = {FloorID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FloorAndRoom(OrderID,FloorID, reader.GetInt32(2), reader.GetInt32(3), reader.GetDouble(4));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Auftrag und/oder Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }
            return null;
        }
        
        /* Gives back a specific FloorAndRoom with the Raum_ID */
        public static FloorAndRoom GiveFloorAndRoomBackByRoom(int OrderID, int RoomID)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this Raum_ID */
                var cmd = new OleDbCommand(
                    $"Select * from EtageUndRaum where Auftrag_ID = {OrderID} and Raum_ID = {RoomID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FloorAndRoom(OrderID,reader.GetInt32(1), RoomID, reader.GetInt32(3), reader.GetDouble(4));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Auftrag und/oder Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }
            return null;
        }
        
        /* Gives back a specific FloorAndRoom with the Auftrag_ID and Etage_ID and Raum_ID */
        public static FloorAndRoom GiveFloorAndRoomBackByFloorAndRoom(int OrderID, int FloorID, int RoomID)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this Auftrag_ID and Etage_ID and Raum_ID */
                var cmd = new OleDbCommand(
                    $"Select * from EtageUndRaum where Auftrag_ID = {OrderID} and Etage_ID = {OrderID} and Raum_ID = {RoomID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FloorAndRoom(OrderID, FloorID, RoomID, reader.GetInt32(3), reader.GetDouble(4));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Auftrag und/oder Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden \n" +
                                "========");
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Creates "EtageUndRaum" with all attributes
        public static void CreateFloorAndRoom(FloorAndRoom floorAndRoom)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                double packagePrice = 0;
                packagePrice = PackageDetails.PackageDetailsDB.PackageDetailsGetPrice(floorAndRoom.Package_ID);
                var cmd = new OleDbCommand(
                    $"insert into EtageUndRaum (Auftrag_ID, Etage_ID, Raum_ID, Paket_ID, Raum_Grosse, Paket_Preis) values ({floorAndRoom.Order_ID}, {floorAndRoom.Floor_ID}, {floorAndRoom.Room_ID}, {floorAndRoom.Package_ID}, {floorAndRoom.Room_Size}, '{packagePrice}')"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Nicht alle Daten wurden richtig eingegeben\n" +
                                "2: Einer der Id's existiert nicht\n" +
                                "========");
                Console.WriteLine(e);
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Updates "EtageUndRaum" with "Paket_ID", "Raum_Grosse" and "Paket_Preis" in the Database using Id for "Auftrag", "Etage" and "Raum" to find it
        public static void UpdatePhase(FloorAndRoom floorAndRoom)
        {
            // Open Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            // Update all but "Paket_Preis" by looking for ID
            try
            {
                double packagePrice = PackageDetails.PackageDetailsDB.PackageDetailsGetPrice(floorAndRoom.Package_ID);
                var cmd = new OleDbCommand(
                    $"Update EtageUndRaum set Paket_ID = {floorAndRoom.Package_ID}, Raum_Grosse = {floorAndRoom.Room_Size}, Paket_Preis = '{packagePrice}'" +
                    $" where Auftrag_ID = {floorAndRoom.Order_ID} and where Floor_ID = {floorAndRoom.Floor_ID} and where Raum_ID = {floorAndRoom.Room_ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Nicht alle Daten wurden richtig eingegeben\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Der Auftrag konnte nicht gefunden werden\n" +
                                "4: Der Raum in diesem Auftrag wurde nicht gefunden\n" +
                                "========");
            }
        }
    }
}