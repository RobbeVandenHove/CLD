using Logic;
using System;
using System.Windows.Forms;

namespace CustomersService {
    public partial class MainGUI : Form {
        private Form activeForm = null;
        public IAuthController loginContr;
        public MainGUI() {
            InitializeComponent();
            ShowNewForm("login");
        }

        public void ShowNewForm(string formName) {
            if (activeForm != null) activeForm.Dispose();
            switch (formName) {
                case "home": activeForm = new HomeGUI(this) { TopLevel = false, TopMost = true }; break;
                case "login": activeForm = new LoginGUI(this) { TopLevel = false, TopMost = true }; break;
            }
            MainPanel.Controls.Add(activeForm);
            activeForm.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (loginContr.LogedInSuccesfully) ShowNewForm("home");
            else ShowNewForm("login");
        }
    }
}
