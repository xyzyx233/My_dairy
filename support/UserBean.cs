using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My_dairy.support
{
    public class UserBean
    {
        private string username;
        private string password;
        public UserBean(string username="",string password="")
        {
            this.username = username;
            this.password = password;
        }
        public string Username
        {
            set { this.username = value; }
            get { return username; }
        }
        public string Password
        {
            set { this.password = value; }
            get { return password; }
        }
        public bool isempty()
        {
            if (username == "" || password == "")
                return false;
            return true;
        }
    }
}
