using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Documents;
using Konfigurator.Logic.Models.Article;

namespace Konfigurator.Logic.Models.PackageDetails
{
    /*  */
    public class PackageDetailsDB
    {
        
        /* DataSet to fill DataGrids and such */
        public static DataSet GetDataSetPackageDetails(int id)
        {
            /* Database Connection being opened */
            using (var db = new DataBase.DataBase())
            {
                /* Database Connection being opened */
                db.Connection.Open();

                /* in case the Table is not found try is used */
                try
                {
                    /* in the PacketDetails all entries will be selected and added to the DataSet (consider changing this for not all but just all recently) */
                    var cmd = new OleDbDataAdapter($"SELECT * FROM PaketDetails WHERE Paket_ID  = {id}"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "PaketDetails");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                    "Die PaketDetails-Tabelle Konnte nicht gefunden werden :(\n" +
                                    "========");
                    Console.WriteLine(e);
                   
                }
            }

            return null;
        }

        /* ======================================================================================================================================================= */
        
        /* Find  the "Paket_ID" with the Name*/
        public int PackageIdToName(string Name)
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
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "1: Der Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }
            return 0;
        }

        /* Find  the "Artikel_ID" with the Name*/
        public int ArticleIdToName(string Name)
        {
            /* Open the connection to the DataBase */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* Select the ID by searching with the Name */
                var cmd = new OleDbCommand($"Select Artikel_ID from Artikel where Artikel_Name = {Name}"
                    , db.Connection);
                return Int32.Parse(cmd.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "1: Der Artikel konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }

            return 0;
        }

        /* ======================================================================================================================================================= */

        /* Gives back PackageDetails with the PackageID */
        public static PackageDetails GivePackageDetailsBackByPackage(int ID)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this PackageID */
                var cmd = new OleDbCommand(
                    $"Select * from PaketDetails where Paket_ID = {ID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new PackageDetails(ID, reader.GetInt32(1), reader.GetInt32(2), reader.GetDouble(3),
                        reader.GetString(4) ,reader.GetBoolean(5));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "1: PaketDetails konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }

            return null;
        }

        /* Gives back PackageDetails with the ArticleID */
        public static PackageDetails GivePackageDetailsBackByArticle(int id)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* SQL-Command to select everything from this ArticleID */
                var cmd = new OleDbCommand(
                    $"Select * from PaketDetails where Artikel_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new PackageDetails(reader.GetInt32(0), id, reader.GetInt32(2), reader.GetDouble(3),
                        reader.GetString(4) ,reader.GetBoolean(5));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "1: PaketDetails konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }

            return null;
        }

        /* ======================================================================================================================================================= */

        /* To Add an Article to a Package along with all other Attributes */
        public static void CreatePackageDetails(PackageDetails packageDetails)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                double articlePrice = ArticleDB.PriceOfArticle(packageDetails.Article_ID);
                /* SQL-Command to insert everything into PackageDetails */
                var cmd = new OleDbCommand(
                    $"insert into PaketDetails (Paket_ID,Artikel_ID, Artikel_Menge, Artikel_Preis, Artikel_Name, Preis_Aktuell) Values({packageDetails.Package_ID}, {packageDetails.Article_ID}, {packageDetails.ArtMenge},  '{packageDetails.Price}','{packageDetails.ArticleName}', {packageDetails.Recent})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "Nicht alle Daten wurden richtig eingegeben\n" +
                                "========");
            }
        }

        /* ======================================================================================================================================================= */

        public static void UpdatePackageDetails(PackageDetails packageDetails)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                double articlePrice = ArticleDB.PriceOfArticle(packageDetails.Article_ID);
                if (articlePrice != 0)
                {
                    var cmd = new OleDbCommand(
                        $"Update PaketDetails set Artikel_ID = {packageDetails.Article_ID}, Artikel_Menge = {packageDetails.ArtMenge} where Paket_ID = {packageDetails.Package_ID}"
                        , db.Connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        /* ======================================================================================================================================================= */

        // Get the Price of all Articles within this Package
        public static double PackageDetailsGetPrice(int id)
        {
            // open DataBase Connection
            var db = new DataBase.DataBase();
            db.Connection.Open();
            double totalPrice = 0;
            List<int> articleIds = new List<int>();
            // Select all "Artikel_ID" in "PaketDetails" and add them to articeIds
            try
            {
                var cmd = new OleDbCommand(
                    $"Select Artikel_ID from PaketDetails where Paket_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        articleIds.Add(reader.GetInt32(0));
                    }
                }
                else
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Das Paket hat keine Artikel\n" +
                                    "========");
                }

                // For each id in articleIds go to the id and get the Price Then add it so the totalPrice
                foreach (int Ids in articleIds)
                {
                    totalPrice += ArticleDB.PriceOfArticle(Ids);
                }
                return totalPrice;
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Ein unbekannter Fehler ist Aufgetreten\n" +
                                "========");
            }

            return 0;
        }
        
          
        // Deletes Gebaude by looking for ID
        public static void DeleteArticleFromPackage(int packageID, int articleID)
        {
            // Database connection opens 
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from PaketDetails where Paket_ID={packageID} and Artikel_ID = {articleID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Artikel in diesem Paket wurde nicht gefunden\n" +
                                "========");
            }
        }

    }
}