using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Customer
{
    public class CustomerDB
    {
        
        // Return the Dataset with "Kunden" inside
        public static DataSet GetDataSetCustomer()
        {
            // Open the connection to the database
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Kunde"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Kunde");
                    return dataSet;
                }
                catch (Exception e)
                {
                    // If the above failed show following Error Message: 
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Kunde-Tabelle Konnte nicht gefunden werden\n" +
                                    "========");
                }
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Returns a "Kunde" by ID
        public static Customer GiveCustomerBack(int id)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand($"Select * from Kunde where Kunde_ID={id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Customer(id, reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), 
                        reader.GetString(5), reader.GetString(6), reader.GetBoolean(7));
                }
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Kunde konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }

            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Create a new "Kunde" (all data required) 
        public static void CreateCustomer(Customer customer)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Kunde (Kunde_Name, Kunde_Plz, Kunde_Strasse, Kunde_Ort, " +
                    $"Kunde_Tel, Kunde_Email, Kunde_Aktuell) value (?, ?, ?, ?, ?, ?)"
                    , db.Connection);
                cmd.Parameters.AddRange( new[]
                {
                    new OleDbParameter("Mitarbeiter_ID", customer.ID),
                    new OleDbParameter("Mitarbeiter_ID", customer.ID),
                    new OleDbParameter("Mitarbeiter_ID", customer.ID),
                    new OleDbParameter("Mitarbeiter_ID", customer.ID),
                    new OleDbParameter("Mitarbeiter_ID", customer.ID),
                    new OleDbParameter("Mitarbeiter_ID", customer.ID),
                });
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
        /// Marks "Kunde" as recent or not
        /// </summary>
        /// <param name="ID"></param>
        
        // Marks "kunde" as not Recent
        public static void KillCustomer(int ID)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Kunde set Kunde_Aktuell = {false} where Kunde_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Kunde wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        // Marks "Kunde" as recent 
        public static void ReviveCustomer(int ID)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Kunde set Kunde_Aktuell = {true} where Kunde_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Kunde wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        /* ======================================================================================================================================================= */

        // Update a "Kunde" by ID
        public static void UpdateCustomer(Customer customer)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Kunde set Kunde_Name = {customer.Name}, Kunde_Plz = {customer.Plz}, Kunde_Strasse = {customer.Street}, Kunde_Ort = {customer.Region}, " +
                    $"Kunde_Tel = {customer.Tel}, Kunde_Email = {customer.Email}, Kunde_Aktuell = {customer.Recent} where Kunde_ID =  = {customer.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Nicht alle Daten wurden richtig eingegeben\n" +
                                "2: Der Kunde konnte nicht gefunden werden\n" +
                                "3: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }
        }
    }
}