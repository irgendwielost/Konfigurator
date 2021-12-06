using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;

namespace Konfigurator.Logic.Models.Housing
{
    public class HousingDB
    {
        
        // Returns Dataset with Gebaude inside
        public static DataSet GetDataSetHousing()
        {
            // Database connection opens 
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Gebaude"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Gebaude");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Die Gebaude-Tabelle Konnte nicht gefunden werden");
                }
            }

            return null;
        }
        
        /* ======================================================================================================================================================= */

        public static List<Housing> ReturnHousingInList()
        {
            // Database connection opens 
            var db = new DataBase.DataBase();
            db.Connection.Open();

            List<Housing> listOfHouses = null; 
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Gebaude"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (var VARIABLE in reader)
                    {
                        Housing housingObject = new Housing(reader.GetInt32(0), reader.GetString(1));
                        listOfHouses.Add(housingObject);
                    }
                }
                return listOfHouses;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Das Gebaude konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Returns "Gebaude" by looking for ID 
        public static Housing GiveHousingBack(int id)
        {
            // Database connection opens 
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Gebaude where Gebaude_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Housing(id, reader.GetString(1));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Das Gebaude konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
                return null;
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Creates Gebaude with all attributes 
        public static void CreateHousing(Housing housing)
        {
            // Database connection opens 
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Gebaude (Gebaude_ID, Gebaude_Name) values ({housing.ID}, {housing.ID})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nicht alle Daten wurden richtig eingegeben");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Deletes Gebaude by looking for ID
        public static void DeleteHousing(int ID)
        {
            // Database connection opens 
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from Gebaude where Gebaude_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Das Gebaude wurde nicht gefunden");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        //Updates Gebaude by looking for ID 
        public static void UpdateHousing(Housing housing)
        {
            // Database connection opens 
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Gebaude set Gebaude_Name = {housing.Name}" +
                    $"where Gebaude_ID = {housing.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Das Gebaude konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben");
            }
        }
    }
}