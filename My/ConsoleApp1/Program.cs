// See https://aka.ms/new-console-template for more information


using ConsoleApp1;
using System.Diagnostics;

Thread T = new Thread(() =>
{
    //int a=0;

    while (true)
    {
        //a++;
        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        Thread.Sleep(1000);
    }
});
T.Start();

Person[] PA = new Person[]
{
    new Person(),
    new Student(),
    new CollegeStudent()
};
for (int a = 0; a < PA.Length; a++)
{
    PA[a].Call();
}

CheckIDCS.CheckID("E124300986");


Console.WriteLine("Hello, World!");
Console.ReadLine();


