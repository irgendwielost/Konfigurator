using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Article
{
    public class ArticleDB
    {
        public static DataSet GetDataSetArticle()
        {
            // Open the connection to the database
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

    }
}