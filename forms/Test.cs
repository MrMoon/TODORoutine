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
using TODORoutine.Database.user.DTO;
using TODORoutine.models;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class Test : Form {

        private DocumentDTO dto;
        private Document document;

        public Test() {
            InitializeComponent();
        }

        private void Test_Load(object sender , EventArgs e) {
            dto = DocumentDTOImplementation.getInstance();
            document = new Document();
        }

        private void btnInsert_Click(object sender , EventArgs e) {
            document.setOwner(txtOwner.Text);
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt";
            fileDialog.FilterIndex = 0;
            fileDialog.RestoreDirectory = true;

            if(fileDialog.ShowDialog() == DialogResult.OK) document.setDocument(File.ReadAllBytes(fileDialog.FileName));
            dto.save(document);
        }

        private void btnDelete_Click(object sender , EventArgs e) {
            MessageBox.Show(dto.delete(document.getId()).ToString());
        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            document.setOwner(txtOwner.Text);
            MessageBox.Show(dto.update(document , DatabaseConstants.COLUMN_OWENER).ToString());
        }

        private void btnSelect_Click(object sender , EventArgs e) {
            List<Document> documents = dto.getAll();
            foreach (Document document in documents) MessageBox.Show(document.toString());
        }

        private void txtId_TextChanged(object sender , EventArgs e) {

        }
    }
}
