using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using TODORoutine.database.document.dto;
using TODORoutine.database.parsers;
using TODORoutine.general.constants;
using TODORoutine.models;

namespace TODORoutine.editor {
    class EditorOperation {

        public int tabCounter = 0;
        public TabControl tabControl = null;

        public EditorOperation(TabControl tabControl) {
            this.tabControl = tabControl;
        }

        public RichTextBox getCurrentDocument => (RichTextBox) tabControl.SelectedTab.Controls["Body"];

        public void addTab(ContextMenuStrip menuStrip) {

            RichTextBox body = new RichTextBox {
                Name = "Body" ,
                Dock = DockStyle.Fill ,
                ContextMenuStrip = menuStrip
            };

            TabPage newPage = new TabPage();
            tabCounter += 1;

            String documentText = "BrainStorm " + tabCounter;
            newPage.Name = documentText;
            newPage.Text = documentText;
            newPage.Controls.Add(body);

            tabControl.TabPages.Add(newPage);
        }

        public void removeTab(ContextMenuStrip menuStrip) {
            if (tabControl.TabPages.Count != 1) tabControl.TabPages.Remove(tabControl.SelectedTab);
            else {
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                addTab(menuStrip);
            }
        }

        public void removeAllTabs(ContextMenuStrip menuStrip) {
            foreach (TabPage page in tabControl.TabPages) tabControl.TabPages.Remove(page);
            addTab(menuStrip);
        }

        public void removeAllTabsButThis() {
            foreach (TabPage page in tabControl.TabPages) if (page.Name != tabControl.SelectedTab.Name) tabControl.TabPages.Remove(page);
        }

        public void save(SaveFileDialog saveFileDialog , bool isSaveAs , Document document) {
            saveFileDialog.FileName = tabControl.SelectedTab.Name;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = TypesConstants.FILE_TYPES;
            saveFileDialog.Title = isSaveAs ? "Save As" : "Save";
            if (isSaveAs) {
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    if (saveFileDialog.FileName.Length > 4) {
                        getCurrentDocument.SaveFile(saveFileDialog.FileName , RichTextBoxStreamType.RichText);
                        DocumentDTOImplementation.getInstance().save(document);
                    }
                }
            } else DocumentDTOImplementation.getInstance().update(document , DatabaseConstants.COLUMN_DOCUMENT);
        }

        public void open(OpenFileDialog openFileDialog) {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = TypesConstants.FILE_TYPES;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (openFileDialog.FileName.Length > 4) getCurrentDocument.LoadFile(openFileDialog.FileName , RichTextBoxStreamType.PlainText);
                else MessageBox.Show(DatabaseConstants.INVALID("File"));
            }
        }

        public void undo() => getCurrentDocument.Undo(); 

        public void redo() => getCurrentDocument.Redo(); 

        public void cut() => getCurrentDocument.Cut(); 

        public void copy() => getCurrentDocument.Copy(); 

        public void paste() => getCurrentDocument.Paste(); 

        public void selectAll() => getCurrentDocument.SelectAll(); 

        public void getFontCollection(ToolStripComboBox toolStripComboBox) {
            InstalledFontCollection insFonts = new InstalledFontCollection();
            foreach (FontFamily item in insFonts.Families) toolStripComboBox.Items.Add(item.Name);
            toolStripComboBox.SelectedIndex = 0;
        }

        public void populateFontSizes(ToolStripComboBox toolStripComboBox) {
            for (int i = 1 ; i <= 75 ; ++i) toolStripComboBox.Items.Add(i);
            toolStripComboBox.SelectedIndex = 15;
        }

        private void find(String word , Color highlightColor , Color defaultColor) {
            if (word == "") return;
            unfind(defaultColor);
            int s_start = getCurrentDocument.SelectionStart, startIndex = 0, index;
            while ((index = getCurrentDocument.Text.IndexOf(word , startIndex)) != -1) {
                getCurrentDocument.Select(index , word.Length);
                getCurrentDocument.SelectionColor = highlightColor;
                startIndex = index + word.Length;
            }
        }
        private void unfind(Color color) {
            getCurrentDocument.SelectionStart = 0;
            getCurrentDocument.SelectionLength = getCurrentDocument.TextLength;
            getCurrentDocument.SelectionColor = color;
        }

        private void replace(String s , String t) { getCurrentDocument.Text = getCurrentDocument.Text.Replace(s , t); }

        public void findDialog(Color colorHighlight , Color defaultColor) {
            Form findDialog = new Form { Width = 500 , Height = 160 , Text = "Find and Replace" };
            Label lblFind = new Label() { Left = 10 , Top = 20 , Text = "Find :" , Width = 100 };
            Label lblReplace = new Label() { Left = 10 , Top = 50 , Text = "Replace :" , Width = 100 };
            TextBox txtFind = new TextBox() { Left = 150 , Top = 20 , Width = 300 };
            TextBox txtReplace = new TextBox() { Left = 150 , Top = 50 , Width = 300 };
            Button btnFind = new Button() { Text = "Find" , Left = 350 , Width = 100 , Top = 90 };
            Button btnReplace = new Button() { Text = "Replace" , Left = 250 , Width = 100 , Top = 90 };

            btnFind.Click += (object sender , EventArgs e) => find(txtFind.Text , colorHighlight , defaultColor);
            btnReplace.Click += (object sender , EventArgs e) => replace(txtFind.Text , txtReplace.Text);

            findDialog.Controls.Add(btnFind);
            findDialog.Controls.Add(btnReplace);
            findDialog.Controls.Add(lblFind);
            findDialog.Controls.Add(lblReplace);
            findDialog.Controls.Add(txtFind);
            findDialog.Controls.Add(txtReplace);
            findDialog.Show();

            findDialog.FormClosed += (object sender , FormClosedEventArgs e) => unfind(defaultColor);
        }
    }
}
