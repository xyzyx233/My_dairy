using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My_dairy.support
{
    class filehelper
    {
        private string path = "./data";

        public string Path
        {
            set{ path = value; }
        }
        public string writefile(string context, UserBean u)
        {
            randomlength r = new randomlength(10, true);
            string filename = r.getrand();
            Console.WriteLine(filename);
            //string filename ="hello";
            DBhelper d = new DBhelper();
            while (DBhelper.queryfile(filename,u))
            {
                filename = r.getrand();
            }
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(context);
            using (FileStream fsWrite = new FileStream(path+"/"+filename, FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            };
            if(!DBhelper.insertd(filename, u))
            {
                MessageBox.Show("保存文件失败！");
            }
            else
            {
                MessageBox.Show("保存成功！");
            }
                return filename;
        }
        public string readfile(string filename, UserBean u)
        {
            using (FileStream fsRead = new FileStream(path+"/"+filename, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                return myStr;
            }
        }
        public int filesize(string filename)
        {
            FileInfo f = new FileInfo(path + "/" + filename);
            return (int)f.Length;
        }
    }
}
