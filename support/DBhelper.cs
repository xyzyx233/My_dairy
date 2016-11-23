using System;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Data;

namespace My_dairy.support
{
    public class DBhelper
    {
        private static string strDataSource = "./data/database.db";//SQLite数据库文件存放物理地址
        //private string password ="123456";
        private static string createuserdatabase = "create table if not exists user (name varchar(30),password varchar(8), rootbase varchar(10));";
        private static string inusersql = "INSERT INTO USER VALUES('@name','@pwd','@rootname')";
        private static string createusertable = "create table if not exists @username ( date varchar(8),filename varchar(20) ) ";
        private static string insertdairy = "  insert into @username values( '@time', '@filename')";
        private static string queryfilename = "select count(*) from @username where filename like '@filename'";
        private static string queryuser = "select count(*) from user where name like '@username'";
        private static SQLiteConnection GetSQLiteConnection()

        {

            return new SQLiteConnection("Data Source=" + strDataSource + "; Pooling = true; FailIfMissing = false");

        }
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params object[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (p != null)
            {
                foreach (object parm in p)
                    cmd.Parameters.AddWithValue(string.Empty, parm);
            }

        }/// <summary>

        /// 返回受影响的行数

        /// </summary>

        /// <param name="cmdText">a</param>

        /// <param name="commandParameters">传入的参数</param>

        /// <returns>返回受影响的行数</returns>

        private static int ExecuteNonQuery(string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                PrepareCommand(command, connection, cmdText, p);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 返回SqlDataReader对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        private static SQLiteDataReader ExecuteReader(string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = GetSQLiteConnection();
            try
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }
        /// <summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        private static object ExecuteScalar(string cmdText, params object[] p)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                PrepareCommand(cmd, connection, cmdText, p);
                return cmd.ExecuteScalar();
            }
        }
        public static void ini()
        {
            ExecuteNonQuery(createuserdatabase, null);
        }
        public static bool createuser(string name,string password)
        {
            SQLiteParameter p = new SQLiteParameter("@name", name);
            int count =(int)(Int64) ExecuteScalar(queryuser, p);
            if(count >= 1)
            {
                MessageBox.Show("用户已存在！");
                return false;
            }
            randomlength r = new randomlength(8, true);
            string rootname=r.getrand();
            count =(int)(Int64) ExecuteScalar("select count(*) from user where rootbase like '"+rootname+"'", null);
            while (count != 0)
            {
                rootname = r.getrand();

            }
            string inusersqls = inusersql.Replace("@name", name).Replace("@pwd", password).Replace("@rootname", rootname);
            ExecuteNonQuery(inusersqls, null);
            string createusert = createusertable.Replace("@username", name);
            count = ExecuteNonQuery(createusertable, null);
            if (count != 0)
                return true;
            else
            {
                return false;
            }
        }
        public static bool queryfile(string filename,UserBean user)
        {
            string selectfile = queryfilename.Replace("@username", user.Username).Replace("@filename", filename);
            int count = (int)(Int64)ExecuteScalar(selectfile, null);
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool queryuserinfo(UserBean u)
        {
            string selectuserinfo = queryuser.Replace("@username", u.Username);
            int count = (int)(Int64)ExecuteScalar(selectuserinfo, null);
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool insertd(string fname,UserBean u)
        {
            string insertdairys = insertdairy.Replace("@username", u.Username).Replace("@time", DateTime.Now.ToString("yyyyMMdd")).Replace("@filename", fname);
            int count = ExecuteNonQuery(insertdairys, null);
            if(count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
