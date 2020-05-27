using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TODORoutine.database.task.dto;
using TODORoutine.general;
using TODORoutine.general.enums;
using TODORoutine.models;

namespace TODORoutine.forms {
    public partial class TaskReportForm : Form {

        private readonly TaskDTO taskDTO = TaskDTOImplentation.getInstance();
        private int[] priorityCount;
        private int[] statusCount;
        private int[] monuthCount;
        private int lastTaskId = 1;
        private HashSet<TaskNote> tasks = new HashSet<TaskNote>();

        public TaskReportForm() {
            InitializeComponent();
        }

        private void drawPieChart(String seriesName) {
            //Reset the chart series and legends
            reportChart.Series.Clear();
            reportChart.Legends.Clear();

            reportChart.Legends.Add(seriesName);
            reportChart.Legends[0].LegendStyle = LegendStyle.Table;
            reportChart.Legends[0].Docking = Docking.Bottom;
            reportChart.Legends[0].Alignment = StringAlignment.Center;
            reportChart.Legends[0].Title = seriesName + " Report";
            reportChart.Legends[0].BorderColor = Color.Black;
            reportChart.Series.Add(seriesName);
            reportChart.Series[seriesName].ChartType = SeriesChartType.Pie;
        }

        private void refreshData() {
            priorityCount = new int[4];
            statusCount = new int[4];
            monuthCount = new int[13];
            tasks.UnionWith(taskDTO.getAllTasks(lastTaskId.ToString()));
            foreach (TaskNote task in tasks) {
                ++statusCount[(int) task.status];
                ++priorityCount[(int) task.priority];
                ++monuthCount[task.dueDate.Month];
                if (int.Parse(task.id) > lastTaskId) lastTaskId = int.Parse(task.id);
            }
        }

        private void TaskReportForm_Load(object sender , EventArgs e) => refreshData();

        private void statusToolStripMenuItem_Click(object sender , EventArgs e) {
            drawPieChart("Status");
            for(int i = 0 ; i < 4 ; ++i)
                if(statusCount[i] != 0) 
                    reportChart.Series["Status"].Points.AddXY(((Status) i).ToString() , statusCount[i]);
        }

        private void priorityToolStripMenuItem_Click(object sender , EventArgs e) {
            drawPieChart("Priority");
            for(int i = 0 ; i < 4 ; ++i)
                if(priorityCount[i] != 0)
                    reportChart.Series["Priority"].Points.AddXY(((Priority) i).ToString() , priorityCount[i]);
        }

        private void dueDatesToolStripMenuItem_Click(object sender , EventArgs e) {
            drawPieChart("Due Dates");
            for(int i = 1 ; i <= 12 ; ++i)
                if(monuthCount[i] != 0) 
                    reportChart.Series["Due Dates"].Points.AddXY(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).ToString() , monuthCount[i]);
        }

        private void getMoreDataToolStripMenuItem_Click(object sender , EventArgs e) => refreshData();
    }
}
