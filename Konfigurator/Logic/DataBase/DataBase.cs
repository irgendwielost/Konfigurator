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
                var connectionString = new OleDbConnectionStringBuilder
                {
                    Provider = "Microsoft.ACE.OLEDB.12.0",
                    DataSource = @"BestellVerwaltung.accdb"
                }.ConnectionString;
                Connection = new OleDbConnection(connectionString);
            }
            catch (Exception e)
            {
                MessageBox.Show("The Database could not be found");
            }
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}