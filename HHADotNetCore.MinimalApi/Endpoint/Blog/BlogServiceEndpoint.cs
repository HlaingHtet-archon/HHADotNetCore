﻿using HHADotNetCore.Domain.Features.Blog;

namespace HHADotNetCore.MinimalApi.Endpoint.Blog;

public static class BlogServiceEndpoint
{
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            BlogService service = new BlogService();
            var lst = service.GetBlogs();
            return Results.Ok(lst);
        })
.WithName("GetBlogs")
.WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();
            var lst = service.GetBlog();
            if (lst == null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok(lst);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            AppDbContext db = new AppDbContext();
            db.TblBlogs.Add(blog);
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("CreateBlogs")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            AppDbContext db = new AppDbContext();
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

        app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
        {
            AppDbContext db = new AppDbContext();
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

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
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
