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

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("a", "b", "c");
//dapperExample.Edit(6);
//dapperExample.Edit(1);
//dapperExample.Update(1,"aaa", "bbb", "ccc");
//dapperExample.Delete(6);

EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Read();
//eFCoreExample.Create("i334", "235lw", "2oejow");
//eFCoreExample.Edit(1);
//eFCoreExample.Update(3, "jaifdj", "oadfhoew", "ahfeo");
eFCoreExample.Delete(3);


Console.ReadKey();