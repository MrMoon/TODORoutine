using System;
using System.Windows.Forms;
using TODORoutine.database.document.dto;
using TODORoutine.database.general.shared;
using TODORoutine.database.note.dto;
using TODORoutine.database.notebook.dto;
using TODORoutine.database.sharing.dto;
using TODORoutine.database.user.dto;
using TODORoutine.general.constants;
using TODORoutine.general.validation;
using TODORoutine.models;

namespace TODORoutine.forms {
    public partial class NotebookForm : Form {

        private readonly User user = null;
        private Notebook notebook = null;
        private Share share = null;
        private int lastNotebookId = 1, lastNoteId = 1;
        private readonly NotebookDTO notebookDTO = NotebookDTOImplementation.getInstance();
        private readonly NoteDTO noteDTO = NoteDTOImplementation.getInstance();

        public NotebookForm(User user) {
            InitializeComponent();
            this.user = user;
            this.share = new Share {
                userId = user.getId()
            };
        }

        private void NotebookForm_Load(object sender , EventArgs e) {
            refreshNoteData();
            refreshNotebookData();
        }

        private void refreshNoteData() {
            foreach (Note note in noteDTO.getAll(lastNoteId.ToString(lastNoteId.ToString()))) addNote(note);
        }

        private void refreshNotebookData() {
            foreach (Notebook notebook in notebookDTO.getAll(lastNotebookId.ToString(lastNotebookId.ToString()))) addNotebook(notebook);
        }

        private bool binarySearch(ListView.ListViewItemCollection list , int val) {
            int start = 0, end = list.Count - 1;
            while(start <= end) {
                int mid = start + ((end - start) >> 1);
                int midVal = int.Parse(list[mid].Text);
                if (midVal == val) return true;
                else if (midVal < val) start = mid + 1;
                else end = mid - 1;
            }
            return false;
        }

        private void addNote(Note note) {
            if (int.Parse(note.getId()) > lastNoteId) lastNoteId = int.Parse(note.getId());
            if (!binarySearch(noteListView.Items , int.Parse(note.getId()))) noteListView.Items.Add(note.getId());
        }

        private void addNotebook(Notebook notebook) {
            if (int.Parse(notebook.getId()) > lastNotebookId) lastNotebookId = int.Parse(notebook.getId());
            if (!binarySearch(notebookListView.Items , int.Parse(notebook.getId()))) notebookListView.Items.Add(notebook.getId());
            this.notebook = notebook;
        }

        public void openAddNotebookDialog(String id , String title) {
            Form addNotebookDialog = new Form { Width = 500 , Height = 120 , Text = "Add Notebook" };
            Label lblTitle = new Label() { Left = 10 , Top = 20 , Text = "Notebook Title :" , Width = 100 };
            TextBox txtTitle = new TextBox() { Left = 150 , Top = 20 , Width = 300 };
            Button btnAdd = new Button() { Text = "Add" , Left = 350 , Width = 100 , Top = 40 };
            addNotebookDialog.AcceptButton = btnAdd;
            btnAdd.Click += (o , e) => {
                if(DataValidator.isValidTexts(txtTitle)) {
                    Notebook notebookTemp = new Notebook();
                    bool flag = false;
                    notebookTemp.setAuthor(user.getFullName());
                    notebookTemp.setLastModified(DateTime.Now);
                    notebookTemp.setTitle(txtTitle.Text);
                    if (String.IsNullOrEmpty(id)) flag = notebookDTO.save(notebookTemp);
                    else flag = notebookDTO.update(notebookTemp , DatabaseConstants.COLUMN_AUTHOR , DatabaseConstants.COLUMN_LASTMODIFIED , DatabaseConstants.COLUMN_TITLE);
                    notebook = notebookTemp;
                    user.setNotebookId(notebook.getId());
                    UserDTOImplementation.getInstance().update(user , DatabaseConstants.COLUMN_NOTEBOOKID);
                    UserMessages.messageStatus(flag);
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
            bool flag = false;
            foreach(ListViewItem item in noteListView.SelectedItems) {
                share.documentsIds.Add(item.Text);
                if (ShareDTOImplementation.getInstance().update(share , DatabaseConstants.COLUMN_DOCUMENTSIDS)) flag = true;
                else flag = ShareDTOImplementation.getInstance().save(share);
                UserMessages.messageStatus(flag);
            }
        }

        private void noteListView_SelectedIndexChanged(object sender , EventArgs e) {
            if (noteListView.SelectedItems.Count > 1) {
                btnDeleteNote.Text = "Delete Notes";
                btnShareNote.Text = "Share Notes";
            } else {
                btnShareNote.Text = "Share Note";
                btnDeleteNote.Text = "Delete Note";
            }
            foreach(ListViewItem item in noteListView.SelectedItems) MessageBox.Show(noteDTO.getNoteDocument(item.Text).getDocumentContent());
        }

        private void btnAddNote_Click(object sender , EventArgs e) {
            AddTaskDialog addTaskDialog = new AddTaskDialog(user , notebook);
            addTaskDialog.Show();
        }

        private void btnEditNote_Click(object sender , EventArgs e) {
            foreach(ListViewItem item in noteListView.Items) {
                AddTaskDialog addTaskDialog = new AddTaskDialog(user , notebook , noteDTO.getById(item.Text));
                addTaskDialog.Show();
            }
        }

        private void btnDeleteNote_Click(object sender , EventArgs e) {
            if (noteListView.SelectedItems.Count == 0) MessageBox.Show("Please Select a notebook to delete");
            else {
                if(MessageBox.Show(UserMessages.ARE_YOU_SURE("Delete , (this cannot be undone) ") 
                    , UserMessages.CONFIRMION("Delete") , MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    Note noteTemp;
                    bool flag = false;
                    foreach (ListViewItem item in noteListView.SelectedItems) {
                        noteTemp = noteDTO.getById(item.Text);
                        flag = DocumentDTOImplementation.getInstance().delete(noteTemp.getDocumentId());
                        flag &= noteDTO.delete(noteTemp.getId());
                        UserMessages.messageStatus(flag);
                    }
                }
            }
        }

        private void btnAddNotebook_Click(object sender , EventArgs e) => openAddNotebookDialog(null , null);

        private void btnEditNotebook_Click(object sender , EventArgs e) {
            Notebook notebookTemp;
            foreach(ListViewItem item in notebookListView.SelectedItems) {
                notebookTemp = notebookDTO.getById(item.Text);
                openAddNotebookDialog(notebookTemp.getId() , notebookTemp.getTitle());
            }
        }

        private void btnSwitch_Click(object sender , EventArgs e) {
            if (notebookListView.SelectedItems.Count == 0) MessageBox.Show("Please Select a notebook");
            else if (notebookListView.SelectedItems.Count != 1) MessageBox.Show("Please Select only one notebook");
            else {
                notebook = notebookDTO.getById(notebookListView.SelectedItems[0].Text);
                user.setNotebookId(notebook.getId());
                bool flag = UserDTOImplementation.getInstance().update(user , DatabaseConstants.COLUMN_NOTEBOOKID);
                UserMessages.messageStatus(flag);
            }
        }

        private void notebookListView_SelectedIndexChanged(object sender , EventArgs e) {
            btnDeleteNotebook.Text = (notebookListView.SelectedItems.Count > 1) ? "Delete Notebooks" : "Delete Notebook";
            foreach (ListViewItem item in notebookListView.SelectedItems) 
                MessageBox.Show(notebookDTO.getById(item.Text).ToString());
        }

        private void btnDeleteNotebook_Click(object sender , EventArgs e) {
            if (notebookListView.SelectedItems.Count == 0) MessageBox.Show("Please Select a notebook to delete");
            else {
                if(MessageBox.Show(UserMessages.ARE_YOU_SURE("Delete Notebook , This will also delete the notes (Can't Be Undone)") 
                    , UserMessages.CONFIRMION("Delete Notebook") , MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    Notebook notebookTemp;
                    bool flag = false;
                    foreach (ListViewItem item in notebookListView.SelectedItems) {
                        notebookTemp = notebookDTO.getById(item.Text);
                        flag = notebookTemp != null;
                        foreach (String noteId in notebookTemp.getNotes()) flag &= noteDTO.delete(noteId);
                        flag &= notebookDTO.delete(notebookTemp.getId());
                        UserMessages.messageStatus(flag);
                    }
                }
            }
        }
    }
}
