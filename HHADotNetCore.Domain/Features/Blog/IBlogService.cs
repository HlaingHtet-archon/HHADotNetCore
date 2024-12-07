using HHADotNetCore.Database.Models;

namespace HHADotNetCore.Domain.Features.Blog
{
    public interface IBlogService
    {
        TblBlog CreateBlogs(TblBlog blog);
        bool? DeleteBlog(int id);
        TblBlog GetBlog(int id);
        List<TblBlog> GetBlogs();
        TblBlog PatchBlog(int id, TblBlog blog);
        TblBlog UpdateBlog(int id, TblBlog blog);
    }
}