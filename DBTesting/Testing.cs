using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTesting
{
    class Testing
    {
        static void Main(string[] args)
        {
            Testing t = new Testing();
            t.connectDB();
            t.createUser();
        }
        MySqlCommand cmd;
        MySqlConnection conn;
        public void connectDB()
        {
            string connectStr = "server=localhost;user=root;database=test;port=3306;password=DarkFantom10;";
            conn = new MySqlConnection(connectStr);
            try
            {
                Console.WriteLine("Conneting to database...");
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public void createUser()
        {
            MySqlDataReader rdr = cmd.ExecuteReader();
            string user = "set @username = 'testUser'";
            string pass = "set @pass = 'password'";
            string dbLocation = "localhost";
            string createUser = "create user @username @'localhost' identified by @pass";
            cmd = new MySqlCommand(user, conn);
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "---" + rdr[1]);
            }
            rdr.Close();
            conn.Close();
        }
    }
}
