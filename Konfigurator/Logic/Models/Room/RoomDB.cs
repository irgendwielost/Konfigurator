using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Room
{
     public class RoomDB
        {
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
                        MessageBox.Show("Die Raum-Tabelle Konnte nicht gefunden werden");
                    }
                }
    
                return null;
            }
            
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
                    MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                    "1: Der Raum konnte nicht gefunden werden\n" +
                                    "2: Die Tabelle konnte nicht gefunden werden");
                    return null;
                }
                return null;
            }
            
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
                    MessageBox.Show("Nicht alle Daten wurden richtig eingegeben");
                }
            }
            
            public static void DeleteRoom(int ID)
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
                    MessageBox.Show("Die Raum wurde nicht gefunden");
                }
            }
            
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
                    MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                    "1: Der Raum konnte nicht gefunden werden\n" +
                                    "2: Die Tabelle konnte nicht gefunden werden\n" +
                                    "3: Nicht alle Daten wurden richtig eingegeben");
                }
            }
        }
}