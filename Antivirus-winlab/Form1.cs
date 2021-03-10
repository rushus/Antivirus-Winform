using System;
using System.Threading;
using System.Windows.Forms;

namespace Antivirus_winlab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "===Начало сканирования===\r\n";
            string textdirectory = textBox1.Text.Replace("\\","/");
            Antivirus antivirus = new Antivirus();
            Thread ThreadAntivirus = new Thread((ThreadStart)delegate { Antivirus.IteratingFiles(textdirectory, this); });
            ThreadAntivirus.IsBackground = true;
            ThreadAntivirus.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void UpdateTxt(string str)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTxt), new object[] { str });
                return;
            }
            textBox2.AppendText(str);
        }
        public void UpdateTxt2(string str)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTxt2), new object[] { str });
                return;
            }
            textBox3.AppendText(str);
        }
    }
}
