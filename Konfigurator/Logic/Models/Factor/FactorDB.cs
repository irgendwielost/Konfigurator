using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
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
                    $"Update Faktor set Faktor_Name = '{factor.Name}', Faktor_Mult = {factor.Mult}, Faktor_Grosse = {factor.Grosse}, Faktor_Benutzung = {factor.Used} where Faktor_ID = {factor.ID}"
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
                throw e;
            }
        }
        
        // gets the Multiplier for the Order
        /*
        public static double GetMultOfFactor(double currentPrice)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();

            List<double> allGrosse = new List<double>();

            try
            {
                // get all the Fakto_Grosse which are used 
                var cmd = new OleDbCommand($"select Faktor_Grosse from Faktor where Faktor_Grosse={true}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                // put all the Grosse in a list
                if (reader.Read() && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        allGrosse.Add(reader.GetDouble(0));
                    }
                }
                else
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Kein Faktor konnte gefunden werden\n" +
                                    "========");
                }
                // sort the list small to big
                allGrosse.OrderBy(d => d);
                double currentGrosse = 0;
                // get the smallest number closest to the actual price
                foreach (var grosse in allGrosse)
                {
                    if (grosse <= currentPrice)
                    {
                        currentGrosse = grosse;
                    }
                }

                try
                {
                    // search for the Multiplier
                    var cmd2 = new OleDbCommand(
                        $"select Faktor_Mult from Faktor where Faktor_Grosse={currentGrosse} and Faktor_Benutzung={true}"
                        , db.Connection);
                    var readerMult = cmd2.ExecuteReader();
                    return readerMult.GetDouble(0);
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Ein unbekannter Fehler ist aufgetreten\n" +
                                    "========");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Faktor konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "========");
            }

            return 0;
        }
        
        */
        
        // gets the Multiplier for the Order
        public static double GetMultOfFactor(double currentPrice)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();

            List<double> allGrosse = new List<double>();

            try
            {
                // get all the Fakto_Grosse which are used 
                var cmd = new OleDbCommand($"select Faktor_Grosse from Faktor where Faktor_Grosse={true}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                // put all the Grosse in a list
                if (reader.Read() && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        allGrosse.Add(reader.GetDouble(0));
                    }
                }
                else
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Kein Faktor konnte gefunden werden\n" +
                                    "========");
                }
                // sort the list small to big
                allGrosse.OrderBy(d => d);
                double currentGrosse = 0;
                // get the smallest number closest to the actual price
                foreach (var grosse in allGrosse)
                {
                    if (grosse <= currentPrice)
                    {
                        currentGrosse = grosse;
                    }
                }

                try
                {
                    // search for the Multiplier
                    var cmd2 = new OleDbCommand(
                        $"select Faktor_Mult from Faktor where Faktor_Grosse={currentGrosse} and Faktor_Benutzung={true}"
                        , db.Connection);
                    var readerMult = cmd2.ExecuteReader();
                    return readerMult.GetDouble(0);
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Ein unbekannter Fehler ist aufgetreten\n" +
                                    "========");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Faktor konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "========");
            }

            return 0;
        }
    }
}