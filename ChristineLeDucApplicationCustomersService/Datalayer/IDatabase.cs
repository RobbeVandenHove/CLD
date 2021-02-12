using Globals;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Datalayer {
    public interface IDatabase {
        string ConnString { get; set; }
        OleDbConnection Conn { get; set; }
        List<string> GetDataInListStringFormat(OleDbCommand sql);
        Order GetOrderByReference(OleDbCommand sql);
        int GetLastId(OleDbCommand sql);
        void Crud(OleDbCommand sql);
    }
}
