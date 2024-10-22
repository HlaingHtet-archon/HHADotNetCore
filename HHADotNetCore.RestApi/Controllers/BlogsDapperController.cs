using Dapper;
using HHADotNetCore.Database.Models;
using HHADotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=HHADotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                                FROM [dbo].[Tbl_Blog] 
                                WHERE DeleteFlag = 0;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var blogs = db.Query<BlogViewModel>(query).ToList();

                return Ok(blogs);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                                FROM [dbo].[Tbl_Blog] 
                                WHERE BlogId = @BlogId AND DeleteFlag = 0;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {            
                var blog = db.QueryFirstOrDefault<BlogViewModel>(query, new { BlogId = id });

                if (blog == null)
                {
                    return NotFound();
                }

                return Ok(blog);
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag])
                                 VALUES
                                   (@BlogTitle, @BlogAuthor, @BlogContent, 0);";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, blog);
                return Ok(result > 0 ? "Creating Successful." : "Creating Failed.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                                   SET BlogTitle = @BlogTitle,
                                       BlogAuthor = @BlogAuthor,
                                       BlogContent = @BlogContent
                                 WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content,
                    BlogId = id
                });

                return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";

            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += "BlogTitle = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += "BlogAuthor = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += "BlogContent = @BlogContent, ";
            }

            if (string.IsNullOrEmpty(conditions))
            {
                return BadRequest("Invalid Parameter!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog] 
                           SET {conditions} 
                         WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new { 
                    BlogTitle = blog.Title, 
                    BlogAuthor = blog.Author, 
                    BlogContent = blog.Content, 
                    BlogId = id });

                return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                                   SET DeleteFlag = 1
                                 WHERE BlogId = @BlogId;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new { BlogId = id });

                return Ok(result == 1 ? "Deleting Successful." : "Deleting Failed.");
            }
        }
    }
}
