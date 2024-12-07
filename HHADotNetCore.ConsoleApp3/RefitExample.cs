using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.ConsoleApp3
{
    public class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("http://localhost:5241");
            var lst = await blogApi.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogTitle);
            }

            var item2 = await blogApi.GetBlog(1);
            try
            {
                var item3 = await blogApi.GetBlog(100);
            }
            catch (ApiException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data found.");
                }
            }

            var item4 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "Task",
                BlogAuthor = "Task",
                BlogContent = "Task",
            });

            var item5 = await blogApi.PutBlog(2, new BlogModel
            {
                BlogTitle = "Task",
                BlogAuthor = "Task",
                BlogContent = "Task",
            });

            var item6 = await blogApi.PatchBlog(2, new BlogModel
            {
                BlogTitle = "Task",
                BlogAuthor = "Task",
                BlogContent = "Task",
            });

            var item7 = await blogApi.DeleteBlog(13);
        }
    }
}
