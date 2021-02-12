using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CustomersService {
    public partial class HomeGUI : Form {
        private MainGUI mainForm;
        public HomeGUI(MainGUI mainForm) {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Afmelden_Click(object sender, EventArgs e) {
            mainForm.loginContr = null;
            mainForm.button1.Text = "             Login";
            mainForm.ShowNewForm("login");
        }
    }
}
