using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace Konfigurator.Logic.Models.Phase
{
     public class PhaseDB
    {
        // Returns a Dataset filled with all Phases
        public static DataSet GetDataSetPhasen()
        {
            // opening the Database
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("select * from Phasen"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Phasen");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Phasen-Tabelle Konnte nicht gefunden werden\n" +
                                    "========");
                }
            }

            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // Returns a Phase looking with the ID
        public static Phase GivePhaseBack(int id)
        {
            // Database will be opened 
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                var cmd = new OleDbCommand(
                    $"Select * from Phasen where Phase_ID = {id}"
                    , db.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Phase(id, reader.GetString(1));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Die Phase konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                return null;
            }
            return null;
        }
        
        /* ======================================================================================================================================================= */
        
        // creates a "Phase" in the Database 
        public static void CreatePhase(Phase phase)
        {
            // opening the Database
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"INSERT INTO Phasen (Phase_ID, Phase_Name) VALUES ({phase.ID}, '{phase.Name}')"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Nicht alle Daten wurden richtig eingegeben\n" +
                                "========");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // deletes a "Phase" from the Database
        public static void DeleteFactor(int ID)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Delete * from Phasen where Phase_ID={ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Die Phase wurde nicht gefunden\n" +
                                "========");
            }
        }
        
        /* ======================================================================================================================================================= */
        
        // Updates a "Phase" in the Database using Id to find it
        public static void UpdatePhase(Phase phase)
        {
            var db = new DataBase.DataBase();
            db.Connection.Open();
            
            try
            {
                var cmd = new OleDbCommand(
                    $"Update Phasen set Phase_Name = '{phase.Name}' where Phase_ID = {phase.ID}"
                    , db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Die Phase konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "3: Nicht alle Daten wurden richtig eingegeben\n" +
                                "========");
            }
        }
        
        // Returns a Dataset filled with all Phase names
        public static DataSet GetDataSetPhasenName()
        {
            // opening the Database
            using (var db = new DataBase.DataBase())
            {
                db.Connection.Open();

                try
                {
                    var cmd = new OleDbDataAdapter("SELECT Phase_Name FROM Phasen"
                        , db.Connection);
                    DataSet dataSet = new DataSet();
                    cmd.Fill(dataSet, "Phasen");
                    return dataSet;
                }
                catch (Exception e)
                {
                    MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                    "Die Phasen-Tabelle Konnte nicht gefunden werden\n" +
                                    "========");
                }
            }

            return null;
        }
        
        // Returns a Phase looking with the ID
        /* Find  the "Etage_ID" in "Etage" with the Name */
        public static int PhaseIdByName(string name)
        {
            /* Open the connection to the DataBase */
            var db = new DataBase.DataBase();
            db.Connection.Open();

            try
            {
                /* Select the ID by searching with the Name */
                var cmd = new OleDbCommand($"Select Phase_ID from Phasen where Phase_Name = '{name}'"
                    , db.Connection);
                return Int32.Parse(cmd.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "1: Die Etage konnte nicht gefunden werden\n" +
                                "2: Die Tabelle konnte nicht gefunden werden\n" +
                                "========");
                throw e;
            }
            
        }


    }
}