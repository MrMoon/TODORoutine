using System;
using System.Drawing;
using System.Windows.Forms;
using TODORoutine.database.parsers;
using TODORoutine.Database.user.DTO;
using TODORoutine.editor;
using TODORoutine.forms;
using TODORoutine.Models;

namespace MainTextEditor {

    public partial class TextEditorForm : Form {

        private readonly User user = null;
        private Color color = Color.Black;
        private readonly EditorOperation operation = null;
        private readonly UserDTO userDTO = null;

        public TextEditorForm(User user , bool isLogin = false) {
            userDTO = UserDTOImplementation.getInstance();
            InitializeComponent();
            if (isLogin) user = userDTO.getByUsername(user.getUsername());
            else userDTO.save(user);
            operation = new EditorOperation(textTabControl);
        }

        private void TextEditorForm_Load(object sender , EventArgs e) {
            operation.addTab(contextMenuStrip1);
            operation.getFontCollection(fonts);
            operation.populateFontSizes(sizes);
        }

        #region EventBinding

        private void timer1_Tick(object sender , EventArgs e) {
            if (operation.getCurrentDocument.Text.Length > 0) toolStripStatusLabel1.Text = operation.getCurrentDocument.Text.Length.ToString();
        }

        private void newToolStripMenuItem_Click(object sender , EventArgs e) { operation.addTab(contextMenuStrip1); }

        private void openToolStripMenuItem_Click(object sender , EventArgs e) { operation.open(openFileDialog); }

        private void saveToolStripMenuItem_Click(object sender , EventArgs e) { operation.save(saveFileDialog); }

        private void saveAsToolStripMenuItem_Click(object sender , EventArgs e) { operation.saveAs(saveFileDialog); }

        private void exitToolStripMenuItem_Click(object sender , EventArgs e) { Application.Exit(); }

        private void undoToolStripMenuItem_Click(object sender , EventArgs e) { operation.undo(); }

        private void redoToolStripMenuItem_Click(object sender , EventArgs e) { operation.redo(); }

        private void cutToolStripMenuItem_Click(object sender , EventArgs e) { operation.cut(); }

        private void copyToolStripMenuItem_Click(object sender , EventArgs e) { operation.copy(); }

        private void pasteToolStripMenuItem_Click(object sender , EventArgs e) { operation.paste(); }

        private void selectAllToolStripMenuItem_Click(object sender , EventArgs e) { operation.selectAll(); }

        private void closeToolStripMenuItem_Click(object sender , EventArgs e) { operation.removeTab(contextMenuStrip1); }

        private void btnBold_Click(object sender , EventArgs e) {
            System.Drawing.FontStyle fontStyle = FontStyle.Regular;
            if (operation.getCurrentDocument.SelectionFont.Bold) fontStyle |= FontStyle.Regular;
            else fontStyle |= FontStyle.Bold;
            if (operation.getCurrentDocument.SelectionFont.Italic) fontStyle |= FontStyle.Italic;
            if (operation.getCurrentDocument.SelectionFont.Underline) fontStyle |= FontStyle.Underline;
            if (operation.getCurrentDocument.SelectionFont.Strikeout) fontStyle |= FontStyle.Strikeout;
            btnBold.Checked = flip(btnBold.Checked);
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.FontFamily ,
                operation.getCurrentDocument.SelectionFont.Size , fontStyle);
        }

        private void btnItalic_Click(object sender , EventArgs e) {
            System.Drawing.FontStyle fontStyle = FontStyle.Regular;
            if (operation.getCurrentDocument.SelectionFont.Italic) fontStyle |= FontStyle.Regular;
            else fontStyle |= FontStyle.Italic;
            if (operation.getCurrentDocument.SelectionFont.Underline) fontStyle |= FontStyle.Underline;
            if (operation.getCurrentDocument.SelectionFont.Bold) fontStyle |= FontStyle.Bold;
            if (operation.getCurrentDocument.SelectionFont.Strikeout) fontStyle |= FontStyle.Strikeout;
            btnItalic.Checked = flip(btnItalic.Checked);
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.FontFamily ,
                operation.getCurrentDocument.SelectionFont.Size , fontStyle);
        }

        private void btnUnderline_Click(object sender , EventArgs e) {
            System.Drawing.FontStyle fontStyle = FontStyle.Regular;
            if (operation.getCurrentDocument.SelectionFont.Underline) fontStyle |= FontStyle.Regular;
            else fontStyle |= FontStyle.Underline;
            if (operation.getCurrentDocument.SelectionFont.Italic) fontStyle |= FontStyle.Italic;
            if (operation.getCurrentDocument.SelectionFont.Bold) fontStyle |= FontStyle.Bold;
            if (operation.getCurrentDocument.SelectionFont.Strikeout) fontStyle |= FontStyle.Strikeout;
            btnUnderline.Checked = flip(btnUnderline.Checked);
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.FontFamily ,
                operation.getCurrentDocument.SelectionFont.Size , fontStyle);
        }

        private void btnStrikeout_Click(object sender , EventArgs e) {
            System.Drawing.FontStyle fontStyle = FontStyle.Regular;
            if (operation.getCurrentDocument.SelectionFont.Strikeout) fontStyle |= FontStyle.Regular;
            else fontStyle |= FontStyle.Strikeout;
            if (operation.getCurrentDocument.SelectionFont.Italic) fontStyle |= FontStyle.Italic;
            if (operation.getCurrentDocument.SelectionFont.Bold) fontStyle |= FontStyle.Bold;
            if (operation.getCurrentDocument.SelectionFont.Underline) fontStyle |= FontStyle.Underline;
            btnStrikeout.Checked = flip(btnStrikeout.Checked);
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.FontFamily ,
                operation.getCurrentDocument.SelectionFont.Size , fontStyle);
            
        }


        private void btnUppercase_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectedText = operation.getCurrentDocument.SelectedText.ToUpper();
        }

        private void btnLowercase_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectedText = operation.getCurrentDocument.SelectedText.ToLower();
        }

        private void btnSizeUp_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.Name ,
                operation.getCurrentDocument.SelectionFont.SizeInPoints + 2 , operation.getCurrentDocument.SelectionFont.Style);
            int selectedTemp = sizes.SelectedIndex;
            if (sizes.SelectedIndex + 2 >= sizes.Items.Count) for (int i = sizes.SelectedIndex + 1 ; i <= (sizes.Items.Count << 1) ; ++i) sizes.Items.Add(i);
            sizes.SelectedIndex = selectedTemp + 2;
        }

        private void btnSizeDown_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.Name ,
                operation.getCurrentDocument.SelectionFont.SizeInPoints - 2 , operation.getCurrentDocument.SelectionFont.Style);
            if (sizes.SelectedIndex - 2 < 0) sizes.SelectedIndex = 0;
            else sizes.SelectedIndex -= 2;
        }

        private void btnColor_Click(object sender , EventArgs e) {
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                color = colorDialog.Color;
                operation.getCurrentDocument.SelectionColor = color;
            }
        }

        private void HighlighGreen_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionBackColor = (HighlighGreen.Checked) ? color : Color.LightGreen;
        }

        private void HighlighOrange_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionBackColor = (HighlighOrange.Checked) ? color : Color.Orange;
        }

        private void HighlighYellow_Click(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionBackColor = (HighlighOrange.Checked) ? color : Color.Yellow;
        }

        private void size_SelectedIndexChanged(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(operation.getCurrentDocument.SelectionFont.Name
                , float.Parse(sizes.SelectedItem.ToString()) , operation.getCurrentDocument.SelectionFont.Style);
        }

        private void fonts_SelectedIndexChanged(object sender , EventArgs e) {
            operation.getCurrentDocument.SelectionFont = new Font(fonts.SelectedItem.ToString() ,
                operation.getCurrentDocument.SelectionFont.Size , operation.getCurrentDocument.SelectionFont.Style);
        }

        private void newToolStripButton_Click(object sender , EventArgs e) {
            operation.addTab(contextMenuStrip1);
        }

        private void RemoveTabToolStripButton_Click(object sender , EventArgs e) {
            operation.removeTab(contextMenuStrip1);
        }

        private void openToolStripButton_Click(object sender , EventArgs e) {
            operation.open(openFileDialog);
        }

        private void saveToolStripButton_Click(object sender , EventArgs e) {
            operation.save(saveFileDialog);
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
            operation.save(saveFileDialog);
        }

        private void closeAllToolStripMenuItem_Click(object sender , EventArgs e) {
            operation.removeAllTabs(contextMenuStrip1);
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender , EventArgs e) {
            operation.removeAllTabsButThis();
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender , EventArgs e) {
            MessageBox.Show(DatabaseConstants.ALL);
        }

        private void findToolStripMenuItem_Click(object sender , EventArgs e) {
            operation.findDialog(operation.getCurrentDocument.BackColor.Equals(HighlighGreen.BackColor) ? Color.OrangeRed : Color.Green , color);
        }

        private bool flip(bool flag) {
            return flag ? false : true;
        }

        private void btnTask_Click(object sender , EventArgs e) {
            TaskForm taskForm = new TaskForm(user);
            taskForm.Show();
        }

        private void taskToolStripMenuItem_Click(object sender , EventArgs e) {
            TaskForm taskForm = new TaskForm(user);
            taskForm.Show();
        }
    }
}
