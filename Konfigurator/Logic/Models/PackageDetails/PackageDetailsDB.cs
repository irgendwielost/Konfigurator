using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.PackageDetails
{
    /*  */
    public class PackageDetailsDB
    {
        
        /* DataSet to fill DataGrids and such */
        public static DataSet GetDataSetPackageDetails()
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
                    var cmd = new OleDbDataAdapter("select * from PaketDetails"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "PaketDetails");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                    "Die PaketDetails-Tabelle Konnte nicht gefunden werden\n" +
                                    "================");
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
                                "================");
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
                                "================");
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
                        reader.GetBoolean(4));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "1: PaketDetails konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
                return null;
            }

            return null;
        }

        /* Gives back PackageDetails with the ArticleID */
        public static PackageDetails GivePackageDetialsBackByArticle(int id)
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
                        reader.GetBoolean(4));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "1: PaketDetails konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
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
                /* SQL-Command to insert everything into PackageDetails */
                var cmd = new OleDbCommand(
                    $"insert into PaketDetails (Paket_ID,Artikel_ID, Artikel_Menge, Artikel_Preis, Preis_Aktuell)" +
                    $" Values({packageDetails.Package_ID}, {packageDetails.Article_ID}, {packageDetails.ArtMenge}, {packageDetails.Preis}, {packageDetails.Recent})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "Nicht alle Daten wurden richtig eingegeben\n" +
                                "================");
            }
        }

        /* ======================================================================================================================================================= */

        /* Add function to find out if the Price is still the same */

        public static bool ArticlePriceChecker(PackageDetails packageDetails)
        {
            /* Database Connection being opened */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            PackageDetails detailsArticle = null;
            Article.Article article = null;

            try
            {
                /* SQL-Command to select everything from this ArticleID */
                var cmd = new OleDbCommand(
                    $"Select * from PaketDetails where Artikel_ID = {packageDetails.Article_ID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    detailsArticle = new PackageDetails(reader.GetInt32(0), packageDetails.Article_ID,
                        reader.GetInt32(2), reader.GetDouble(3), reader.GetBoolean(4));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Artikel konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
            }

            try
            {
                /* SQL-Command to select everything from this ArticleID */
                var cmd = new OleDbCommand(
                    $"Select * from Artikel where Artikel_ID = {packageDetails.Article_ID}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    article = new Article.Article(packageDetails.Article_ID , reader.GetString(1), reader.GetDouble(2), reader.GetBoolean(3));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Artikel konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
            }

            if (detailsArticle.Preis == article.Price)
            {
                return true;
            }
            return false;
        }
    }
}