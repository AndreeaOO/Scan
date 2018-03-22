using MySql.Data.MySqlClient;
using System;

namespace AdoExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = 
                "server=localhost;database=scan;uid=andreea;pwd=password;SslMode=none";

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("select * from products", conn))
                {
                   

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
