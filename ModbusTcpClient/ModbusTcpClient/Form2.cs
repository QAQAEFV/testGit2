using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusTcpClient
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string str)
        {
            InitializeComponent();
            textBox1.Text = str;
        }
        int flag;
        private void button1_Click(object sender, EventArgs e)
        {
            flag = 0;
            Form1.tempStr = textBox1.Text;
            Form1 parentForm = this.Owner as Form1;
            if(Convert.ToInt32(textBox1.Text) >= (-32768) && Convert.ToInt32(textBox1.Text) <= 32767)
            {
                parentForm.WriteRegister("6", Form1.RowNumber, textBox1.Text);
                flag++;
            }
            else
            {
                MessageBox.Show("数值应在-32768到32767之间！");
            }
            if(flag == 1)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
