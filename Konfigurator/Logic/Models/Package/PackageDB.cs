using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Package
{
    public class PackageDB
    {
        public static DataSet GetDataSetPackage()
        {
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Paket"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Paket");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Die Paket-Tabelle Konnte nicht gefunden werden");
                }
            }

            return null;
        }
        
        public static Package GivePackageBack(int id)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Paket where Paket_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Package(id, reader.GetString(1), reader.GetBoolean(2));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Das Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
                return null;
            }
            return null;
        }
        
        public static void CreatePackage(Package package)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Paket (Paket_ID = {package.ID}, Paket_Name = {package.Name}, Paket_Verfuegbar = {package.Available})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nicht alle Daten wurden richtig eingegeben");
            }
        }
        
        public static void DeletePackage(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from Paket where Paket_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Der Paket wurde nicht gefunden");
            }
        }

        public static void UpdatePackage(Package package)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Paket set Paket_Name = {package.Name},Paket_Verfuegbar = {package.Available}," +
                    $"where Paket_ID = {package.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Das Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben");
            }
        }
    }
}