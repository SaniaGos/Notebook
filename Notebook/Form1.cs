using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Notebook
{
    public partial class Form1 : Form
    {
        private string path;
        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = DateTime.Now.ToShortDateString();
        }

        private void textBoxOwner_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Sumbols: " + Convert.ToString(textBoxOwner.Text.Length);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://google.com/");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void SaveDialog(SaveFileDialog save)
        {
            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save.FileName, textBoxOwner.Text);
                path = save.FileName;
                MessageBox.Show(save.FileName, "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Text = $"Notebook   {path}";
                textBoxOwner.Modified = false;
            }
        }
        private void SaveFile()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text (*.txt) | *.txt";
            save.FileName = path;
            if (path == "NewText.txt")
            {
                SaveDialog(save);
            }
            else
            {
                File.WriteAllText(path, textBoxOwner.Text);
            }
            
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text (*.txt) | *.txt";
            save.FileName = path;
            if (save.ShowDialog() == DialogResult.OK)
            {
                SaveDialog(save);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (textBoxOwner.Modified)
            {
                DialogResult rez = MessageBox.Show("Save file ?", "File", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rez == DialogResult.OK)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
            }
            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.FileName;
                textBoxOwner.Text = File.ReadAllText(path, Encoding.Default);
                this.Text = $"Notebook   {path}";
                textBoxOwner.Modified = false;
            }
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxOwner.Copy();
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxOwner.Paste();
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxOwner.Cut();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxOwner.Undo();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CloseFile()
        {
            if (textBoxOwner.Modified)
            {
                DialogResult result = MessageBox.Show("Save file?", "File modified", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SaveFile();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            fontToolStripMenuItem.Text = $"Font -> {textBoxOwner.Font.Size}";
            path = "NewText.txt";
            this.Text = $"Notebook   {path}";
        }
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = fontDialog.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                textBoxOwner.Font = fontDialog.Font;
                fontToolStripMenuItem.Text = $"Font size: {textBoxOwner.Font.Size}";
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseFile();
        }
    }
}
