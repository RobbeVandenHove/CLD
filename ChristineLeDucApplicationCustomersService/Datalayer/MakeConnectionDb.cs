using System.Data.OleDb;

namespace Datalayer {
    public class MakeConnectionDb {
        public string ConnString { get; set; }
        public OleDbConnection Conn { get; set; }
    }
}
