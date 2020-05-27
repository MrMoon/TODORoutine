using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TODORoutine.database.document.dto;
using TODORoutine.database.note.dto;
using TODORoutine.database.notebook.dto;
using TODORoutine.database.parsers;
using TODORoutine.database.sharing.dto;
using TODORoutine.Database.Shared;
using TODORoutine.models;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class NotebookForm : Form {

        private readonly User user = null;
        private Notebook notebook;
        private int lastNotebookId = 1 , lastNoteId = 1;
        private readonly NotebookDTO notebookDTO = NotebookDTOImplementation.getInstance();
        private readonly NoteDTO noteDTO = NoteDTOImplementation.getInstance();
        private HashSet<Notebook> notebooks = new HashSet<Notebook>();
        private HashSet<Note> notes = new HashSet<Note>();

        public NotebookForm(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void NotebookForm_Load(object sender , EventArgs e) {
            refreshNoteData();
        }

        private void refreshNoteData() {
            foreach (Note note in noteDTO.getAll(lastNoteId.ToString())) createNotePanel(note);
        }

        private void createNotePanel(Note note) {
            noteListView.Items.Add(note.getId());
        }

        private void refreshNotebookData() {
            notebooks.Union(notebookDTO.getAll(lastNotebookId.ToString()));
            foreach (Notebook notebook in notebooks) {
                createNotebookPanel(notebook);
            }
        }

        private void createNotebookPanel(Notebook notebook) {
            notebookListView.Items.Add(notebook.getId());
        }

        public void openAddNotebookDialog(String id , String title) {
            Form addNotebookDialog = new Form { Width = 500 , Height = 120 , Text = "Find and Replace" };
            Label lblTitle = new Label() { Left = 10 , Top = 20 , Text = "Notebook Title :" , Width = 100 };
            TextBox txtTitle = new TextBox() { Left = 150 , Top = 20 , Width = 300 };
            Button btnAdd = new Button() { Text = "Add" , Left = 350 , Width = 100 , Top = 90 };

            btnAdd.Click += (o , e) => {
                if(DataValidator.isValidTexts(txtTitle)) {
                    Notebook notebook = new Notebook();
                    notebook.setAuthor(user.getFullName());
                    notebook.setLastModified(DateTime.Now);
                    notebook.setTitle(txtTitle.Text);
                    if (String.IsNullOrEmpty(id)) notebookDTO.save(notebook);
                    else notebookDTO.update(notebook , DatabaseConstants.COLUMN_AUTHOR , DatabaseConstants.COLUMN_LASTMODIFIED , DatabaseConstants.COLUMN_TITLE);
                    refreshNotebookData();
                }
            };
            if (!String.IsNullOrEmpty(title)) txtTitle.Text = title;
            addNotebookDialog.Controls.Add(btnAdd);
            addNotebookDialog.Controls.Add(lblTitle);
            addNotebookDialog.Controls.Add(txtTitle);
            addNotebookDialog.Show();
        }

        private void btnShare_Click(object sender , EventArgs e) {
            foreach(ListViewItem item in noteListView.SelectedItems) {
                Share share = new Share();
                share.userId = user.getId();
                share.documentsIds.Add(item.Text);
                ShareDTOImplentation.getInstance().save(share);
            }
        }

        private void noteListView_SelectedIndexChanged(object sender , EventArgs e) {
            foreach(ListViewItem item in noteListView.SelectedItems) {
                MessageBox.Show(noteDTO.getNoteDocument(item.Text).getDocumentContent());
            }
        }

        private void btnAddNote_Click(object sender , EventArgs e) {
            AddTaskDialog addTaskDialog = new AddTaskDialog(user);
            addTaskDialog.Show();
        }

        private void btnEditNote_Click(object sender , EventArgs e) {
            AddTaskDialog addTaskDialog = new AddTaskDialog(user);
            addTaskDialog.Show();
        }

        private void btnDeleteNote_Click(object sender , EventArgs e) {
            if (noteListView.SelectedItems.Count == 0) {
                MessageBox.Show("Please Select a notebook to delete");
            } else {
                foreach (ListViewItem item in noteListView.SelectedItems) {
                    notebookDTO.delete(item.Text);
                }
            }
        }

        private void btnAddNotebook_Click_1(object sender , EventArgs e) => openAddNotebookDialog(null , null);

        private void btnEditNotebook_Click(object sender , EventArgs e) {
            foreach(ListViewItem item in notebookListView.SelectedItems) {
                Notebook notebookTemp = notebookDTO.getById(item.Text);
                openAddNotebookDialog(notebookTemp.getId() , notebookTemp.getTitle());
            }
        }

        private void btnSwitch_Click(object sender , EventArgs e) {
            notebook = notebookDTO.getById(notebookListView.SelectedItems[0].Text);
        }

        private void notebookListView_SelectedIndexChanged(object sender , EventArgs e) {
            foreach (ListViewItem item in notebookListView.SelectedItems) {
                MessageBox.Show(notebookDTO.getById(item.Text).ToString());
            }
        }

        private void btnDeleteNotebook_Click(object sender , EventArgs e) {
            if(notebookListView.SelectedItems.Count == 0) {
                MessageBox.Show("Please Select a notebook to delete");
            } else {
                foreach(ListViewItem item in notebookListView.SelectedItems) {
                    notebookDTO.delete(item.Text);
                }
            }
        }
    }
}
