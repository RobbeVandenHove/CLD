using Logic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomersService {
    public partial class AddWorkerGUI : Form {
        private MainGUI mainForm;
        private List<TextBox> boxes;
        public AddWorkerGUI(MainGUI mainForm) {
            InitializeComponent();
            this.textBox4.PasswordChar = '*';
            this.textBox5.PasswordChar = '*';
            boxes = new List<TextBox>() {
                this.textBox1,
                this.textBox2,
                this.textBox3,
                this.textBox4,
                this.textBox5,
                this.textBox6,
            };
            this.ErrorLabel.Text = "";
            this.mainForm = mainForm;
        }

        private void FileSelect_Click(object sender, System.EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK) this.textBox6.Text = file.FileName;
        }

        private void button1_Click(object sender, System.EventArgs e) {
            try {
                if (CheckIfFilledIn()) {
                    mainForm.loginContr = new AuthController(this.textBox6.Text,
                        this.textBox1.Text,
                        this.textBox2.Text,
                        this.textBox3.Text,
                        this.textBox4.Text,
                        this.textBox5.Text
                        );
                    MessageBox.Show("Jou werknemer id = " + mainForm.loginContr.LastInsertedId);
                    mainForm.loginContr = null;
                    ClearTextBoxes();
                }
                else this.ErrorLabel.Text = "Gelieve alles in te vullen.";
            }
            catch (ArgumentException argex) {
                this.ErrorLabel.Text = argex.Message;
            }
            catch (Exception ex) {
                this.ErrorLabel.Text = "Oeps er ging iets mis...";
            }            
        }

        private bool CheckIfFilledIn() {
            foreach (TextBox box in boxes) {
                if (string.IsNullOrEmpty(box.Text)) return false;
            }
            return true;
        }

        private void ClearTextBoxes() {
            foreach (TextBox box in boxes) {
                box.Text = "";
            }
        }
    }
}
