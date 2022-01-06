using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Factor
{
    public class FactorDB
    {
        
        // Return a Dataset with "Faktors" inside
        public static DataSet GetDataSetFactor()
        {
            // Opening a Connection to the Database (Has try in it already)
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Faktor"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Faktor");
                    return dataSet;
                }
                catch (Exception e)
                {
                    // If the above failed show following Error Message: 
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Faktor-Tabelle Konnte nicht gefunden werden\n" +
                                    "========");
                }
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Returns a "Faktor" by ID
        public static Factor GiveFactorBack(int id)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Faktor where Faktor_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Factor(id, reader.GetString(1), reader.GetFloat(2), reader.GetFloat(3), reader.GetBoolean(4));
                }
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Faktor konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Create a "Faktor" (All attributes required) 
        public static void CreateFactor(Factor factor)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"INSERT INTO Faktor (Faktor_ID, Faktor_Name, Faktor_Mult," +
                    $" Faktor_Grosse, Faktor_Benutzung) VALUES ({factor.ID}, \"{factor.Name}\", " +
                    $"{factor.Mult}, {factor.Grosse}, {factor.Used})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Nicht alle Daten wurden richtig eingegeben\n" +
                                "========"); 
            }
        }
        
        /* ======================================================================================================================================================= */
        
        /// <summary>
        /// Marks a "Faktor" as used or not
        /// </summary>
        /// <param name="ID"></param>
        
        //  Marks a "Faktor" as not to use
        public static void KillFactor(int ID)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Faktor set Faktor_Benutzung={false} where Faktor_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Faktor wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        // Marks a "Faktor" as used 
        public static void ReviveFactor(int ID)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Faktor set Faktor_Benutzung={true} where Faktor_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Faktor wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Updates a "Faktor" using all attributes except ID which is to search the "Faktor" 
        public static void UpdateFaktor(Factor factor)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Faktor set Faktor_Name = {factor.Name}" +
                    $" Faktor_Angestellt = {factor.Mult}, Faktor_Angefangen = {factor.Grosse}, Faktor_Benutzung = {factor.Used} where Faktor_ID = {factor.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Faktor konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "========");
            }
        }
    }
}