using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Models.Floor
{
    public class FloorDB
    {
        public static DataSet GetDataSetFloor()
        {
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Etage"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Etage");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Etage-Tabelle Konnte nicht gefunden werden\n" +
                                    "================");
                }
            }

            return null;
        }
        
        public static Floor GiveFlooreBack(int id)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Etage where Etage_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Floor(id, reader.GetString(1));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Die Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
                return null;
            }
            return null;
        }
        
        public static void CreateFloor(Floor floor)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Etage (Etage_ID = {floor.ID}, Etage_Name = {floor.Name}"
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
        
        public static void DeleteFloor(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from Etage where Etage_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Die Etage wurde nicht gefunden\n" +
                                "================");
            }
        }
        
        public static void UpdateFloor(Floor floor)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Etage set Etage_Name = {floor.Name} where Etage_ID = {floor.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Die Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "================");
            }
        }
    }
}