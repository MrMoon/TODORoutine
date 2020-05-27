namespace TODORoutine.forms {
    partial class NotebookForm {
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
            this.groupBoxNotebook = new System.Windows.Forms.GroupBox();
            this.notebookListView = new System.Windows.Forms.ListView();
            this.btnAddNotebook = new System.Windows.Forms.Button();
            this.btnEditNotebook = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btnDeleteNotebook = new System.Windows.Forms.Button();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.btnEditNote = new System.Windows.Forms.Button();
            this.btnDeleteNote = new System.Windows.Forms.Button();
            this.btnShare = new System.Windows.Forms.Button();
            this.groupBoxNote = new System.Windows.Forms.GroupBox();
            this.noteListView = new System.Windows.Forms.ListView();
            this.groupBoxNotebook.SuspendLayout();
            this.groupBoxNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxNotebook
            // 
            this.groupBoxNotebook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxNotebook.Controls.Add(this.notebookListView);
            this.groupBoxNotebook.Controls.Add(this.btnAddNotebook);
            this.groupBoxNotebook.Controls.Add(this.btnEditNotebook);
            this.groupBoxNotebook.Controls.Add(this.btnSwitch);
            this.groupBoxNotebook.Controls.Add(this.btnDeleteNotebook);
            this.groupBoxNotebook.Location = new System.Drawing.Point(13, 12);
            this.groupBoxNotebook.Name = "groupBoxNotebook";
            this.groupBoxNotebook.Size = new System.Drawing.Size(553, 721);
            this.groupBoxNotebook.TabIndex = 11;
            this.groupBoxNotebook.TabStop = false;
            this.groupBoxNotebook.Text = "Notebook";
            // 
            // notebookListView
            // 
            this.notebookListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notebookListView.HideSelection = false;
            this.notebookListView.Location = new System.Drawing.Point(6, 59);
            this.notebookListView.Name = "notebookListView";
            this.notebookListView.Size = new System.Drawing.Size(541, 656);
            this.notebookListView.TabIndex = 18;
            this.notebookListView.UseCompatibleStateImageBehavior = false;
            this.notebookListView.SelectedIndexChanged += new System.EventHandler(this.notebookListView_SelectedIndexChanged);
            // 
            // btnAddNotebook
            // 
            this.btnAddNotebook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNotebook.Location = new System.Drawing.Point(6, 21);
            this.btnAddNotebook.Name = "btnAddNotebook";
            this.btnAddNotebook.Size = new System.Drawing.Size(134, 31);
            this.btnAddNotebook.TabIndex = 2;
            this.btnAddNotebook.Text = "Add Notebook";
            this.btnAddNotebook.UseVisualStyleBackColor = true;
            this.btnAddNotebook.Click += new System.EventHandler(this.btnAddNotebook_Click_1);
            // 
            // btnEditNotebook
            // 
            this.btnEditNotebook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditNotebook.Location = new System.Drawing.Point(146, 21);
            this.btnEditNotebook.Name = "btnEditNotebook";
            this.btnEditNotebook.Size = new System.Drawing.Size(130, 31);
            this.btnEditNotebook.TabIndex = 3;
            this.btnEditNotebook.Text = "Edit Notebook";
            this.btnEditNotebook.UseVisualStyleBackColor = true;
            this.btnEditNotebook.Click += new System.EventHandler(this.btnEditNotebook_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitch.Location = new System.Drawing.Point(282, 21);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(130, 31);
            this.btnSwitch.TabIndex = 5;
            this.btnSwitch.Text = "Switch Notebook";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btnDeleteNotebook
            // 
            this.btnDeleteNotebook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteNotebook.Location = new System.Drawing.Point(417, 21);
            this.btnDeleteNotebook.Name = "btnDeleteNotebook";
            this.btnDeleteNotebook.Size = new System.Drawing.Size(130, 31);
            this.btnDeleteNotebook.TabIndex = 4;
            this.btnDeleteNotebook.Text = "Delete NoteBook";
            this.btnDeleteNotebook.UseVisualStyleBackColor = true;
            this.btnDeleteNotebook.Click += new System.EventHandler(this.btnDeleteNotebook_Click);
            // 
            // btnAddNote
            // 
            this.btnAddNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNote.Location = new System.Drawing.Point(6, 21);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(83, 31);
            this.btnAddNote.TabIndex = 13;
            this.btnAddNote.Text = "Add Note";
            this.btnAddNote.UseVisualStyleBackColor = true;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // btnEditNote
            // 
            this.btnEditNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditNote.Location = new System.Drawing.Point(95, 21);
            this.btnEditNote.Name = "btnEditNote";
            this.btnEditNote.Size = new System.Drawing.Size(83, 31);
            this.btnEditNote.TabIndex = 14;
            this.btnEditNote.Text = "Edit Note";
            this.btnEditNote.UseVisualStyleBackColor = true;
            this.btnEditNote.Click += new System.EventHandler(this.btnEditNote_Click);
            // 
            // btnDeleteNote
            // 
            this.btnDeleteNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteNote.Location = new System.Drawing.Point(321, 21);
            this.btnDeleteNote.Name = "btnDeleteNote";
            this.btnDeleteNote.Size = new System.Drawing.Size(114, 31);
            this.btnDeleteNote.TabIndex = 15;
            this.btnDeleteNote.Text = "Delete Note";
            this.btnDeleteNote.UseVisualStyleBackColor = true;
            this.btnDeleteNote.Click += new System.EventHandler(this.btnDeleteNote_Click);
            // 
            // btnShare
            // 
            this.btnShare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShare.Location = new System.Drawing.Point(184, 21);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(131, 31);
            this.btnShare.TabIndex = 16;
            this.btnShare.Text = "Share Note";
            this.btnShare.UseVisualStyleBackColor = true;
            this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
            // 
            // groupBoxNote
            // 
            this.groupBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNote.Controls.Add(this.noteListView);
            this.groupBoxNote.Controls.Add(this.btnDeleteNote);
            this.groupBoxNote.Controls.Add(this.btnShare);
            this.groupBoxNote.Controls.Add(this.btnAddNote);
            this.groupBoxNote.Controls.Add(this.btnEditNote);
            this.groupBoxNote.Location = new System.Drawing.Point(579, 12);
            this.groupBoxNote.Name = "groupBoxNote";
            this.groupBoxNote.Size = new System.Drawing.Size(457, 721);
            this.groupBoxNote.TabIndex = 12;
            this.groupBoxNote.TabStop = false;
            this.groupBoxNote.Text = "Note";
            // 
            // noteListView
            // 
            this.noteListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noteListView.HideSelection = false;
            this.noteListView.Location = new System.Drawing.Point(7, 59);
            this.noteListView.Name = "noteListView";
            this.noteListView.Size = new System.Drawing.Size(444, 656);
            this.noteListView.TabIndex = 17;
            this.noteListView.UseCompatibleStateImageBehavior = false;
            this.noteListView.SelectedIndexChanged += new System.EventHandler(this.noteListView_SelectedIndexChanged);
            // 
            // NotebookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 745);
            this.Controls.Add(this.groupBoxNotebook);
            this.Controls.Add(this.groupBoxNote);
            this.MinimumSize = new System.Drawing.Size(1027, 792);
            this.Name = "NotebookForm";
            this.Text = "NotebookForm";
            this.Load += new System.EventHandler(this.NotebookForm_Load);
            this.groupBoxNotebook.ResumeLayout(false);
            this.groupBoxNote.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxNotebook;
        private System.Windows.Forms.Button btnAddNotebook;
        private System.Windows.Forms.Button btnEditNotebook;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnDeleteNotebook;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.Button btnEditNote;
        private System.Windows.Forms.Button btnDeleteNote;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.GroupBox groupBoxNote;
        private System.Windows.Forms.ListView noteListView;
        private System.Windows.Forms.ListView notebookListView;
    }
}