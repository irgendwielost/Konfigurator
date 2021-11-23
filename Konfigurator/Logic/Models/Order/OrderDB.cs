using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Order
{
    public class OrderDB
    {
        public static DataSet GetDataSetOrder()
        {
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Auftrag"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Auftrag");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Die Auftrag-Tabelle Konnte nicht gefunden werden");
                }
            }

            return null;
        }
        
        public static Order GiveOrderBack(int id)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Auftrag where Auftrag_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Order(id, reader.GetDateTime(1), reader.GetBoolean(2), reader.GetDouble(3),
                        reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6),
                        reader.GetDouble(7), reader.GetDouble(8));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Mitarbeiter konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden");
                return null;
            }
            return null;
        }
        
        public static void CreateOrder(Order order)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            
            
            try
            {
                var cmd = new OleDbCommand(
                    $"select  count(Auftrag_ID) from EtageUndRaum where Auftrag_ID = {order.Id}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nicht alle Daten wurden richtig eingegeben");
            }
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Auftrag (Auftrag_ID = {order.Id}, Auftrag_Datum = {order.Datum}, Auftrag_NeuBesta = {order.NeuOrBestand}," +
                    $" Auftrag_Grosse = {order.Size}, Kunde_ID= {order.CustomerId}, Gebaude_ID= {order.HousingId}, Phase_ID = {order.PhaseId}," +
                    $" Faktor_Mult = {order.FactorMult}, Auftrag_PreisGesamt = {order.OverallPrice})"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nicht alle Daten wurden richtig eingegeben");
            }
        }
        
        public static void UpdateEmployee(Order order)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Mitarbeiter set Auftrag_Datum = {order.Datum}, Auftrag_NeuBesta = {order.NeuOrBestand}," +
                    $" Auftrag_Grosse = {order.Size}, Kunde_ID= {order.CustomerId}, Gebaude_ID= {order.HousingId}, Phase_ID = {order.PhaseId}," +
                    $" Faktor_Mult = {order.FactorMult}, Auftrag_PreisGesamt = {order.OverallPrice} where Order_ID = {order.Id}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ein Fehler ist Aufgetreten:\n" +
                                "1: Der Mitarbeiter konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben");
            }
        }
    }
}