using HHADotNetCore.Database.Models;
using HHADotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=HHADotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where deleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                //lst.Add(new BlogViewModel
                //{
                //    Id = Convert.ToInt32(reader["BlogId"]),
                //    Title = Convert.ToString(reader["BlogTitle"]),
                //    Author = Convert.ToString(reader["BlogAuthor"]),
                //    Content = Convert.ToString(reader["BlogContent"]),
                //    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                //});
                var item = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                };
                lst.Add(item);
            }

            connection.Close();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogViewModel lst = null;

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
        ,[BlogTitle]
        ,[BlogAuthor]
        ,[BlogContent]
        ,[DeleteFlag]
    FROM [dbo].[Tbl_Blog]
    WHERE [BlogId] = @id AND [DeleteFlag] = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                };
            }

            connection.Close();

            if (lst == null)
            {
                return NotFound();
            }

            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog] ([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag])
                     VALUES (@BlogTitle, @BlogAuthor, @BLogContent, 0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BLogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Creating Successful." : "Creating Failed.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog] 
                SET [BlogTitle] = @BlogTitle, 
                [BlogAuthor] = @BlogAuthor, 
                [BlogContent] = @BlogContent 
                WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();
            return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";
            if(!string.IsNullOrEmpty( blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if(conditions.Length == 0)
            {
                return BadRequest("Invalid Parameter!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
        }

        [HttpDelete("{int}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog] SET DeleteFlag = 1 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(result == 1 ? "Deleting Successful." : "Deleting Failed.");
        }
    }
}