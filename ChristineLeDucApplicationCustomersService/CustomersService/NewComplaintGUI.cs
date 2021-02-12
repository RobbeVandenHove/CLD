using Globals;
using Logic;
using System;
using System.Windows.Forms;

namespace CustomersService {
    public partial class NewComplaintGUI : Form {
        private MainGUI mainForm;
        private Order searchOrder;
        public NewComplaintGUI(MainGUI mainForm) {
            InitializeComponent();
            this.mainForm = mainForm;
            this.mainForm.dataController = new DataController(this.mainForm.loginContr.DatabasePath);
            LoadComboBoxes();
            ClearLabels();
        }
 
        private void LoadComboBoxes() {
            foreach (string main in mainForm.dataController.GetAllMainSubjects()) {
                this.comboBox1.Items.Add(main);
            }
            foreach (string main in mainForm.dataController.GetAllSubSubjects()) {
                this.comboBox2.Items.Add(main);
            }
            foreach (string main in mainForm.dataController.GetAllWorkers()) {
                this.comboBox3.Items.Add(main);
            }
            this.comboBox3.SelectedIndex = comboBox3.FindStringExact(mainForm.loginContr.WorkerName);
            this.comboBox4.Items.AddRange(new object[] { "Open", "Gesloten" });
        }

        private void ClearLabels() {
            this.ErrorLabel.Text = "";
            this.label21.Text = "";
            this.label22.Text = "";
            this.label23.Text = "";
            this.label24.Text = "";
            this.label25.Text = "";
            this.label26.Text = "";
            this.label27.Text = "";
            this.label28.Text = "";
            this.label29.Text = "";
            this.label30.Text = "";
            this.label31.Text = "";
            this.label32.Text = "";
            this.label33.Text = "";
        }

        private void button2_Click(object sender, EventArgs e) {
            ClearLabels();
        }

        private void NewComplaintGUI_Load(object sender, EventArgs e) {
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            if (this.textBox1.Text.Length == 10) FindOrderByReferentionNumber();
            else ClearLabels();
        }

        private void FindOrderByReferentionNumber() {
            try {
                DisplayOrder(mainForm.dataController.GetOrderByReference(this.textBox1.Text));
            }
            catch (Exception ex) {
                this.ErrorLabel.Text = ex.Message;
            }            
        }

        private void DisplayOrder(Order order) {
            this.label21.Text = order.OrderId;
            this.label22.Text = order.Factuuradres;
            this.label23.Text = order.FactuurPostcode;
            this.label24.Text = order.FactuurWoonplaats;
            this.label25.Text = order.FactuurLand;
            this.label26.Text = order.Verzendnaam;
            this.label27.Text = order.Verzendadres;
            this.label28.Text = order.VerzendPostcode;
            this.label29.Text = order.VerzendLand;
            this.label30.Text = order.Telefoonnummer;
            this.label31.Text = order.Emailadres;
            this.label32.Text = order.Orderdatum;
            this.label33.Text = order.Ordertijd;
            this.searchOrder = order;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (CheckIfFilledIn()) {
                int id = mainForm.dataController.GetLastIdLogbook();
                searchOrder.OrderId = searchOrder.OrderId == null ? "unknown" : searchOrder.OrderId;
                string dataString = $"{this.textBox1.Text};{searchOrder.OrderId};{this.textBox1.Text}_{searchOrder.OrderId}_{id};" +
                                    $"{this.comboBox1.Text};{this.comboBox2.Text};{this.textBox2.Text};{DateTime.Now.ToString()};" +
                                    $"{this.comboBox4.Text}";
                this.ErrorLabel.Text = "";
                try {
                    mainForm.dataController.InsertNewLogbook(dataString, mainForm.loginContr.WorkerId);
                    MessageBox.Show("Logboek toegevoegd.");
                    ClearInputs();
                }
                catch (Exception ex) {
                    this.ErrorLabel.Text = "Oeps er ging iets mis...";
                }                
            }
        }

        private bool CheckIfFilledIn() {
            if (String.IsNullOrEmpty(this.comboBox1.Text)) return false;
            if (String.IsNullOrEmpty(this.comboBox2.Text)) return false;
            if (String.IsNullOrEmpty(this.comboBox3.Text)) return false;
            if (String.IsNullOrEmpty(this.comboBox4.Text)) return false;
            if (String.IsNullOrEmpty(this.textBox1.Text)) return false;
            if (String.IsNullOrEmpty(this.textBox2.Text)) return false;
            try {
                var order = mainForm.dataController.GetOrderByReference(this.textBox1.Text);
                if (order.ReferentieNummer == "Unknown" || string.IsNullOrEmpty(order.ReferentieNummer)) this.ErrorLabel.Text = "Dit order is niet meer actief.";
            }
            catch (Exception ex) {
                this.ErrorLabel.Text = ex.Message;
            }
            return true;
        }

        private void ClearInputs() {
            this.comboBox1.Text = "";
            this.comboBox2.Text = "";
            this.comboBox3.Text = "";
            this.comboBox4.Text = "";
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }
    }
}
