using System;
using System.Windows.Forms;
using TODORoutine.database;
using TODORoutine.database.authentication.dto;

namespace TODORoutine.forms {
    public partial class Test : Form {

        private AuthenticationDTO dto = null;

        public Test() {
            InitializeComponent();
        }

        private void Test_Load(object sender , EventArgs e) {
            dto = AuthenticationDTOImplentation.getInstance();
        }

        private void btnInsert_Click(object sender , EventArgs e) {
            MessageBox.Show(dto.authenticate(new Authentication(txtUsername.Text , txtPassword.Text) , true).ToString());
        }

        private void btnDelete_Click(object sender , EventArgs e) {
            MessageBox.Show(dto.authenticate(new Authentication(txtUsername.Text , txtPassword.Text)).ToString());
        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            
        }

        private void btnSelect_Click(object sender , EventArgs e) {
            
        }

        private void txtId_TextChanged(object sender , EventArgs e) {

        }
    }
}
