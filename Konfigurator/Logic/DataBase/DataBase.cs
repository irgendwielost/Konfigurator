using System;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.DataBase
{
    public class DataBase : IDisposable
    {
        public OleDbConnection Connection { get; }

        public DataBase()
        {
            try
            {
                Connection = new OleDbConnection(
                    new OleDbConnectionStringBuilder
                    {
                        Provider = "Microsoft.ACE.OLEDB.12.0",
                        DataSource = @"DatenBank.accdb"
                    }.ConnectionString
                );
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Die Datenbank konnte nicht gefunden werden\n" +
                                "================");
            }
            
        }

        public void Dispose()
        {
            
            Connection.Close();
        }
    }
}