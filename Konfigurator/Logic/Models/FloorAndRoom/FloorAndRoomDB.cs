using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.FloorAndRoom
{
    public class FloorAndRoomDB
    {
        /// <summary>
        /// Finds ID with the name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>int ID</returns>
        
        /* Find  the FloorID with the Name*/
        public static int FloorIdToName(string Name)
        {
            /* Open the connection to the DataBase */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* Select the ID by searching with the Name */
                var cmd = new OleDbCommand($"Select Etage_ID from Etage where Etage_Name = {Name}"
                    , db.Connection);
                return Int32.Parse(cmd.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Die Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
            }
            return 0;
        }
        
        /* Find  the RoomID with the Name*/
        public static int RoomIdToName(string Name)
        {
            /* Open the connection to the DataBase */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* Select the ID by searching with the Name */
                var cmd = new OleDbCommand($"Select Raum_ID from Raum where Raum_Name = {Name}"
                    , db.Connection);
                return Int32.Parse(cmd.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Raum konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
            }
            return 0;
        }
        
        /* Find  the PackageID with the Name*/
        public static int PackageIdToName(string Name)
        {
            /* Open the connection to the DataBase */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* Select the ID by searching with the Name */
                var cmd = new OleDbCommand($"Select Paket_ID from Paket where Paket_Name = {Name}"
                    , db.Connection);
                return Int32.Parse(cmd.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
            }
            return 0;
        }
        
        /* ======================================================================================================================================================= */
        
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
                    MessageBox.Show("Die EtageUndRaum-Tabelle konnte nicht gefunden werden");
                }
            }
            return null;
        }
        
        /* DataSet to fill DataGrids */
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
                    var cmd = new OleDbDataAdapter($"select * from EtageUndRaum where Auftrag_ID = {id}"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "EtageUndRaum");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Die EtageUndRaum-Tabelle konnte nicht gefunden werden");
                }
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        /// <summary>
        /// Returns a full set of FloorAndRoom by looking for the different ID's
        /// </summary>
        /// <param name="ID"></param>
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: EtageUndRaum konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Auftrag und/oder Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Auftrag und/oder Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Auftrag und/oder Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Creates "Etage" with all attributes
        public static void CreateFloor(FloorAndRoom floorAndRoom)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into EtageUndRaum (Auftrag_ID, Etage_ID, Raum_ID, Paket_ID, Raum_Grosse) values ({floorAndRoom.Order_ID}, {floorAndRoom.Floor_ID}, {floorAndRoom.Room_ID}, " +
                    $"{floorAndRoom.Package_ID}, {floorAndRoom.Room_Size})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Nicht alle Daten wurden richtig eingegeben\n" +
                                "================");
            }
        }
    }
}