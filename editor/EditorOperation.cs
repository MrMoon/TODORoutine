using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TODORoutine.editor {
    class EditorOperation {

        public int tabCounter = 0;
        public TabControl tabControl = null;

        public EditorOperation(TabControl tabControl) {
            this.tabControl = tabControl;
        }

        public RichTextBox getCurrentDocument => (RichTextBox) tabControl.SelectedTab.Controls["Body"];

        public void addTab(ContextMenuStrip menuStrip) {

            RichTextBox body = new RichTextBox();

            body.Name = "Body";
            body.Dock = DockStyle.Fill;
            body.ContextMenuStrip = menuStrip;

            TabPage newPage = new TabPage();
            tabCounter += 1;

            string documentText = "Document " + tabCounter;
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

        public void save(SaveFileDialog saveFileDialog) {
            saveFileDialog.FileName = tabControl.SelectedTab.Name;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "RTF|.rtf";
            saveFileDialog.Title = "Save";

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (saveFileDialog.FileName.Length > 0) getCurrentDocument.SaveFile(saveFileDialog.FileName , RichTextBoxStreamType.RichText);
            }
        }

        public void saveAs(SaveFileDialog saveFileDialog) {
            saveFileDialog.FileName = tabControl.SelectedTab.Name;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";
            saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (saveFileDialog.FileName.Length > 0) getCurrentDocument.SaveFile(saveFileDialog.FileName , RichTextBoxStreamType.PlainText);
            }
        }

        public void open(OpenFileDialog openFileDialog) {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "RTF|*.rtf|Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (openFileDialog.FileName.Length > 9) getCurrentDocument.LoadFile(openFileDialog.FileName , RichTextBoxStreamType.RichText);
            }
        }

        public void undo() { getCurrentDocument.Undo(); }

        public void redo() { getCurrentDocument.Redo(); }

        public void cut() { getCurrentDocument.Cut(); }

        public void copy() { getCurrentDocument.Copy(); }

        public void paste() { getCurrentDocument.Paste(); }

        public void selectAll() { getCurrentDocument.SelectAll(); }

        public void getFontCollection(ToolStripComboBox toolStripComboBox) {
            InstalledFontCollection insFonts = new InstalledFontCollection();
            foreach (FontFamily item in insFonts.Families) toolStripComboBox.Items.Add(item.Name);
            toolStripComboBox.SelectedIndex = 0;
        }

        public void populateFontSizes(ToolStripComboBox toolStripComboBox) {
            for (int i = 1 ; i <= 75 ; i++) toolStripComboBox.Items.Add(i);
            toolStripComboBox.SelectedIndex = 11;
        }
    }
}
