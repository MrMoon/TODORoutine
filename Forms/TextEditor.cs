using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using TODORoutine.Database.user.DTO;
using TODORoutine.editor;
using TODORoutine.Models;

namespace MainTextEditor {

    public partial class TextEditor : Form {

        private readonly User user = null;
        private readonly UserDTO userDTO = null;
        private readonly EditorOperation operation;

        public TextEditor(User user , bool isLogin = false) {
            userDTO = UserDTOImplementation.getInstance();
            InitializeComponent();
            if (isLogin) user = userDTO.getByUsername(user.getUsername());
            else userDTO.save(user);
            operation = new EditorOperation(textTabControl);
        }

        private void TextEditor_Load(object sender , EventArgs e) {
            operation.addTab(contextMenuStrip1);
            operation.getFontCollection(toolStripComboBox1);
            operation.populateFontSizes(toolStripComboBox2);
        }

        #region EventBinding

        private void timer1_Tick(object sender , EventArgs e) {
            if (operation.getCurrentDocument.Text.Length > 0) toolStripStatusLabel1.Text = operation.getCurrentDocument.Text.Length.ToString();
        }

        private void newToolStripMenuItem_Click(object sender , EventArgs e) { operation.addTab(contextMenuStrip1); }

        private void openToolStripMenuItem_Click(object sender , EventArgs e) { operation.open(openFileDialog1); }

        private void saveToolStripMenuItem_Click(object sender , EventArgs e) { operation.save(saveFileDialog1); }

        private void saveAsToolStripMenuItem_Click(object sender , EventArgs e) { operation.saveAs(saveFileDialog1); }

        private void exitToolStripMenuItem_Click(object sender , EventArgs e) { Application.Exit(); }

        private void undoToolStripMenuItem_Click(object sender , EventArgs e) { operation.undo(); }

        private void redoToolStripMenuItem_Click(object sender , EventArgs e) { operation.redo(); }

        private void cutToolStripMenuItem_Click(object sender , EventArgs e) { operation.cut(); }

        private void copyToolStripMenuItem_Click(object sender , EventArgs e) { operation.copy(); }

        private void pasteToolStripMenuItem_Click(object sender , EventArgs e) { operation.paste(); }

        private void selectAllToolStripMenuItem_Click(object sender , EventArgs e) { operation.selectAll(); }

        private void closeToolStripMenuItem_Click(object sender , EventArgs e) { operation.removeTab(contextMenuStrip1); }

        private void toolStripButton1_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = operation.getCurrentDocument.SelectionFont.Bold ?
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Regular)  :
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Bold);
        }

        private void toolStripButton2_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = operation.getCurrentDocument.SelectionFont.Italic ? 
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Regular) :
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Italic);
        }

        private void toolStripButton3_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = operation.getCurrentDocument.SelectionFont.Underline ? 
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Regular) :
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Underline);
        }

        private void toolStripButton4_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = operation.getCurrentDocument.SelectionFont.Strikeout ?
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Regular) :
                new Font(operation.getCurrentDocument.SelectionFont.FontFamily , operation.getCurrentDocument.SelectionFont.SizeInPoints , FontStyle.Strikeout);
        }


        private void toolStripButton5_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectedText = operation.getCurrentDocument.SelectedText.ToUpper();
        }

        private void toolStripButton6_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectedText = operation.getCurrentDocument.SelectedText.ToLower();
        }

        private void toolStripButton7_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.Name ,
                operation.getCurrentDocument.SelectionFont.SizeInPoints + 2 , operation.getCurrentDocument.SelectionFont.Style);
        }

        private void toolStripButton8_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.Name ,
                operation.getCurrentDocument.SelectionFont.SizeInPoints - 2 , operation.getCurrentDocument.SelectionFont.Style);
        }

        private void toolStripButton9_Click(object sender , EventArgs e) {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) operation.getCurrentDocument.SelectionColor = colorDialog1.Color;
        }

        private void HighlighGreen_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionBackColor = Color.LightGreen;
        }

        private void HighlighOrange_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionBackColor = Color.Orange;
        }

        private void HighlighYellow_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionBackColor = Color.Yellow;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(toolStripComboBox1.SelectedItem.ToString() ,
                operation.getCurrentDocument.SelectionFont.Size , operation.getCurrentDocument.SelectionFont.Style);
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.Name ,
                float.Parse(toolStripComboBox2.SelectedItem.ToString()) , operation.getCurrentDocument.SelectionFont.Style);
        }

        private void newToolStripButton_Click(object sender , EventArgs e) {
            operation.addTab(contextMenuStrip1);
        }

        private void RemoveTabToolStripButton_Click(object sender , EventArgs e) {
            operation.removeTab(contextMenuStrip1);
        }

        private void openToolStripButton_Click(object sender , EventArgs e) {
            operation.open(openFileDialog1);
        }

        private void saveToolStripButton_Click(object sender , EventArgs e) {
            operation.save(saveFileDialog1);
        }

        private void cutToolStripButton_Click(object sender , EventArgs e) {
            operation.cut();
        }

        private void copyToolStripButton_Click(object sender , EventArgs e) {
            operation.copy();
        }

        private void pasteToolStripButton_Click(object sender , EventArgs e) {
            operation.paste();
        }

        private void undoToolStripMenuItem1_Click(object sender , EventArgs e) {
            operation.undo();
        }

        private void redoToolStripMenuItem1_Click(object sender , EventArgs e) {
            operation.redo();
        }

        private void cutToolStripMenuItem1_Click(object sender , EventArgs e) {
            operation.cut();
        }

        private void copyToolStripMenuItem1_Click(object sender , EventArgs e) {
            operation.copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender , EventArgs e) {
            operation.paste();
        }

        private void saveToolStripMenuItem1_Click(object sender , EventArgs e) {
            operation.save(saveFileDialog1);
        }

        private void closeAllToolStripMenuItem_Click(object sender , EventArgs e) {
            operation.removeAllTabs(contextMenuStrip1);
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender , EventArgs e) {
            operation.removeAllTabsButThis();
        }
        #endregion 
    }
}
