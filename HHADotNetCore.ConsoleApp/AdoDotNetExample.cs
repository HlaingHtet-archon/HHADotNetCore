﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=HHADotNetCore;User ID=sa;Password=sasa@123;";
        public void Read()
        {
            Console.WriteLine("Connection String: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection opening...");
            connection.Open();
            Console.WriteLine("Connection opened");

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where deleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
            }

            Console.WriteLine("Connection closing...");
            connection.Close();
            Console.WriteLine("Connection closed");

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //}
        }

        public void Create()
        {
            Console.WriteLine("BlogTitle: ");
string title = Console.ReadLine();

Console.WriteLine("BlogAuthor: ");
string author = Console.ReadLine();

Console.WriteLine("BlogContent: ");
string content = Console.ReadLine();

SqlConnection connection = new SqlConnection(_connectionString);
connection.Open();

//string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
//           ([BlogTitle]
//           ,[BlogAuthor]
//           ,[BlogContent]
//           ,[DeleteFlag])
//     VALUES
//           ('{title}'
//           ,'{author}'
//           ,'{content}'
//           ,0)";

string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";


SqlCommand cmd = new SqlCommand(query, connection);
cmd.Parameters.AddWithValue("@BlogTitle", title);
cmd.Parameters.AddWithValue("@BlogAuthor", author);
cmd.Parameters.AddWithValue("@BlogContent", content);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
//DataTable dt = new DataTable();
//adapter.Fill(dt);

int result = cmd.ExecuteNonQuery();

connection.Close();

//if (result == 1)
//{
//    Console.WriteLine("Saving Successful.");
//}
//else
//{
//    Console.WriteLine("Saving Failed.");
//}

Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
        }
    }
}