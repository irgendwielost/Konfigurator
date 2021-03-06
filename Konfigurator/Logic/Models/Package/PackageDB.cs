using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Package
{
    public class PackageDB
    {
        /* Find  the "Paket_ID" with the Name */
        public static int PackageIdToName(string Name)
        {
            /* Open the connection to the DataBase */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* Select the ID by searching with the Name */
                var cmd = new OleDbCommand($"Select Paket_ID from Paket where Paket_Name = {Name}"
                    , db.Connection);
                return Int32.Parse(cmd.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }

            return 0;
        }

        /* ======================================================================================================================================================= */
        public static DataSet GetDataSetPackage()
        {
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Paket"
                        , db.Connection);
                    DataSet dataSet1 = new DataSet();
                    cmd.Fill(dataSet1, "Paket");
                    return dataSet1;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Paket-Tabelle Konnte nicht gefunden werden\n" +
                                    "========");
                    
                }
            }

            return null;
        }

        /* ======================================================================================================================================================= */

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
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Das Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }

            return null;
        }

        /* ======================================================================================================================================================= */

        public static void CreatePackage(Package package)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                        $"INSERT INTO Paket (Paket_ID, Paket_Name, Paket_Verfugbar) VALUES ({package.ID}, \"{package.Name}\",  {package.Available})"
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

        /// <summary>
        /// Marks a "Paket" as available or not
        /// </summary>
        /// <param name="ID"></param>
        public static void KillPackage(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Update Paket set Paket_Verfugbar = {false} where Paket_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Paket wurde nicht gefunden\n" +
                                "========");
            }
        }

        public static void RevivePackage(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Update Paket set Paket_Verfuegbar = {true} where Paket_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Paket wurde nicht gefunden\n" +
                                "========");
            }
        }

        /* ======================================================================================================================================================= */

        public static void UpdatePackage(Package package)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Update Paket set Paket_Name = '{package.Name}',Paket_Verfugbar = {package.Available} where Paket_ID = {package.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Das Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "========");
            }
        }
    }
}