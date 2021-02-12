using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace Datalayer {
    public class AuthDb : MakeConnectionDb, IAuthDb {
        public string GetPassword(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    sql.Connection = Conn;
                    Conn.Open();
                    OleDbDataReader rdr = sql.ExecuteReader();
                    rdr.Read();
                    var result = (string)rdr["Passwoord"];
                    Conn.Close();
                    return result;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public string GetWorkerName(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    sql.Connection = Conn;
                    Conn.Open();
                    OleDbDataReader rdr = sql.ExecuteReader();
                    rdr.Read();
                    var result = (string)rdr["Voornaam"];
                    Conn.Close();
                    return result;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
