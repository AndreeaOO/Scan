using MySql.Data.MySqlClient;
using System;

namespace AdoExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = 
                "server=localhost;database=northwind;uid=bulskov;pwd=henrik;SslMode=none";

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("select * from categories", conn))
                {
                    // cmd.Connection = conn;
                    // cmd.CommandText = "select * from categories";

                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Console.WriteLine(reader.GetString(1));
                    }


                }

            }

        }
    }
}
