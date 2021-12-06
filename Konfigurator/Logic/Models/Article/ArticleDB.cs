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
                                    "================");
                }
            }
            return null;
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
                                "================");
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
                    $"insert into Artikel (Artikel_ID, Artikel_Name," +
                    $" Artikel_Angestellt, Artikel_Angefangen) values ({article.ID}, {article.Name}, {article.Price}, {article.Available})"
                    , db.Connection);
                cmd.Parameters.Add(article.Name);
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
        
        // (Needs to be Changed!) Has to mark a "Artikel" as not to use
        public static void DeleteArticle(int ID)
        {
            // Opening a Connection to the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Artikel set Artikel_Verfuegbar={false} where Artikel_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Artikel wurde nicht gefunden\n" +
                                "================");
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
                    $"Update Artikel set Artikel_Name = {article.Name}" +
                    $" Artikel_Preis = {article.Price}, Artikel_Verfuegabar = {article.Available} where Artikel_ID = {article.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // If the above failed show following Error Message: 
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Artikel konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "================");
            }
        }
    }
}