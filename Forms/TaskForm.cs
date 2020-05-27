using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TODORoutine.database.document.dto;
using TODORoutine.database.note.dto;
using TODORoutine.database.parsers;
using TODORoutine.database.task.dto;
using TODORoutine.general;
using TODORoutine.general.constants;
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
        private readonly TaskDTO taskDTO = TaskDTOImplementation.getInstance();

        public TaskForm(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void refreshTaskData() {
            tasks.UnionWith(taskDTO.getAllTasks(lastId.ToString()));
            foreach (TaskNote task in tasks) {
                if(String.IsNullOrEmpty(task.document)) task.document = NoteDTOImplementation.getInstance().getNoteDocument(task.noteId).getDocumentContent();
                if (int.Parse(task.id) > lastId) lastId = int.Parse(task.id);
            }
            ++lastId;
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
            refreshTaskData();
        }

        private void taskData_CellContentClick(object sender , DataGridViewCellEventArgs e) => txtNote.Text = taskData.SelectedCells[0].Value.ToString();

        private void btnGet_Click(object sender , EventArgs e) => refreshTaskData();

        private void btnAdd_Click(object sender , EventArgs e) {
            AddTaskDialog addTask = new AddTaskDialog(user);
            addTask.Show();
            addTask.FormClosed += (obj , form) => refreshTaskData();
        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            if (!chkUpdate.Checked) {
                MessageBox.Show(UserMessages.ENABLE_EDIT);
                chkUpdate.Focus();
                return;
            }
            if (taskData.SelectedRows.Count > 0) {
                if (MessageBox.Show(UserMessages.ARE_YOU_SURE("Update") , UserMessages.CONFIRMION("Update") , MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    TaskNote task;
                    bool flag = false;
                    foreach (DataGridViewRow row in taskData.SelectedRows) {
                        task = (TaskNote) row.DataBoundItem;
                        flag = taskDTO.update(task , DatabaseConstants.COLUMN_DUEDATE , DatabaseConstants.COLUMN_PRIORITY , DatabaseConstants.COLUMN_STATUS);
                        UserMessages.messageStatus(flag);
                    }
                    refreshTaskData();
                }
            } else MessageBox.Show(UserMessages.EMPTY_OPERATION("Update"));
        }

        private void chkUpdate_CheckedChanged(object sender , EventArgs e) {
            foreach (DataGridViewColumn column in taskData.Columns) column.ReadOnly = TypesConstants.FLIP(column.ReadOnly);
            txtNote.ReadOnly = TypesConstants.FLIP(txtNote.ReadOnly);
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
                if (MessageBox.Show(UserMessages.ARE_YOU_SURE("Delete") , UserMessages.CONFIRMION("Delete") , MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    lastId -= taskData.SelectedRows.Count;
                    if (lastId < 0) lastId = 1;
                    TaskNote task;
                    bool flag = false;
                    foreach (DataGridViewRow row in taskData.SelectedRows) {
                        task = (TaskNote) row.DataBoundItem;
                        undoBufferIndex = (undoBufferIndex + 1) % bufferSize;
                        flag = taskDTO.delete(task.id);
                        Note noteTemp = NoteDTOImplementation.getInstance().getById(task.noteId);
                        flag &= noteTemp != null;
                        flag &= NoteDTOImplementation.getInstance().delete(task.noteId);
                        flag &= DocumentDTOImplementation.getInstance().delete(noteTemp.getDocumentId());
                        undoBuffer[undoBufferIndex] = task;
                        tasks.Remove(task);
                        UserMessages.messageStatus(flag);
                    }
                    refreshTaskData();
                }
            } else MessageBox.Show(UserMessages.EMPTY_OPERATION("Delete"));
        }

        private void btnUndoDelete_Click(object sender , EventArgs e) {
            if (undoBufferIndex > 0) {
                if (MessageBox.Show(UserMessages.ARE_YOU_SURE("Undo Delete") , UserMessages.CONFIRMION("Undo Delete") , MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    bool flag = taskDTO.save(undoBuffer[undoBufferIndex]);
                    tasks.Add(undoBuffer[undoBufferIndex]);
                    undoBufferIndex = (((undoBufferIndex - 1) % bufferSize) + bufferSize) % bufferSize;
                    ++lastId;
                    UserMessages.messageStatus(flag);
                    refreshTaskData();
                }
            } else MessageBox.Show(UserMessages.EMPTY_OPERATION("Undo"));
        }
    }
}
