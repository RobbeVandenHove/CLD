using System;
using System.Data.OleDb;

namespace Datalayer {
    public class AuthDb : MakeConnectionDb, IAuthDb {
 
        public string GetPassword(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    sql.Connection = Conn;
                    Conn.Open();
                    OleDbDataReader rdr = sql.ExecuteReader();
                    rdr.Read();
                    var result = (string)rdr["Paswoord"];
                    Conn.Close();
                    Conn.Dispose();
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
                    Conn.Dispose();
                    return result;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public void Crud(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    sql.Connection = Conn;
                    Conn.Open();
                    sql.ExecuteNonQuery();
                    Conn.Close();
                    Conn.Dispose();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public int GetLastId(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    sql.Connection = Conn;
                    Conn.Open();
                    var id = sql.ExecuteScalar() == null ? 0 : (int)sql.ExecuteScalar();
                    Conn.Close();
                    Conn.Dispose();
                    return id;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
