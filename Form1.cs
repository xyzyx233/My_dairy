using My_dairy.support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My_dairy
{
    public partial class Form1 : Form
    {
        private UserBean u;
        public Form1()
        {
            InitializeComponent();
            u = new UserBean();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBhelper.ini();
            //d.createuser("test", "12345678");
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.accept += new EventHandler(f2_accept);
            f2.Show();
            Console.WriteLine("login");
        }
        void f2_accept(object sender, EventArgs e)
        {
            //事件的接收者通过一个简单的类型转换得到Form2的引用 
            Form2 f2 = (Form2)sender;
            //接收到Form2的textBox1.Text 
            this.u = f2.uu;
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!u.isempty())
            {
                this.Close();
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("是否退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (dr == DialogResult.Yes)
                {
                    DialogResult dr1 = MessageBox.Show("是否进行保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                    if (dr1 == DialogResult.Yes)
                    {
                        button1_Click_1(sender, e);
                    }
                    this.Close();
                    return;
                }
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            controller c = new controller();
            Console.WriteLine(textBox1.Text);
            Console.WriteLine(u.Username + " " + u.Password);
            c.save(textBox1.Text, u);
        }

        private void checklogin_Click(object sender, EventArgs e)
        {
            controller c = new controller();
            Console.WriteLine(u.Username + " " + u.Password);
            if (c.checkuser(u))
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
            }
        }
    }
}
