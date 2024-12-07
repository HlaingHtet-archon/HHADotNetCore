using HHADotNetCore.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace HHADotNetCore.MinimalApi.Endpoint.Blog;

public static class BlogEndpoint
{
    public static void UseBlogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            var model = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(model);
        })
.WithName("GetBlogs")
.WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var item = db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog blog) =>
        {
            db.TblBlogs.Add(blog);
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("CreateBlogs")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var item = db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return Results.BadRequest("No data found.");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            db.Entry(item).State = EntityState.Modified;

            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("UpdateBlogs")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var item = db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return Results.BadRequest("No data found.");
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            db.Entry(item).State = EntityState.Modified;

            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var item = db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return Results.BadRequest("No data found.");
            }

            db.Entry(item).State = EntityState.Deleted;

            db.SaveChanges();
            return Results.Ok();
        })
        .WithName("DeleteBlogs")
        .WithOpenApi();
    }
}
