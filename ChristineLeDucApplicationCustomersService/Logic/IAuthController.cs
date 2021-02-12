namespace Logic {
    public interface IAuthController {
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public bool LogedInSuccesfully { get; set; }
        public int LastInsertedId { get; set; }
        public string DatabasePath { get; set; }
    }
}
