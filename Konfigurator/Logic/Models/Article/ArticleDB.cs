using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Article
{
    public class ArticleDB
    {
        // Return a Dataset with "Artikel" inside
        public static DataSet GetDataSetArticle()
        {
            // Opening a Connection to the Database (Has try in it already)
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Artikel"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Artikel");
                    return dataSet;
                }
                catch (Exception e)
                {
                    // If the above failed show following Error Message: 
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Artikel-Tabelle Konnte nicht gefunden werden\n" +
                                    "========");
                }
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        /* Find  the "Artikel_ID" with the Name*/
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
                cmd.ExecuteNonQuery();
                return Int32.Parse(cmd.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n"+
                                "1: Das Paket konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }
            return 0;
        }
        
        /* ======================================================================================================================================================= */
        
        // Returns a "Artikel" by ID
        public static Article GiveArticleBack(int id)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Artikel where Artikel_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Article(id, reader.GetString(1), reader.GetFloat(2), reader.GetBoolean(3));
                }
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Artikel konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Create a "Artikel" (All attributes required) 
        public static void CreateArticle(Article article)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"INSERT INTO Artikel (Artikel_ID, Artikel_Name,Artikel_Preis, Artikel_Verfugbar) VALUES ({article.ID}, \"{article.Name} \" , {article.Price},{article.Available})"
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
        
        // Marks a "Artikel" as not in use
        public static void KillArticle(int ID)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"UPDATE Artikel SET Artikel_Verfugbar={false} WHERE Artikel_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Artikel wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        // Marks a "Artikel" as in use
        public static void ReviveArticle(int ID)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Artikel set Artikel_Verfuegbar={true} where Artikel_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Artikel wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Updates a "Artikel" using all attributes except ID which is to search the "Artikel" 
        public static void UpdateArticle(Article article)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Artikel set Artikel_Name = '{article.Name}', Artikel_Preis = {article.Price}, Artikel_Verfugbar = {article.Available} where Artikel_ID = {article.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Nicht alle Daten wurden richtig eingegeben\n" +
                                "2: Der Artikel konnte nicht gefunden werden\n" +
                                "3: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Return the Price of the Article
        public static double PriceOfArticle(int id)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select Artikel_Preis from Artikel where Artikel_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read() && reader.HasRows)
                {
                    return reader.GetDouble(0);
                }

                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Artikel Preis konnte nicht gefunden werden\n" +
                                "========");
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Ein unbekannter Fehler ist Aufgetreten\n" +
                                "========");
            }
            
            return 0;
        }

        
    }
}