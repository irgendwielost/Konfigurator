using System;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.EmployeeToOrder
{
    public class EmployeeToOrderDB
    {
        /// <summary>
        /// Gives back EmployeeToOrder by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        
        /* Gives back a specific EmployeeToOrder with the Aurftrag_ID */
        public static EmployeeToOrder GiveEmployeeToOrderDetailsByOrder(int ID)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this Auftrag_ID */
                var cmd = new OleDbCommand(
                    $"Select * from AuftragZuMitarbeiter where Auftrag_ID = {ID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new EmployeeToOrder(ID, reader.GetInt32(1));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Auftrag konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }
            return null;
        }

        
        /* Gives back a specific EmployeeToOrder with the Mitarbeiter_ID */
        public static EmployeeToOrder GiveEmployeeToOrderBackByEmployee(int id)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this Mitarbeiter_ID */
                var cmd = new OleDbCommand(
                    $"Select * from AuftragZuMitarbeiter where Mitarbeiter_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new EmployeeToOrder(reader.GetInt32(0),id);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Mitarbeiter konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        /* To Add an Employee to an order along with all other Attributes */
        public static void CreateEmployeeToOrderDetails(EmployeeToOrder employeeToOrder)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select Mitarbeiter_ID from Mitarbeiter where Mitarbeiter_ID = {employeeToOrder.EmployeeID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Mitarbeiter wurde nicht gefunden\n" +
                                "========");
                return;
            }
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Select Auftrag_ID from Auftrag where Auftrag_ID = {employeeToOrder.OrderID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Auftrag wurde nicht gefunden\n" +
                                "========");
                return;
            }

            try
            {
                /* SQL-Command to insert everything into AuftragZuMitarbeiter */
                var cmd = new OleDbCommand(
                    $"insert into AuftragZuMitarbeiter (Auftrag_ID, Mitarbeiter_ID) values ({employeeToOrder.OrderID}, {employeeToOrder.EmployeeID})"
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
        
        // Delete a "AuftragZuMitarbeiter" by Auftrag_ID
        public static void DeleteCustomerByOrder(int ID)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from AuftragZuMitarbeiter where Auftrag_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Die Auftrag ID wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        // Delete a "AuftragZuMitarbeiter" by Mitarbeiter_ID
        public static void DeleteCustomerByEmployee(int ID)
        {
            // Open the connection to the database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from AuftragZuMitarbeiter where Mitarbeiter_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Die Mitarbeiter ID wurde nicht gefunden\n" +
                                "========");
            }
        }
    }
}