using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using Datalayer;

namespace Logic {
    public class AuthController : IAuthController {
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public bool LogedInSuccesfully { get; set; }
        private string Password { get; set; }
        private IAuthDb db;
        public AuthController(string id, string password, string path) {
            try {
                this.db = new AuthDb();
                db.ConnString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + path + "; Persist Security Info = False;";
                WorkerId = Int32.Parse(id);
                Password = password;
                CheckIfWorkerCanLogOn();
                if (LogedInSuccesfully) GetWorkerName();
            }
            catch(Exception ex) {
                throw new ArgumentException("Vul een correct id in.");
            }            
        }
        private void CheckIfWorkerCanLogOn() {
            try {
                OleDbCommand sql = new OleDbCommand("SELECT Passwoord FROM Medewerker WHERE MedewerkerId = @id");
                sql.Parameters.AddWithValue("@id", WorkerId);
                var password = db.GetPassword(sql);
                if (Password == password) LogedInSuccesfully = true;
                else LogedInSuccesfully = false;
            } 
            catch(Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }
        private void GetWorkerName() {
            try {
                OleDbCommand sql = new OleDbCommand("SELECT Voornaam FROM Medewerker WHERE MedewerkerId = @id");
                sql.Parameters.AddWithValue("@id", WorkerId);
                WorkerName = db.GetWorkerName(sql);
                
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
