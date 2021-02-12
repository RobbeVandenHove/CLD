using Logic;
using System;
using System.Windows.Forms;

namespace CustomersService {
    public partial class MainGUI : Form {
        private Form activeForm = null;
        public IAuthController loginContr;
        public IDataController dataController;
        public MainGUI() {
            InitializeComponent();
            ShowNewForm("login");
            this.WindowState = FormWindowState.Maximized;
        }

        public void ShowNewForm(string formName) {
            if (activeForm != null) activeForm.Dispose();
            switch (formName) {
                case "home": activeForm = new HomeGUI(this) { TopLevel = false, TopMost = true }; break;
                case "login": activeForm = new LoginGUI(this) { TopLevel = false, TopMost = true }; break;
                case "addWorker": activeForm = new AddWorkerGUI(this) { TopLevel = false, TopMost = true }; break;
                case "newComplaint": activeForm = new NewComplaintGUI(this) { TopLevel = false, TopMost = true }; break;
            }
            MainPanel.Controls.Add(activeForm);
            activeForm.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (loginContr == null || !loginContr.LogedInSuccesfully) ShowNewForm("login");
            else if (loginContr.LogedInSuccesfully) ShowNewForm("home");
        }

        private void button4_Click(object sender, EventArgs e) {
            ShowNewForm("addWorker");
        }

        private void button3_Click(object sender, EventArgs e) {
            if (loginContr != null && loginContr.LogedInSuccesfully) ShowNewForm("newComplaint");
        }

        private void Logo_Click(object sender, EventArgs e) {
            if (loginContr == null || !loginContr.LogedInSuccesfully) ShowNewForm("login");
            else if (loginContr.LogedInSuccesfully) ShowNewForm("home");
        }
    }
}
