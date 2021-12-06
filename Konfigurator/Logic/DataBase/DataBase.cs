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
            Connection = new OleDbConnection(
                new OleDbConnectionStringBuilder
                {
                    Provider = "Microsoft.ACE.OLEDB.12.0",
                    DataSource = @"DatenBank.accdb"
                }.ConnectionString
            );
        }

        public void Dispose()
        {
            
            Connection.Close();
        }
    }
}