using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TODORoutine.authentication;
using TODORoutine.database.document.dao;
using TODORoutine.database.document.dto;
using TODORoutine.database.note.dto;
using TODORoutine.database.notebook.dto;
using TODORoutine.database.parsers;
using TODORoutine.database.sharing.dto;
using TODORoutine.Database.user.DTO;
using TODORoutine.models;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class Test : Form {

        private Authenticate auth;

        public Test() {
            InitializeComponent();
        }

        private void Test_Load(object sender , EventArgs e) {
            
        }

        private void btnInsert_Click(object sender , EventArgs e) {
            auth = new Authenticate(txtUsername.Text , txtPassword.Text);
            MessageBox.Show(auth.authentication(true).ToString());
        }

        private void btnDelete_Click(object sender , EventArgs e) {
            auth = new Authenticate(txtUsername.Text , txtPassword.Text);
            MessageBox.Show(auth.authentication(false).ToString());
        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            
        }

        private void btnSelect_Click(object sender , EventArgs e) {
            
        }

        private void txtId_TextChanged(object sender , EventArgs e) {

        }
    }
}
