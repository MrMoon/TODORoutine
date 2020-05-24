using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TODORoutine.database.task.dto;
using TODORoutine.general;
using TODORoutine.general.enums;
using TODORoutine.models;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class TaskForm : Form {

        private bool editFlag = true;
        private int lastId = 1;
        private readonly User user;
        private readonly TaskDTO taskDTO = null;

        public TaskForm(User user) {
            InitializeComponent();
            this.user = user;
            taskDTO = TaskDTOImplentation.getInstance();
        }

        private void TaskForm_Load(object sender , EventArgs e) {
            PriorityColumn.ValueType = typeof(Priority);
            PriorityColumn.DataSource = Enum.GetValues(typeof(Priority));

            StatusColumn.ValueType = typeof(Status);
            StatusColumn.DataSource = Enum.GetValues(typeof(Status));

            DueDateColumn.ValueType = typeof(DateTime);
            IdColumn.ValueType = typeof(String);
            NoteColumn.ValueType = typeof(String);
        }

        private void txtFilter_TextChanged(object sender , EventArgs e) {

        }

        private void taskData_CellContentClick(object sender , DataGridViewCellEventArgs e) {
            txtNote.Text = taskData.SelectedCells[0].Value.ToString();
        }

        private void btnEdit_Click(object sender , EventArgs e) {
            editFlag = editFlag ? false : true;
            foreach (DataGridViewColumn column in taskData.Columns) column.ReadOnly = editFlag;
            txtNote.ReadOnly = editFlag;
        }

        private void btnGet_Click(object sender , EventArgs e) {
            List<TaskNote> tasks = taskDTO.getTasksAllOrderByDueDate(lastId.ToString());
            foreach (TaskNote task in tasks) taskData.Rows.Add(task.id , task.dueDate , task.priority , task.status , task.noteId);
            lastId = int.Parse(tasks[tasks.Count() - 1].id) + 1;
        }

        private void btnAdd_Click(object sender , EventArgs e) {

        }
    }
}
