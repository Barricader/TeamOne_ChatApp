using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Database
{
    class DbConnect
    {
        //here is the link to the msi needed to use these commands.
        //https://dev.mysql.com/get/Downloads/Connector-Net/mysql-connector-net-6.9.9.msi
        MySqlCommand cmd;
        MySqlConnection conn;
        public void ConnectDB()
        {
            string connectStr = "server=localhost;user=root;database=test;port=3306;password=DarkFantom10;";
            conn = new MySqlConnection(connectStr);
            try
            {
                Console.WriteLine("Conneting to database...");
                conn.Open();
                string createTable = "create table testCode (swiggity int(15), name char(14))";
                cmd = new MySqlCommand(createTable, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + "---" + rdr[1]);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
        public void CreateUser()
        {
            string user = "testUser";
            string pass = "password";
            string dbLocation = "localhost";
            string createUser = "create user \'$user\'@'localhost' identified by \'$pass\'";
            cmd = new MySqlCommand(createUser, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "---" + rdr[1]);
            }
            rdr.Close();
            conn.Close();
        }
    }
}
