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
    public partial class Form2 : Form
    {
        public event EventHandler accept;
        public UserBean uu;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text != "" && passwd.Text != "")
            {
                uu = new UserBean(username.Text, passwd.Text);
                if (accept != null)
                {
                    accept(this, EventArgs.Empty); //当窗体触发事件，传递自身引用 
                }
                this.Close();
            }
             
        }
    }
}
