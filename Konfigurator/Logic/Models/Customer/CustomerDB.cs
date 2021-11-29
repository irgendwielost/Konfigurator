using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Customer
{
    public class CustomerDB
    {
        public static DataSet GetDataSetCustomer()
        {
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Kunden"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Kunde");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Die Kunden-Tabelle Konnte nicht gefunden werden");
                }
            }
            return null;
        }
        
        public static Customer GiveCustomerBack(int id)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand($"Select * from Firma where Kunden_ID={id}"
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Kunde konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
                return null;
            }

            return null;
        }
        
        public static void CreateCustomer(Customer customer)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Kunde_Name = {customer.Name}, Kunde_Plz = {customer.Plz}, Kunde_Strasse = {customer.Street}, Kunde_Ort = {customer.Region}, " +
                    $"Kunde_Tel = {customer.Tel}, Kunde_Email = {customer.Email}, Kunde_Aktuell = {customer.Recent})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nicht alle Daten wurden richtig eingegeben");
            }
        }
        
        public static void DeleteCustomer(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from Kunden where Kunden_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Der Kunde wurde nicht gefunden");
            }
        }
        
        public static void UpdateCustomer(Customer customer)
        {
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
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Kunde konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben");
            }
        }
    }
}