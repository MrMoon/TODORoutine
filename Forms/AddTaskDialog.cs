using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TODORoutine.database.document.dto;
using TODORoutine.database.general.shared;
using TODORoutine.database.note.dto;
using TODORoutine.database.notebook.dto;
using TODORoutine.database.task.dto;
using TODORoutine.general.constants;
using TODORoutine.general.enums;
using TODORoutine.general.validation;
using TODORoutine.models;

namespace TODORoutine.forms {
    public partial class AddTaskDialog : Form {

        private readonly User user = null;
        private Notebook notebook = null;
        private Note note = null;

        public AddTaskDialog(User user) {
            InitializeComponent();
            this.user = user;
            priorityComboBox.DataSource = Enum.GetValues(typeof(Priority));
            statusComboBox.DataSource = Enum.GetValues(typeof(Status));
        }

        public AddTaskDialog(User user , Notebook notebook , Note note = null) {
            InitializeComponent();
            this.user = user;
            this.notebook = notebook;
            this.note = note;
            priorityComboBox.DataSource = Enum.GetValues(typeof(Priority));
            statusComboBox.DataSource = Enum.GetValues(typeof(Status));
        }

        private void btnBrowesFile_Click(object sender , EventArgs e) {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = TypesConstants.FILE_TYPES;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (openFileDialog.FileName.Length > 4) txtNote.Text = File.ReadAllText(openFileDialog.FileName);
                else MessageBox.Show(DatabaseConstants.INVALID("File"));
            }
        }

        private void btnAdd_Click(object sender , EventArgs e) {
            if (!DataValidator.isValidTexts(txtNote , txtTitle , statusComboBox , priorityComboBox)) return;
            if (dueDatePicker.Text.Length == 0) {
                MessageBox.Show("Please Select a Due Date");
                return;
            }
            saveTask();
        }

        private void saveTask() {
            if (note != null) {
                updateTask();
            } else {
                Document document = new Document();
                document.setOwner(user.getUsername());
                document.setDocument(txtNote.Text);
                DocumentDTOImplementation.getInstance().save(document);
                Note noteTemp = new Note();
                noteTemp.setAuthor(user.getFullName());
                noteTemp.setDocumentId(document.getId());
                noteTemp.setLastModified(DateTime.Now);
                noteTemp.setTitle(txtTitle.Text);
                NoteDTOImplementation.getInstance().save(noteTemp);
                TaskNote task = new TaskNote();
                task.status = (Status) Enum.Parse(typeof(Status) , statusComboBox.SelectedItem.ToString());
                task.priority = (Priority) Enum.Parse(typeof(Priority) , priorityComboBox.SelectedItem.ToString());
                task.noteId = noteTemp.getId();
                task.dueDate = dueDatePicker.Value;
                TaskDTOImplementation.getInstance().save(task);
                if (notebook != null) {
                    notebook.addNote(noteTemp.getId());
                    NotebookDTOImplementation.getInstance().update(notebook , DatabaseConstants.COLUMN_NOTESID);
                }
            }
            MessageBox.Show("Task and Note were added succesfully" , "Task Added Successfully");
        }

        private void updateTask() {
            Document document = DocumentDTOImplementation.getInstance().getById(note.getDocumentId());
            document.setDocument(txtNote.Text);
            DocumentDTOImplementation.getInstance().update(document , DatabaseConstants.COLUMN_DOCUMENT);
            Note noteTemp = new Note();
            noteTemp.setDocumentId(document.getId());
            noteTemp.setLastModified(DateTime.Now);
            noteTemp.setTitle(txtTitle.Text);
            NoteDTOImplementation.getInstance().update(noteTemp , DatabaseConstants.COLUMN_LASTMODIFIED , DatabaseConstants.COLUMN_TITLE);
            TaskNote task = new TaskNote {
                status = (Status) Enum.Parse(typeof(Status) , statusComboBox.SelectedItem.ToString()) ,
                priority = (Priority) Enum.Parse(typeof(Priority) , priorityComboBox.SelectedItem.ToString()) ,
                dueDate = dueDatePicker.Value
            };
            TaskDTOImplementation.getInstance().update(task , DatabaseConstants.COLUMN_STATUS , DatabaseConstants.COLUMN_PRIORITY , DatabaseConstants.COLUMN_DUEDATE);
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
