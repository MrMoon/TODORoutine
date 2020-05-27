namespace TODORoutine.forms {
    partial class TaskForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskForm));
            this.btnUpdateTask = new System.Windows.Forms.Button();
            this.btnGetTasks = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.chkUpdate = new System.Windows.Forms.CheckBox();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnUndoDelete = new System.Windows.Forms.Button();
            this.taskData = new System.Windows.Forms.DataGridView();
            this.idDataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dueDateDataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priorityDataColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.statusDataColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.documentDataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskNoteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.taskData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskNoteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdateTask
            // 
            this.btnUpdateTask.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpdateTask.Location = new System.Drawing.Point(73, 12);
            this.btnUpdateTask.Name = "btnUpdateTask";
            this.btnUpdateTask.Size = new System.Drawing.Size(133, 33);
            this.btnUpdateTask.TabIndex = 17;
            this.btnUpdateTask.Text = "Update Task";
            this.btnUpdateTask.UseVisualStyleBackColor = true;
            this.btnUpdateTask.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnGetTasks
            // 
            this.btnGetTasks.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGetTasks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGetTasks.Location = new System.Drawing.Point(354, 12);
            this.btnGetTasks.Name = "btnGetTasks";
            this.btnGetTasks.Size = new System.Drawing.Size(132, 33);
            this.btnGetTasks.TabIndex = 16;
            this.btnGetTasks.Text = "Get All Tasks";
            this.btnGetTasks.UseVisualStyleBackColor = true;
            this.btnGetTasks.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnAddTask
            // 
            this.btnAddTask.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddTask.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddTask.Location = new System.Drawing.Point(492, 12);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(130, 33);
            this.btnAddTask.TabIndex = 13;
            this.btnAddTask.Text = "Add a Task";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtNote
            // 
            this.txtNote.AcceptsReturn = true;
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(13, 442);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ReadOnly = true;
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNote.Size = new System.Drawing.Size(1031, 199);
            this.txtNote.TabIndex = 18;
            // 
            // chkUpdate
            // 
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.Location = new System.Drawing.Point(13, 19);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(54, 21);
            this.chkUpdate.TabIndex = 19;
            this.chkUpdate.Text = "Edit";
            this.chkUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUpdate.UseVisualStyleBackColor = true;
            this.chkUpdate.CheckedChanged += new System.EventHandler(this.chkUpdate_CheckedChanged);
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteTask.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeleteTask.Location = new System.Drawing.Point(914, 12);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(130, 33);
            this.btnDeleteTask.TabIndex = 20;
            this.btnDeleteTask.Text = "Delete Task";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // btnUndoDelete
            // 
            this.btnUndoDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndoDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUndoDelete.Location = new System.Drawing.Point(778, 12);
            this.btnUndoDelete.Name = "btnUndoDelete";
            this.btnUndoDelete.Size = new System.Drawing.Size(130, 33);
            this.btnUndoDelete.TabIndex = 21;
            this.btnUndoDelete.Text = "Undo Delete";
            this.btnUndoDelete.UseVisualStyleBackColor = true;
            this.btnUndoDelete.Click += new System.EventHandler(this.btnUndoDelete_Click);
            // 
            // taskData
            // 
            this.taskData.AllowUserToOrderColumns = true;
            this.taskData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskData.AutoGenerateColumns = false;
            this.taskData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.taskData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataColumn,
            this.dueDateDataColumn,
            this.priorityDataColumn,
            this.statusDataColumn,
            this.documentDataColumn});
            this.taskData.DataSource = this.taskNoteBindingSource;
            this.taskData.Location = new System.Drawing.Point(13, 66);
            this.taskData.Name = "taskData";
            this.taskData.RowHeadersWidth = 51;
            this.taskData.RowTemplate.Height = 24;
            this.taskData.Size = new System.Drawing.Size(1031, 370);
            this.taskData.TabIndex = 22;
            this.taskData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.taskData_CellContentClick);
            this.taskData.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.taskData_RowStateChanged);
            // 
            // idDataColumn
            // 
            this.idDataColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.idDataColumn.DataPropertyName = "id";
            this.idDataColumn.HeaderText = "id";
            this.idDataColumn.MinimumWidth = 6;
            this.idDataColumn.Name = "idDataColumn";
            this.idDataColumn.ReadOnly = true;
            // 
            // dueDateDataColumn
            // 
            this.dueDateDataColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dueDateDataColumn.DataPropertyName = "dueDate";
            this.dueDateDataColumn.HeaderText = "dueDate";
            this.dueDateDataColumn.MinimumWidth = 6;
            this.dueDateDataColumn.Name = "dueDateDataColumn";
            this.dueDateDataColumn.ReadOnly = true;
            // 
            // priorityDataColumn
            // 
            this.priorityDataColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.priorityDataColumn.DataPropertyName = "priority";
            this.priorityDataColumn.HeaderText = "priority";
            this.priorityDataColumn.MinimumWidth = 6;
            this.priorityDataColumn.Name = "priorityDataColumn";
            this.priorityDataColumn.ReadOnly = true;
            this.priorityDataColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.priorityDataColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // statusDataColumn
            // 
            this.statusDataColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.statusDataColumn.DataPropertyName = "status";
            this.statusDataColumn.HeaderText = "status";
            this.statusDataColumn.MinimumWidth = 6;
            this.statusDataColumn.Name = "statusDataColumn";
            this.statusDataColumn.ReadOnly = true;
            this.statusDataColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.statusDataColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // documentDataColumn
            // 
            this.documentDataColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.documentDataColumn.DataPropertyName = "document";
            this.documentDataColumn.HeaderText = "document";
            this.documentDataColumn.MinimumWidth = 6;
            this.documentDataColumn.Name = "documentDataColumn";
            this.documentDataColumn.ReadOnly = true;
            // 
            // taskNoteBindingSource
            // 
            this.taskNoteBindingSource.DataSource = typeof(TODORoutine.models.TaskNote);
            // 
            // TaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1056, 653);
            this.Controls.Add(this.taskData);
            this.Controls.Add(this.btnUndoDelete);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.chkUpdate);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.btnUpdateTask);
            this.Controls.Add(this.btnGetTasks);
            this.Controls.Add(this.btnAddTask);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskForm";
            this.Text = "TaskForm";
            this.Load += new System.EventHandler(this.TaskForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.taskData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskNoteBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnUpdateTask;
        private System.Windows.Forms.Button btnGetTasks;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.CheckBox chkUpdate;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnUndoDelete;
        private System.Windows.Forms.DataGridView taskData;
        private System.Windows.Forms.BindingSource taskNoteBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dueDateDataColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn priorityDataColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn statusDataColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn documentDataColumn;
    }
}