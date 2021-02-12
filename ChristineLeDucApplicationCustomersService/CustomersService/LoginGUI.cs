using Logic;
using System;
using System.Windows.Forms;

namespace CustomersService {
    public partial class LoginGUI : Form {
        private MainGUI mainForm;
        private string database;
        public LoginGUI(MainGUI mainForm) {
            InitializeComponent();
            this.ErrorLabel.Text = "";
            this.mainForm = mainForm;
        }

        private void Aanmelden_Click(object sender, EventArgs e) {
            try {
                mainForm.loginContr = new AuthController(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text);
                if (mainForm.loginContr.LogedInSuccesfully) {
                    mainForm.button1.Text = "            " + mainForm.loginContr.WorkerName;
                    mainForm.ShowNewForm("home");
                }
            }
            catch (Exception ex) {
                this.ErrorLabel.Text = "Oeps er ging iets mis...";
            }           
        }

        private void FileSelect_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK) database = file.FileName;
            this.textBox3.Text = database;
        }
    }
}
