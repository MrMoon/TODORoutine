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

        private Share share;
        private ShareDTO dto = null;

        public Test() {
            InitializeComponent();
        }

        private void Test_Load(object sender , EventArgs e) {
            share = new Share();
            dto = ShareDTOImplentation.getInstance();
        }

        private void btnInsert_Click(object sender , EventArgs e) {
            share = new Share();
            share.userId = txtUserId.Text;
            share.documentsIds.Add(txtDocumentIds.Text);
            Console.WriteLine(dto.save(share));

        }

        private void btnDelete_Click(object sender , EventArgs e) {

        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            share.documentsIds.Add(txtDocumentIds.Text);
            Console.WriteLine(dto.update(share , DatabaseConstants.COLUMN_DOCUMENTSIDS));
        }

        private void btnSelect_Click(object sender , EventArgs e) {
            
        }

        private void txtId_TextChanged(object sender , EventArgs e) {

        }
    }
}
