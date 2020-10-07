using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePad1007
{
    public partial class Form1 : Form
    {
        private bool dirty = false;
        string  editingFileName;
        private string dirtyText= "*{0} - Windows 메모장";
        private string notDirtyText= "{0} - Windows 메모장";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            if (string.IsNullOrEmpty(editingFileName))
                 editingFileName= "제목 없음";
            this.Text = string.Format(notDirtyText, editingFileName);
        }
        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dirty = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                editingFileName = openFileDialog1.FileName;
                try
                {
                    using (StreamReader sr = new StreamReader(editingFileName, Encoding.Default))
                    {
                        textBox1.Text = sr.ReadToEnd();
                    }
                    UpdateText();
                }                                                                  
                catch(Exception err)                                               
                {                                                                  
                    MessageBox.Show(err.Message);                                  
                }                                                                  
            }                                                                      
        }                                                                                                                                    
        private void textBox1_TextChanged(object sender, EventArgs e)              
        {
            UpdateText();
        }
        private void UpdateText()
        {
            int idx = editingFileName.LastIndexOf('\\') + 1;
            string fileNm = editingFileName.Substring(idx);
            if (dirty == true)
            {
                this.Text = string.Format(notDirtyText, fileNm);
                dirty = false;
            }
            else
                this.Text = string.Format(dirtyText, fileNm);

        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(editingFileName=="제목 없음")
            {
                //다른 이름으로 저장하고 동일하게
                다른이름으로저장ToolStripMenuItem_Click(null, null);
            }
            else
            {
                //그 파일명으로 파일 덮어쓰기
                SaveFile(editingFileName);
            }

            
        }
        private void SaveFile(string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName,false, Encoding.Default))
                {
                    sw.Write(textBox1.Text);
                    sw.Flush();
                }
                dirty = true;
                UpdateText();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        private void 다른이름으로저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files(*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                editingFileName = saveFileDialog1.FileName;
                SaveFile(saveFileDialog1.FileName);
            }
        }

    }                                                                              
}                                                                                  
                                                                                   