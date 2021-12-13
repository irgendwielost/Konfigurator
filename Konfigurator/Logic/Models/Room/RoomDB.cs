using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Room
{
     public class RoomDB
        {
            // Returns a Dataset filled with all Rooms
            public static DataSet GetDataSetRoom()
            {
                using (var db = new DataBase.DataBase())
                {
                    db.Connection.Open();
    
                    try
                    {
                        var cmd = new OleDbDataAdapter("select * from Raum"
                            , db.Connection);
                        DataSet dataSet = new DataSet();
                        cmd.Fill(dataSet, "Raum");
                        return dataSet;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                        "Die Raum-Tabelle Konnte nicht gefunden werden\n" +
                                        "========");
                    }
                }
    
                return null;
            }
            
            /* ======================================================================================================================================================= */
            
            // Returns a Room looking with the ID
            public static Room GiveRoomBack(int ID)
            {
                var db = new DataBase.DataBase();
                db.Connection.Open();
    
                try
                {
                    var cmd = new OleDbCommand(
                        $"Select * from Raum where Room_ID = {ID}"
                        , db.Connection);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Room(ID, reader.GetString(1));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "1: Der Raum konnte nicht gefunden werden\n" +
                                    "2: Die Tabelle konnte nicht gefunden werden\n" +
                                    "========");
                    return null;
                }
                return null;
            }
            
            /* ======================================================================================================================================================= */
            
            // creates a "Raum" in the Database 
            public static void CreateRaum(Room room)
            {
                var db = new DataBase.DataBase();
                db.Connection.Open();
                
                try
                {
                    var cmd = new OleDbCommand(
                        $"insert into Raum (Raum_ID = {room.ID}, Raum_Name = {room.Name}"
                        , db.Connection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Nicht alle Daten wurden richtig eingegeben\n" +
                                    "========");
                }
            }
            
            /* ======================================================================================================================================================= */
            
            
            /*public static void DeleteRoom(int ID)
            {
                var db = new DataBase.DataBase();
                db.Connection.Open();
                
                try
                {
                    var cmd = new OleDbCommand(
                        $"Delete * from Raum where Raum_ID={ID}"
                        , db.Connection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Die Raum wurde nicht gefunden\n" +
                                    "========");
                }
            }*/
            
            /* ======================================================================================================================================================= */
            
            // Updates a "Room" in the Database using Id to find it

            public static void UpdateRoom(Room room)
            {
                var db = new DataBase.DataBase();
                db.Connection.Open();
                
                try
                {
                    var cmd = new OleDbCommand(
                        $"Update Raum set Raum_Name={room.Name} where Raum_ID = {room.ID}"
                        , db.Connection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "1: Der Raum konnte nicht gefunden werden\n" +
                                    "2: Die Tabelle konnte nicht gefunden werden\n" +
                                    "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                    "========");
                }
            }
        }
}