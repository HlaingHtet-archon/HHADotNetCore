//using HHADotNetCore.Domain.Features.Blog;

//namespace HHADotNetCore.MinimalApi.Endpoint.Blog;

//public static class BlogServiceEndpoint
//{
//    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
//    {
//        app.MapGet("/blogs", () =>
//        {
//            BlogService service = new BlogService();
//            var lst = service.GetBlogs();
//            return Results.Ok(lst);
//        })
//.WithName("GetBlogs")
//.WithOpenApi();

//        app.MapGet("/blogs/{id}", (int id) =>
//        {
//            BlogService service = new BlogService();
//            var lst = service.GetBlog();
//            if (lst == null)
//            {
//                return Results.BadRequest("No data found.");
//            }

//            return Results.Ok(lst);
//        })
//        .WithName("GetBlog")
//        .WithOpenApi();

//        app.MapPost("/blogs", (TblBlog blog) =>
//        {
//            BlogService service = new BlogService();
//            var model = service.CreateBlog(blog);
//            return Results.Ok(model);
//        })
//        .WithName("CreateBlogs")
//        .WithOpenApi();

//        _ = app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
//        {
//            BlogService service = new BlogService();
//            var item = service.UpdateBlog(id, blog);

//            if (item is null)
//            {
//                return Results.BadRequest("No data found.");
//            }

//            return Results.Ok(blog);
//        })
//        .WithName("UpdateBlogs")
//        .WithOpenApi();

//        app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
//        {
//            BlogService service = new BlogService();
//            var item = service.PatchBlog(id, blog);

//            if (item is null)
//            {
//                return Results.BadRequest("No data found.");
//            }

//            return Results.Ok(blog);
//        })
//        .WithName("UpdateBlog")
//        .WithOpenApi();

//        app.MapDelete("/blogs/{id}", (int id) =>
//        {
//            BlogService service = new BlogService();
//            var item = service.DeleteBlog(id);

//            if (item is null)
//            {
//                return Results.BadRequest("No data found.");
//            }

//            return Results.Ok();
//        })
//        .WithName("DeleteBlogs")
//        .WithOpenApi();
//    }
//}
