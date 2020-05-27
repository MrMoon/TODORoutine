namespace TODORoutine.forms {
    partial class TaskReportForm {
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dueDatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.getMoreDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportChart)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripMenuItem,
            this.priorityToolStripMenuItem,
            this.dueDatesToolStripMenuItem,
            this.getMoreDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1304, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.statusToolStripMenuItem.Text = "&Status";
            this.statusToolStripMenuItem.Click += new System.EventHandler(this.statusToolStripMenuItem_Click);
            // 
            // priorityToolStripMenuItem
            // 
            this.priorityToolStripMenuItem.Name = "priorityToolStripMenuItem";
            this.priorityToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.priorityToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.priorityToolStripMenuItem.Text = "&Priority";
            this.priorityToolStripMenuItem.Click += new System.EventHandler(this.priorityToolStripMenuItem_Click);
            // 
            // dueDatesToolStripMenuItem
            // 
            this.dueDatesToolStripMenuItem.Name = "dueDatesToolStripMenuItem";
            this.dueDatesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.dueDatesToolStripMenuItem.Size = new System.Drawing.Size(92, 24);
            this.dueDatesToolStripMenuItem.Text = "&Due Dates";
            this.dueDatesToolStripMenuItem.Click += new System.EventHandler(this.dueDatesToolStripMenuItem_Click);
            // 
            // reportChart
            // 
            chartArea2.Name = "ChartArea1";
            this.reportChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.reportChart.Legends.Add(legend2);
            this.reportChart.Location = new System.Drawing.Point(13, 32);
            this.reportChart.Name = "reportChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.reportChart.Series.Add(series2);
            this.reportChart.Size = new System.Drawing.Size(1279, 714);
            this.reportChart.TabIndex = 1;
            this.reportChart.Text = "chart";
            // 
            // getMoreDataToolStripMenuItem
            // 
            this.getMoreDataToolStripMenuItem.Name = "getMoreDataToolStripMenuItem";
            this.getMoreDataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.getMoreDataToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.getMoreDataToolStripMenuItem.Text = "&Get More Data";
            this.getMoreDataToolStripMenuItem.Click += new System.EventHandler(this.getMoreDataToolStripMenuItem_Click);
            // 
            // TaskReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 782);
            this.Controls.Add(this.reportChart);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TaskReportForm";
            this.Text = "TaskReportForm";
            this.Load += new System.EventHandler(this.TaskReportForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dueDatesToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart reportChart;
        private System.Windows.Forms.ToolStripMenuItem getMoreDataToolStripMenuItem;
    }
}