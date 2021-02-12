using System;

namespace Logic {
    public interface IAuthController {
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public bool LogedInSuccesfully { get; set; }
    }
}
