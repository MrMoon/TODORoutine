namespace TODORoutine.forms {
    partial class SortForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortForm));
            this.btnAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSort = new System.Windows.Forms.ToolStripMenuItem();
            this.sotMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ckbxEdit = new System.Windows.Forms.CheckBox();
            this.sotMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.btnAdd.Size = new System.Drawing.Size(143, 24);
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.btnConnect.Size = new System.Drawing.Size(143, 24);
            this.btnConnect.Text = "&Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSort
            // 
            this.btnSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSort.Name = "btnSort";
            this.btnSort.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.btnSort.Size = new System.Drawing.Size(143, 24);
            this.btnSort.Text = "&Sort";
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // sotMenuStrip
            // 
            this.sotMenuStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.sotMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sotMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnConnect,
            this.btnSort});
            this.sotMenuStrip.Location = new System.Drawing.Point(1345, 0);
            this.sotMenuStrip.Name = "sotMenuStrip";
            this.sotMenuStrip.Size = new System.Drawing.Size(156, 677);
            this.sotMenuStrip.TabIndex = 5;
            this.sotMenuStrip.Text = "SortMenu";
            // 
            // ckbxEdit
            // 
            this.ckbxEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbxEdit.AutoSize = true;
            this.ckbxEdit.Location = new System.Drawing.Point(1435, 92);
            this.ckbxEdit.Name = "ckbxEdit";
            this.ckbxEdit.Size = new System.Drawing.Size(54, 21);
            this.ckbxEdit.TabIndex = 2;
            this.ckbxEdit.Text = "Edit";
            this.ckbxEdit.UseVisualStyleBackColor = true;
            this.ckbxEdit.CheckedChanged += new System.EventHandler(this.ckbxEdit_CheckedChanged);
            // 
            // SortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1501, 677);
            this.Controls.Add(this.ckbxEdit);
            this.Controls.Add(this.sotMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.sotMenuStrip;
            this.Name = "SortForm";
            this.Text = "SortForm";
            this.sotMenuStrip.ResumeLayout(false);
            this.sotMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem btnAdd;
        private System.Windows.Forms.ToolStripMenuItem btnConnect;
        private System.Windows.Forms.ToolStripMenuItem btnSort;
        private System.Windows.Forms.MenuStrip sotMenuStrip;
        private System.Windows.Forms.CheckBox ckbxEdit;
    }
}