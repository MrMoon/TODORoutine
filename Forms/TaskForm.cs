using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TODORoutine.database.document.dto;
using TODORoutine.database.note.dto;
using TODORoutine.database.parsers;
using TODORoutine.database.task.dto;
using TODORoutine.general;
using TODORoutine.general.enums;
using TODORoutine.models;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class TaskForm : Form {

        private const int bufferSize = 97;
        private HashSet<TaskNote> tasks = new HashSet<TaskNote>();
        private TaskNote[] undoBuffer = new TaskNote[bufferSize];
        private int undoBufferIndex = 0;
        private int lastId = 1;
        private User user;
        private readonly TaskDTO taskDTO = TaskDTOImplentation.getInstance();

        public TaskForm(User user) {
            InitializeComponent();
            this.user = user;
        }

        private bool flip(bool flag) => !flag;

        private void refreshTaskData() {
            tasks.UnionWith(taskDTO.getAllTasks(lastId.ToString()));
            foreach (TaskNote task in tasks) {
                if(String.IsNullOrEmpty(task.document)) task.document = NoteDTOImplementation.getInstance().getNoteDocument(task.noteId).getDocumentContent();
                if (int.Parse(task.id) > lastId) lastId = int.Parse(task.id);
            }
            taskData.DataSource = tasks.ToList();
        }

        private void TaskForm_Load(object sender , EventArgs e) => setupDataGridView();

        private void setupDataGridView() {
            priorityDataColumn.ValueType = typeof(Priority);
            priorityDataColumn.DataSource = Enum.GetValues(typeof(Priority));

            statusDataColumn.ValueType = typeof(Status);
            statusDataColumn.DataSource = Enum.GetValues(typeof(Status));

            dueDateDataColumn.ValueType = typeof(DateTime);

            idDataColumn.ValueType = typeof(String);
            documentDataColumn.ValueType = typeof(String);
        }

        private void taskData_CellContentClick(object sender , DataGridViewCellEventArgs e) => txtNote.Text = taskData.SelectedCells[0].Value.ToString();

        private void btnGet_Click(object sender , EventArgs e) => refreshTaskData();

        private void btnAdd_Click(object sender , EventArgs e) {
            AddTaskDialog addTask = new AddTaskDialog(user);
            addTask.Show();
            addTask.FormClosed += (obj , form) => refreshTaskData();
            tasks.UnionWith(addTask.getTaskNotes());
        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            if (!chkUpdate.Checked) {
                MessageBox.Show("Must Enable the Edit Option First");
                chkUpdate.Focus();
                return;
            }
            if (taskData.SelectedRows.Count > 0) {
                DialogResult result = MessageBox.Show("Are you sure you want to Update ?" , "Update Confirmation" , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    foreach (DataGridViewRow row in taskData.SelectedRows) {
                        TaskNote task = (TaskNote) row.DataBoundItem;
                        taskDTO.update(task , DatabaseConstants.COLUMN_DUEDATE , DatabaseConstants.COLUMN_PRIORITY , DatabaseConstants.COLUMN_STATUS);
                    }
                    refreshTaskData();
                }
            } else MessageBox.Show("There is nothing to Update");
        }

        private void chkUpdate_CheckedChanged(object sender , EventArgs e) {
            foreach (DataGridViewColumn column in taskData.Columns) column.ReadOnly = flip(column.ReadOnly);
            txtNote.ReadOnly = flip(txtNote.ReadOnly);
            idDataColumn.ReadOnly = true;
            documentDataColumn.ReadOnly = true;
        }

        private void taskData_RowStateChanged(object sender , DataGridViewRowStateChangedEventArgs e) {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            if (taskData.SelectedRows.Count > 1) {
                btnUpdateTask.Text = "Update Tasks";
                btnDeleteTask.Text = "Delete Tasks";
            } else {
                btnUpdateTask.Text = "Update Task";
                btnDeleteTask.Text = "Delete Task";
            }
        }

        private void btnDeleteTask_Click(object sender , EventArgs e) {
            if(taskData.SelectedRows.Count > 0) {
                DialogResult result = MessageBox.Show("Are you sure you want to Delete ?" , "Delete Confirmation" , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    lastId -= taskData.SelectedRows.Count;
                    if (lastId < 0) lastId = 1;
                    foreach (DataGridViewRow row in taskData.SelectedRows) {
                        TaskNote task = (TaskNote) row.DataBoundItem;
                        undoBufferIndex = (undoBufferIndex + 1) % bufferSize;
                        taskDTO.delete(task.id);
                        Note noteTemp = NoteDTOImplementation.getInstance().getById(task.noteId);
                        NoteDTOImplementation.getInstance().delete(task.noteId);
                        DocumentDTOImplementation.getInstance().delete(noteTemp.getDocumentId());
                        undoBuffer[undoBufferIndex] = task;
                        tasks.Remove(task);
                    }
                    refreshTaskData();
                }
            } else MessageBox.Show("There is nothing to Delete");
        }

        private void btnUndoDelete_Click(object sender , EventArgs e) {
            if (undoBufferIndex > 0) {
                DialogResult result = MessageBox.Show("Are you sure you want to Undo the Delete ?" , "Undo Delete Confirmation" , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    taskDTO.save(undoBuffer[undoBufferIndex]);
                    tasks.Add(undoBuffer[undoBufferIndex]);
                    undoBufferIndex = (((undoBufferIndex - 1) % bufferSize) + bufferSize) % bufferSize;
                    ++lastId;
                    refreshTaskData();
                }
            } else MessageBox.Show("There is nothing to undo");
        }
    }
}
