﻿using Dapper;
using HHADotNetCore.ConsoleApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=HHADotNetCore;User ID=sa;Password=sasa@123;";
        public void Read()
        {
            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "select * from tbl_blog where DeleteFlag = 0;";
            //    var lst = db.Query(query).ToList();
            //    foreach (var item in lst)
            //    {
            //        Console.WriteLine(item.BlogId);
            //        Console.WriteLine(item.BlogTitle);
            //        Console.WriteLine(item.BlogAuthor);
            //        Console.WriteLine(item.BlogContent);
            //    }
            //}

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDapperDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }

            //DTO = data transfer object
        }

        public void Create(string title, string author, string content)
        {
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDapperDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
            }
        }

        public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId;";
                var item = db.Query<BlogDapperDataModel>(query, new BlogDapperDataModel
                {
                    BlogId = id
                }).FirstOrDefault();

                //if (item == null)
                if (item is null)
                {
                    Console.WriteLine("No data found.");
                    return;
                }
                
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                   
            }
        }

        public void Update(int id,string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDapperDataModel
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
            }
        }

        public void Delete(int id)
        {
            string query = $@"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDapperDataModel { BlogId = id });
                Console.WriteLine(result == 1 ? "deleting Successful." : "Deleting Failed.");
            }
        }
    }
}