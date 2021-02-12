using Globals;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace Datalayer {
    public class Database : MakeConnectionDb, IDatabase {
        public List<string> GetDataInListStringFormat(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    var result = new List<string>();
                    sql.Connection = Conn;
                    Conn.Open();
                    OleDbDataReader rdr = sql.ExecuteReader();
                    while (rdr.Read()) {
                        result.Add((string)rdr[0]);
                    }
                    Conn.Close();
                    Conn.Dispose();
                    return result;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }            
        }
        public Order GetOrderByReference(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    Order result = new Order();
                    sql.Connection = Conn;
                    Conn.Open();
                    OleDbDataReader rdr = sql.ExecuteReader();
                    rdr.Read();
                    result = new Order {
                        ReferentieNummer = rdr["ReferentieNummer"] == DBNull.Value ? "unknown" : (string)rdr["ReferentieNummer"],
                        OrderId = rdr["OrderId"] == DBNull.Value ? "unknown" : (string)rdr["OrderId"],
                        Factuuradres = rdr["Factuuradres"] == DBNull.Value ? "unknown" : (string)rdr["Factuuradres"],
                        FactuurPostcode = rdr["FactuurPostcode"] == DBNull.Value ? "unknown" : (string)rdr["FactuurPostcode"],
                        FactuurWoonplaats = rdr["FactuurWoonplaats"] == DBNull.Value ? "unknown" : (string)rdr["FactuurWoonplaats"],
                        FactuurLand = rdr["FactuurLand"] == DBNull.Value ? "unknown" : (string)rdr["FactuurLand"],
                        Verzendnaam = rdr["Verzendnaam"] == DBNull.Value ? "unknown" : (string)rdr["Verzendnaam"],
                        Verzendadres = rdr["Verzendadres"] == DBNull.Value ? "unknown" : (string)rdr["Verzendadres"],
                        VerzendPostcode = rdr["VerzendPostcode"] == DBNull.Value ? "unknown" : (string)rdr["VerzendPostcode"],
                        VerzendWoonplaats = rdr["VerzendWoonplaats"] == DBNull.Value ? "unknown" : (string)rdr["VerzendWoonplaats"],
                        VerzendLand = rdr["VerzendLand"] == DBNull.Value ? "unknown" : (string)rdr["VerzendLand"],
                        Telefoonnummer = rdr["Telefoonnummer"] == DBNull.Value ? "unknown" : (string)rdr["Telefoonnummer"],
                        Emailadres = rdr["Emailadres"] == DBNull.Value ? "unknown" : (string)rdr["Emailadres"],
                        Orderdatum = rdr["Orderdatum"] == DBNull.Value ? "unknown" : (string)rdr["Orderdatum"],
                        Ordertijd = rdr["Ordertijd"] == DBNull.Value ? "unknown" : (string)rdr["Ordertijd"]
                    };
                    Conn.Close();
                    Conn.Dispose();
                    return result;
                }
            }
            catch (Exception ex) {
                throw new Exception("Order is niet meer actief");
            }
        }
        public int GetLastId(OleDbCommand sql) {
            try {
                using (Conn = new OleDbConnection(ConnString)) {
                    int result = -1;
                    sql.Connection = Conn;
                    Conn.Open();
                    OleDbDataReader rdr = sql.ExecuteReader();
                    while (rdr.Read()) {
                        result = (int)rdr[0];
                    }
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
    }
}
