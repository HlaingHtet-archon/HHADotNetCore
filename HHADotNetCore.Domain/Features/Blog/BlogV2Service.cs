﻿using HHADotNetCore.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HHADotNetCore.Domain.Features.Blog;

public class BlogV2Service : IBlogService
{
    private readonly AppDbContext _db;

    public BlogV2Service(AppDbContext db)
    {
        _db = db;
    }

    public List<TblBlog> GetBlogs()
    {
        var model = _db.TblBlogs.AsNoTracking().ToList();
        return model;
    }

    public TblBlog GetBlog(int id)
    {
        var item = _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);
        return item;
    }

    public TblBlog CreateBlogs(TblBlog blog)
    {
        _db.TblBlogs.Add(blog);
        _db.SaveChanges();
        return blog;
    }

    public TblBlog UpdateBlog(int id, TblBlog blog)
    {
        var item = _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            return null;
        }

        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();
        return item;
    }

    public TblBlog PatchBlog(int id, TblBlog blog)
    {
        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            return null;
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

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        return item;
    }

    public bool? DeleteBlog(int id)
    {
        var item = _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            return null;
        }

        _db.Entry(item).State = EntityState.Deleted;
        int result = _db.SaveChanges();
        return result > 0;
    }
}
