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
            string user = "testUsername";
            string pass = "password";
            string dbLocation = "localhost";

            Testing t = new Testing();
            t.connectDB();
            t.createUser(user, pass);
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
        public void createUser(string username, string pass)
        {
            string createUser = "create user @username @'localhost' identified by @pass";
            cmd = new MySqlCommand(createUser, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", username));
            cmd.Parameters.Add(new MySqlParameter("@pass", pass));
            //cmd.Parameters.Add(new MySqlParameter("@dbLocation", dbLocation));
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "---" + rdr[1]);
            }
            rdr.Close();

            string userPermissions = "grant select, insert on test.* from @username@'localhost'";
            cmd = new MySqlCommand(userPermissions, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", username));
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "---" + rdr[1]);
            }
            rdr.Close();

        }
    }
}
