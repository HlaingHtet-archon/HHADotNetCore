using Dapper;
using HHADotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using ToDoList.Database.Models;
using Microsoft.AspNetCore.Http;

namespace HHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=ToDoList;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"SELECT [TaskID],[TaskTitle],[TaskDescription],[CategoryID],[PriorityLevel],[Status],[DueDate],[CreatedDate],[CompletedDate],[DeleteFlag]
  FROM [dbo].[Tbl_ToDoList] WHERE DeleteFlag = 0;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var tasks = db.Query<ToDoListViewModel>(query).ToList();

                return Ok(tasks);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = @"SELECT [TaskID],[TaskTitle],[TaskDescription],[CategoryID],[PriorityLevel],[Status],[DueDate],[CreatedDate],[CompletedDate],[DeleteFlag]
  FROM [dbo].[Tbl_ToDoList] WHERE TaskID = @TaskID AND DeleteFlag = 0;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var task = db.QueryFirstOrDefault<ToDoListViewModel>(query, new { TaskId = id });

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(TblToDoList task)
        {
            string query = @"INSERT INTO [dbo].[Tbl_ToDoList]
           ([TaskTitle],[TaskDescription],[CategoryID],[PriorityLevel],[Status],[DueDate],[CreatedDate],[CompletedDate],[DeleteFlag])
     VALUES
           (@TaskTitle, @TaskDescription, @CategoryID, @PriorityLevel, 'In Progress', @DueDate, @CreatedDate, @CompletedDate, 0);";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, task);
                return Ok(result > 0 ? "Creating Successful." : "Creating Failed.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, ToDoListViewModel task)
        {
            string query = @"UPDATE [dbo].[Tbl_ToDoList]
   SET [TaskTitle] = @TaskTitle,
       [TaskDescription] = @TaskDescription, 
       [CategoryID] = @CategoryID, 
       [PriorityLevel] = @PriorityLevel, 
       [Status] = @Status, 
       [DueDate] = @DueDate, 
       [CreatedDate] = @CreatedDate, 
       [CompletedDate] = @CompletedDate
 WHERE TaskID = @TaskID AND DeleteFlag = 0";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    TaskTitle = task.TaskTitle,
                    TaskDescription = task.TaskDescription,
                    CategoryID = task.CategoryID,
                    PriorityLevel = task.PriorityLevel,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    CreatedDate = task.CreatedDate,
                    CompletedDate = task.CompletedDate,
                    TaskID = id
                });

                return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, ToDoListViewModel task)
        {
            string conditions = "";

            if (!string.IsNullOrEmpty(task.TaskTitle))
            {
                conditions += "TaskTitle = @TaskTitle, ";
            }
            if (!string.IsNullOrEmpty(task.TaskDescription))
            {
                conditions += "TaskDescription = TaskDescription, ";
            }
            if (task.CategoryID.HasValue)
            {
                conditions += "CategoryID = @CategoryID, ";
            }
            if (task.PriorityLevel > 0)
            {
                conditions += "PriorityLevel = @PriorityLevel, ";
            }
            if (!string.IsNullOrEmpty(task.Status))
            {
                conditions += "Status = @Status, ";
            }
            if (task.DueDate.HasValue)
            {
                conditions += "DueDate = @DueDate, ";
            }
            if (task.CompletedDate.HasValue)
            {
                conditions += "CompletedDate = @CompletedDate, ";
            }

            if (string.IsNullOrEmpty(conditions))
            {
                return BadRequest("Invalid Parameter!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_ToDoList] 
                    SET {conditions} 
                    WHERE TaskID = @TaskID";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    TaskTitle = task.TaskTitle,
                    TaskDescription = task.TaskDescription,
                    CategoryID = task.CategoryID,
                    PriorityLevel = task.PriorityLevel,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    CompletedDate = task.CompletedDate,
                    TaskID = id
                });

                return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"UPDATE [dbo].[Tbl_ToDoList] 
                                   SET DeleteFlag = 1
                                 WHERE TaskID = @TaskID;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new { TaskId = id });

                return Ok(result == 1 ? "Deleting Successful." : "Deleting Failed.");
            }
        }
    }
}
