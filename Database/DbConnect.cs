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
        static void Main(string[] args)
        {
            string connectStr = "server=localhost;user=root;database=test;port=3306;password=DarkFantom10;";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                Console.WriteLine("Conneting to database...");
                conn.Open();
                string createTable = "create table testCode (swiggity int(15), name char(14))";
                MySqlCommand cmd = new MySqlCommand(createTable, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + "---" + rdr[1]);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
