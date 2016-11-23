using My_dairy.support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My_dairy
{
    class controller
    {
        public bool checkuser(UserBean u)
        {
            if (!u.isempty())
                return false;
            if (DBhelper.queryuserinfo(u))
            {
                MessageBox.Show("检查通过！");
                return true;
            }
            else
            {
                MessageBox.Show("检查有问题！");
                return false;
            }
        }
        public bool save(string context, UserBean u)
        {
            filehelper f = new filehelper();
            code c = new code(u.Password, DateTime.Now.ToString("yyyyMMdd"));
            string name = f.writefile(c.encodefile(context), u);
            Console.WriteLine(name);
            if (f.filesize(name) != 0)
                return true;
            return false;
        }
    }
}
