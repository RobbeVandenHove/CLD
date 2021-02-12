using System;
using System.Data.OleDb;
using Datalayer;

namespace Logic {
    public class AuthController : IAuthController {
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public bool LogedInSuccesfully { get; set; }
        private string Password { get; set; }
        public int LastInsertedId { get; set; }
        private IAuthDb db;
        public AuthController(string path, string firstName, string lastName, string email, string password, string admin) {
            try {
                this.db = new AuthDb();
                db.ConnString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + path + "; Persist Security Info = False;";
                if (admin == "ChristineLeDuc") {
                    CreateNewWorker(firstName, lastName, email, password);
                    GetLastInsertedId();
                    LogedInSuccesfully = false;
                }
                else throw new ArgumentException("Fout admin paswoord.");
            }
            catch (ArgumentException argex) {
                throw new ArgumentException(argex.Message);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
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
                throw new Exception(ex.Message);
            }            
        }
        private void CheckIfWorkerCanLogOn() {
            try {
                OleDbCommand sql = new OleDbCommand("SELECT [Paswoord] FROM Medewerker WHERE [MedewerkerId] = @id");
                sql.Parameters.AddWithValue("@id", WorkerId.ToString());
                var password = db.GetPassword(sql);
                if (EncryptPassword(Password) == password) LogedInSuccesfully = true;
                else LogedInSuccesfully = false;
            } 
            catch(Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }
        private void GetWorkerName() {
            try {
                OleDbCommand sql = new OleDbCommand("SELECT [Voornaam] FROM Medewerker WHERE [MedewerkerId] = @id");
                sql.Parameters.AddWithValue("@id", WorkerId.ToString());
                WorkerName = db.GetWorkerName(sql);
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }
        private void CreateNewWorker(string firstName, string lastName, string email, string password) {
            var encrypredPassword = EncryptPassword(password);
            try {
                OleDbCommand sql = new OleDbCommand("INSERT INTO Medewerker ([Voornaam], [Achternaam], [Emailadres], [Paswoord])" +
                                                    "VALUES (@firstName, @lastName, @email, @password)");
                sql.Parameters.AddWithValue("@firstName", firstName);
                sql.Parameters.AddWithValue("@lastName", lastName);
                sql.Parameters.AddWithValue("@email", email);
                sql.Parameters.AddWithValue("@password", encrypredPassword);
                db.Crud(sql);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        private void GetLastInsertedId() {
            OleDbCommand sql = new OleDbCommand("SELECT [MedewerkerId] FROM Medewerker ORDER BY [MedewerkerId] DESC");
            LastInsertedId = db.GetLastId(sql);
        }
        private string EncryptPassword(string password) {
            char[] pass = password.ToCharArray();
            var encrypted = "";
            for (int i = 0; i < pass.Length; i++) {
                char encrypt = (char) ((int)pass[i] + 14);
                encrypted += encrypt;
            }
            return encrypted;
        }
    }
}
