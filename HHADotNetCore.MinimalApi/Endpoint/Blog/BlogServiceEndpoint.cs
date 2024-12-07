using Microsoft.AspNetCore.Mvc;
using HHADotNetCore.Domain.Features.Blog;

namespace HHADotNetCore.MinimalApi.Endpoint.Blog;

public static class BlogServiceEndpoint
{
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] IBlogService service) =>
        {
            var lst = service.GetBlogs();
            return Results.Ok(lst);
        })
.WithName("GetBlogs")
.WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var lst = service.GetBlog();
            if (lst == null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok(lst);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog blog) =>
        {
            var model = service.CreateBlog(blog);
            return Results.Ok(model);
        })
        .WithName("CreateBlogs")
        .WithOpenApi();

        _ = app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var item = service.UpdateBlog(id, blog);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok(blog);
        })
        .WithName("UpdateBlogs")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var item = service.PatchBlog(id, blog);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok(blog);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var item = service.DeleteBlog(id);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok();
        })
        .WithName("DeleteBlogs")
        .WithOpenApi();
    }
}
