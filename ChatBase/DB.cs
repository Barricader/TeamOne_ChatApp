using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Database
{
    class DB
    {
        //here is the link to the msi needed to use these commands.
        //https://dev.mysql.com/get/Downloads/Connector-Net/mysql-connector-net-6.9.9.msi

        MySqlCommand cmd;
        MySqlConnection conn;
        MySqlDataReader rdr;
        static void Main(string[] args)
        {
            string user = "testUsername";
            string pass = "password";
            string dbLocation = "localhost";

            DB t = new DB();
            t.connectDB();
            t.createUser(user, pass);
            //t.dropUser(user, pass);
        }
        
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
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "---" + rdr[1]);
            }
            rdr.Close();

            string userPermissions = "grant select, insert on test.* to @username @'localhost'";
            cmd = new MySqlCommand(userPermissions, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", username));

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "---" + rdr[1]);
            }
            rdr.Close();
            conn.Close();
        }
        public void dropUser(string username, string pass)
        {
            string dropUser = "drop user @username @'localhost'";
            cmd = new MySqlCommand(dropUser, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", username));
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "---" + rdr[1]);
            }
            conn.Close();
        }
    }
}
