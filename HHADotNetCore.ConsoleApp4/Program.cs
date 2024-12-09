// See https://aka.ms/new-console-template for more information
using HHADotNetCore.ConsoleApp4;
using Microsoft.Extensions.DependencyInjection;



var service1 = new ServiceCollection().AddSingleton<AdoDotNetExample>().BuildServiceProvider();
var adoDotNetExample = service1.GetRequiredService<AdoDotNetExample>();


//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

var service2 = new ServiceCollection().AddSingleton<DapperExample>().BuildServiceProvider();    
var dapperExample = service2.GetRequiredService<DapperExample>();

//dapperExample.Read();
//dapperExample.Create("a1", "b2", "c3");
//dapperExample.Edit(6);
//dapperExample.Edit(1);
//dapperExample.Update(1,"aaa", "bbb", "ccc");
//dapperExample.Delete(6);

var service3 = new ServiceCollection().AddSingleton<EFCoreExample>().BuildServiceProvider();
var efCoreExample = service3.GetRequiredService<EFCoreExample>();

//eFCoreExample.Read();
//eFCoreExample.Create("i334", "235lw", "2oejow");
//eFCoreExample.Edit(1);
//eFCoreExample.Update(3, "jaifdj", "oadfhoew", "ahfeo");
//eFCoreExample.Delete(3);