﻿using HHADotNetCore.Database.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var lst = db.TblBlogs.ToList();

var blog = new BlogModel
{
    Id = 1,
    Title = "Test Title",
    Author = "Test Author",
    Content = "Test Content",
};

string jsonStr = blog.toJson();

Console.WriteLine(jsonStr);

string jsonStr2 = """{"Id":1,"Title":"Test Title","Author":"Test Author","Content":"Test Content"}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2);

Console.WriteLine(blog2.Title);

Console.ReadLine();

public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public static class Extensions //DevCode
{
    public static string toJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj); // C# to Json
        return jsonStr;
    }
}