using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My_dairy.support
{
    class randomlength
    {
        private int len;
        private bool ContainAlpha;
        public randomlength(int x,bool y)
        {
            len = x;
            ContainAlpha = y;
        }
        public randomlength()
        {
            len = 8;
            ContainAlpha = false;
        }
        public string getrand()
        {
            string result="";
            string box = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] abox = box.ToCharArray();
            Random r1 = new Random();
            if (!ContainAlpha)
            {
                for(int i = 0; i < len; i++)
                {
                    result +=r1.Next(0, 9);
                }
            }
            else
            {
                for(int i = 0; i < len; i++)
                {
                    result += abox[r1.Next(0, 61)];
                }
            }
            return result;
        }
    }
}
