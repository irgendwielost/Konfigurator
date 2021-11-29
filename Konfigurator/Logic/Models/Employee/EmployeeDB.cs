using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Employee
{
     public class EmployeeDB
    {
        public static DataSet GetDataSetEmployee()
        {
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
                    MessageBox.Show("Die Mitarbeiter-Tabelle Konnte nicht gefunden werden");
                }
            }

            return null;
        }

        public static Employee GiveEmployeeBack(int id)
        {
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Mitarbeiter konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
                return null;
            }
            return null;
        }
        
        public static void CreateEmployee(Employee employee)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Mitarbeiter (Mitarbeiter_ID = {employee.ID}, Mitarbeiter_Name = {employee.Name}, Mitarbeiter_Passwort = {employee.Password}," +
                    $" Mitarbeiter_Angestellt = {employee.Working}, Mitarbeiter_Angefangen = {employee.Started}, Mitarbeiter_Kuendigung = {employee.Ended})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nicht alle Daten wurden richtig eingegeben");
            }
        }
        
        public static void DeleteEmployee(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from Mitarbeiter where Mitarbeiter_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Der Mitarbeiter wurde nicht gefunden");
            }
        }
        
        public static void UpdateEmployee(Employee employee)
        {
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Mitarbeiter konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben");
            }
        }
    }
}