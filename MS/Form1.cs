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

namespace MS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dic;
        private void button1_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            string [] words = null;
            char[] sep = ",! .-!?:;+".ToArray();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    filePath = openFileDialog.FileName;
            }

            if (filePath != string.Empty)
                words = File.ReadAllText(filePath, Encoding.GetEncoding(1251)).Split(sep, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();

            if(words.Count()>0)
            {
                dataGridView1.RowCount = words.Count();

                for (int i = 0; i < words.Count(); i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = words[i];
                    var rez = dic.Where(x => x.Key == words[i]).ToDictionary(x => x.Key, x => x.Value);
                    if (rez.Count > 0)
                        dataGridView1.Rows[i].Cells[1].Value = rez.First().Value;
                    else
                        dataGridView1.Rows[i].Cells[1].Value = "Слово не найдено";

                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dic = new Dictionary<string, string>();
            string[] line = File.ReadAllLines("dic.txt", Encoding.GetEncoding(1251));
            for(int i=0; i<line.Count(); i++)
            {
                string[] item = line[i].Split('|');
                if (!dic.ContainsKey(item[0].Trim()))
                    dic.Add(item[0].Trim(), item[1].Trim());
            }
        }
    }
}
