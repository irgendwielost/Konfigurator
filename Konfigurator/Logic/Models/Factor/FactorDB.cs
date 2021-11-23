using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Factor
{
    public class FactorDB
    {
        public static DataSet GetDataSetFactor()
        {
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Faktor"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Faktor");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Faktor-Tabelle Konnte nicht gefunden werden\n" +
                                    "================");
                }
            }
            return null;
        }
        
        public static Factor GiveEmployeeBack(int id)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Faktor where Faktor_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Factor(id, reader.GetString(1), reader.GetFloat(2), reader.GetFloat(3));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Faktor konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "================");
                return null;
            }
            return null;
        }
        
        public static void CreateFactor(Factor factor)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"insert into Faktor (Faktor_ID = {factor.ID}, Faktor_Name = {factor.Name}" +
                    $" Faktor_Angestellt = {factor.Mult}, Faktor_Angefangen = {factor.Grosse})"
                    , db.Connection);
                cmd.Parameters.Add(factor.Name);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Nicht alle Daten wurden richtig eingegeben\n" +
                                "================");
            }
        }
        
        public static void DeleteFactor(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from Faktor where Faktor_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Der Faktor wurde nicht gefunden\n" +
                                "================");
            }
        }
        
        public static void UpdateFaktor(Factor factor)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Faktor set Faktor_ID = {factor.ID}, Faktor_Name = {factor.Name}" +
                    $" Faktor_Angestellt = {factor.Mult}, Faktor_Angefangen = {factor.Grosse} where Faktor_ID = {factor.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Der Faktor konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "================");
            }
        }
    }
}