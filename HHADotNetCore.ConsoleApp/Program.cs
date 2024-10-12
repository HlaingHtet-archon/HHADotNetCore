// See https://aka.ms/new-console-template for more information
using HHADotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("a", "b", "c");
dapperExample.Edit(6);
dapperExample.Edit(1);
//dapperExample.Create("aaa", "bbb", "ccc");
//dapperExample.Delete(6);


Console.ReadKey();