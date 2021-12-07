using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Employee
{
     public class EmployeeDB
    {
        // Return the Dataset with "Mitarbeiter" inside
        public static DataSet GetDataSetEmployee()
        {
            // Open the connection to the database
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Mitarbeiter"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Mitarbeiter");
                    return dataSet;
                }
                catch (Exception e)
                {
                    // If the above failed show following Error Message: 
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Mitarbeiter-Tabelle Konnte nicht gefunden werden\n" +
                                    "================");
                }
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */

        // Returns a "Mitarbeiter" by ID
        public static Employee GiveEmployeeBack(int id)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Mitarbeiter where Mitarbeiter_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    
                    return new Employee(id, reader.GetString(1), reader.GetString(2), reader.GetBoolean(3),
                        reader.GetDateTime(4),
                        reader.GetDateTime(5));
                }
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Mitarbeiter konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
                return null;
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Create a new "Mitarbeiter" (all data required) 
        public static void CreateEmployee(Employee employee)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Mitarbeiter (Mitarbeiter_ID, Mitarbeiter_Name, Mitarbeiter_Passwort," +
                    $" Mitarbeiter_Angestellt, Mitarbeiter_Angefangen, Mitarbeiter_Kuendigung) values (?, ?, ?, ?, ?, ?)"
                    , db.Connection);
                // gives the Values to the "?" 
                cmd.Parameters.AddRange( new[]
                {
                    new OleDbParameter("Mitarbeiter_ID",employee.ID),
                    new OleDbParameter("Mitarbeiter_Name",employee.Name),
                    new OleDbParameter("Mitarbeiter_Passwort",employee.Password),
                    new OleDbParameter("Mitarbeiter_Angestellt",employee.Working),
                    new OleDbParameter("Mitarbeiter_Angefangen",employee.Started),
                    new OleDbParameter("Mitarbeiter_Kuendigung",employee.Ended)
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Nicht alle Daten wurden richtig eingegeben\n" +
                                "================");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Whether the Employee is still working or not (SQL command needs to be changed!)
        public static void DeleteEmployee(int ID)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                // Change this!
                var cmd = new OleDbCommand(
                    $"Update Mitarbeiter set Mitarbeiter_Angestellt={false} where Mitarbeiter_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Mitarbeiter wurde nicht gefunden\n" +
                                "================");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Update the data on "Mitarbeiter" except ID
        public static void UpdateEmployee(Employee employee)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Mitarbeiter set Mitarbeiter_Name = {employee.Name},Mitarbeiter_Passwort = {employee.Password}," +
                    $" Mitarbeiter_Anglestellt = {employee.Working},Mitarbeiter_Angefangen = {employee.Started}, Mitarbeiter_Kuendigung = {employee.Ended} where Mitarbeiter_ID = {employee.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Nicht alle Daten wurden richtig eingegeben\n" +
                                "2: Der Mitarbeiter konnte nicht gefunden werden\n" +
                                "3: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
            }
        }
    }
}