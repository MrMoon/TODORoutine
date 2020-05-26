using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TODORoutine.general.constants;
using TODORoutine.graph;

namespace TODORoutine.forms {
    public partial class SortForm : Form {

        private const int N = 200010;
        private List<Panel> selectedNodes = new List<Panel>();
        private int counter = 1;
        private Point mouseDownLocation;
        private Graph graph;

        public SortForm() {
            InitializeComponent();
            graph = new Graph(N);
        }

        private Panel createPanel() {
            if(counter > 2e5) {
                MessageBox.Show("You can't add more than 200,000 Nodes , Sorry ");
                return null;
            }
            Panel nodePanel = new Panel {
                AutoSize = true ,
                Name = counter.ToString() , 
                BorderStyle = BorderStyle.FixedSingle ,
                BackColor = Color.LightBlue ,
            };
            TextBox node = new TextBox {
                Dock = DockStyle.Fill ,
                Multiline = true
            };
            CheckBox nodeCheck = new CheckBox {
                Enabled = false ,
                Dock = DockStyle.Bottom ,
                Text = "Node " + counter++
            };
            nodePanel.MouseDown += (o , eventArgs) => {
                if (eventArgs.Button == System.Windows.Forms.MouseButtons.Left) mouseDownLocation = eventArgs.Location;
            };
            nodePanel.MouseMove += (o , eventArgs) => {
                if (eventArgs.Button == System.Windows.Forms.MouseButtons.Left) {
                    nodePanel.Left = eventArgs.X + nodePanel.Left - mouseDownLocation.X;
                    nodePanel.Top = eventArgs.Y + nodePanel.Top - mouseDownLocation.Y;
                }
            };
            nodeCheck.CheckedChanged += (o , eventArgs) => {
                if (nodeCheck.Checked) selectedNodes.Add(nodePanel);
                else selectedNodes.Remove(nodePanel);
            };
            nodePanel.Controls.Add(node);
            nodePanel.Controls.Add(nodeCheck);
            return nodePanel;
        }
        
        private void connectEdge(Panel source , Panel destination) {
            Pen pen = new Pen(Color.Red , 4);
            GraphicsPath penCapPath = new GraphicsPath();
            // A triangle for the arrow
            penCapPath.AddLine(-1 , 0 , 1 , 0);
            penCapPath.AddLine(-1 , 0 , 0 , 1);
            penCapPath.AddLine(0 , 1 , 1 , 0);
            pen.CustomEndCap = new CustomLineCap(null , penCapPath);
            Graphics graphics = this.CreateGraphics();
            graphics.DrawLine(pen , source.Location , destination.Location);
            //Connecting the graph
            int u = int.Parse(source.Name), v = int.Parse(destination.Name);
            destination.Controls.OfType<CheckBox>().ToList().ForEach((bx) => bx.Checked = false);
            source.Controls.OfType<CheckBox>().ToList().ForEach((bx) => bx.Checked = false);
            graph.add(u , v);
        }

        private void btnAdd_Click(object sender , EventArgs e) {
            this.Controls.Add(createPanel());
        }        

        private void ckbxEdit_CheckedChanged(object sender , EventArgs e) {
            foreach (Panel panel in this.Controls.OfType<Panel>())
                foreach (CheckBox checkbox in panel.Controls.OfType<CheckBox>())
                    checkbox.Enabled = TypesConstants.FLIP(checkbox.Enabled);
        }

        private void btnConnect_Click(object sender , EventArgs e) {
            if (ckbxEdit.Checked) {
                if (selectedNodes.Count == 2) {
                    if (MessageBox.Show(UserMessages.ARE_YOU_SURE("Connect") , UserMessages.CONFIRMION("Connection") 
                        , MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        connectEdge(selectedNodes[0] , selectedNodes[1]);
                    }
                } else MessageBox.Show(UserMessages.TWO_NODE);
            } else MessageBox.Show(UserMessages.ENABLE_EDIT);
        }

        private void btnSort_Click(object sender , EventArgs e) {
            try {
                List<int> list = graph.topologicalSorting(counter - 1);
                StringBuilder sortedNodes = new StringBuilder();
                sortedNodes.Append("{ sorted Nodes : ");
                String prefix = "";
                foreach (int x in list) {
                    sortedNodes.Append(prefix);
                    prefix = ",";
                    sortedNodes.Append(x);
                }
                sortedNodes.Append(" }");
                MessageBox.Show(sortedNodes.ToString());
            } catch (ArgumentException ex) {
                if(ex.Message.Equals(UserMessages.CYCLE)) MessageBox.Show(ex.Message);
            }
        }
    }
}
