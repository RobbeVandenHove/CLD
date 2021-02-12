using Datalayer;
using Globals;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Logic {
    public class DataController : IDataController {
        private IDatabase db;
        public DataController(string path) {
            db = new Database();
            db.ConnString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + path + "; Persist Security Info = False;";
        }
        public List<string> GetAllMainSubjects() {
            OleDbCommand sql = new OleDbCommand("SELECT Naam FROM Hoofdonderwerp");
            try {
                return db.GetDataInListStringFormat(sql);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public List<string> GetAllSubSubjects() {
            OleDbCommand sql = new OleDbCommand("SELECT Naam FROM Subonderwerp");
            try {
                return db.GetDataInListStringFormat(sql);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public List<string> GetAllWorkers() {
            OleDbCommand sql = new OleDbCommand("SELECT Voornaam FROM Medewerker");
            try {
                return db.GetDataInListStringFormat(sql);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public Order GetOrderByReference(string refer) {
            OleDbCommand sql = new OleDbCommand($"SELECT * FROM OrderData WHERE [ReferentieNummer] = @refer");
            sql.Parameters.AddWithValue("@refer", refer);
            try {
                return db.GetOrderByReference(sql);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public int GetLastIdLogbook() {
            OleDbCommand sql = new OleDbCommand("SELECT Volgnummer FROM LogBoek ORDER BY Volgnummer DESC");
            try {
                var result = db.GetLastId(sql);
                return ++result;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public void InsertNewLogbook(string data, int workerId) {
            try {
                var dataArr = data.Split(";");
                OleDbCommand cmd = new OleDbCommand($"INSERT INTO LogBoek ([ReferentieNummer], [OrderId], [Index], [Hoofdonderwerp], [Subonderwerp], [Memo], [Datum], [MedewerkerId], [Status]) " +
                                                    $"VALUES (@ref, @order, @ind, @hoofd, @sub, @mem, @dtum, @mede, @stat)");
                cmd.Parameters.AddWithValue("@ref", dataArr[0]);
                cmd.Parameters.AddWithValue("@order", dataArr[1]);
                cmd.Parameters.AddWithValue("@ind", dataArr[2]);
                cmd.Parameters.AddWithValue("@hoofd", dataArr[3]);
                cmd.Parameters.AddWithValue("@sub", dataArr[4]);
                cmd.Parameters.AddWithValue("@mem", dataArr[5]);
                cmd.Parameters.AddWithValue("@dtum", dataArr[6]);
                cmd.Parameters.AddWithValue("@mede", workerId.ToString());
                cmd.Parameters.AddWithValue("@stat", dataArr[7]);
                db.Crud(cmd);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
