using Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic {
    public interface IDataController {
        List<string> GetAllMainSubjects();
        List<string> GetAllSubSubjects();
        List<string> GetAllWorkers();
        Order GetOrderByReference(string refer);
        int GetLastIdLogbook();
        void InsertNewLogbook(string data, int workerId);
    }
}
