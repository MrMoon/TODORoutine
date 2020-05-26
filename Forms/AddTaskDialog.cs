﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TODORoutine.database.document.dto;
using TODORoutine.database.general.dto;
using TODORoutine.database.note.dto;
using TODORoutine.database.task.dto;
using TODORoutine.Database.Shared;
using TODORoutine.editor;
using TODORoutine.general;
using TODORoutine.general.enums;
using TODORoutine.models;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class AddTaskDialog : Form {

        private readonly User user = null;
        public HashSet<TaskNote> taskNotes = new HashSet<TaskNote>();

        public AddTaskDialog(User user) {
            InitializeComponent();
            this.user = user;
            priorityComboBox.DataSource = Enum.GetValues(typeof(Priority));
            statusComboBox.DataSource = Enum.GetValues(typeof(Status));
        }

        public HashSet<TaskNote> getTaskNotes() => taskNotes;

        private void btnBrowesFile_Click(object sender , EventArgs e) {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (openFileDialog.FileName.Length > 4) txtNote.Text = File.ReadAllText(openFileDialog.FileName);
                else MessageBox.Show("Invalid File");
            }
        }

        private void btnAdd_Click(object sender , EventArgs e) {
            if (!Validator.isValidTexts(txtNote , txtTitle , statusComboBox , priorityComboBox)) return;
            if (dueDatePicker.Text.Length == 0) MessageBox.Show("Please Select a Due Date");
            saveTask();
        }

        private void saveTask() {
            Document document = new Document();
            document.setOwner(user.getUsername());
            document.setDocument(txtNote.Text);
            DocumentDTOImplementation.getInstance().save(document);
            Note note = new Note();
            note.setAuthor(user.getFullName());
            note.setDocumentId(document.getId());
            note.setLastModified(DateTime.Now);
            note.setTitle(txtTitle.Text);
            NoteDTOImplementation.getInstance().save(note);
            TaskNote task = new TaskNote();
            task.status = (Status) Enum.Parse(typeof(Status) , statusComboBox.SelectedItem.ToString());
            task.priority = (Priority) Enum.Parse(typeof(Priority) , priorityComboBox.SelectedItem.ToString());
            task.noteId = note.getId();
            task.dueDate = dueDatePicker.Value;
            TaskDTOImplentation.getInstance().save(task);
            taskNotes.Add(task);
        }

        private void txtNote_TextChanged(object sender , EventArgs e) {
            if (txtNote.Focused) txtNote.BackColor = Color.White;
        }

        private void txtTitle_TextChanged(object sender , EventArgs e) {
            if (txtTitle.Focused) txtTitle.BackColor = Color.White;
        }

        private void priorityComboBox_SelectedIndexChanged(object sender , EventArgs e) {
            if (priorityComboBox.Focused) priorityComboBox.BackColor = Color.White;
        }

        private void statusComboBox_SelectedIndexChanged(object sender , EventArgs e) {
            if (statusComboBox.Focused) statusComboBox.BackColor = Color.White;
        }

    }
}