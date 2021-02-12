using System.Data.OleDb;

namespace Datalayer {
    public interface IAuthDb {
        string ConnString { get; set; }
        OleDbConnection Conn { get; set; }
        string GetPassword(OleDbCommand sql);
        string GetWorkerName(OleDbCommand sql);
        void Crud(OleDbCommand sql);
        int GetLastId(OleDbCommand sql);
    }
}
