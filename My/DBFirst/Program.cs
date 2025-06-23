using DBFirst.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region 設定區域
//讀取appsettings.json 方法1
//builder.Services.AddDbContext<dbStudentsContext>(Options=>
//    Options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionStrings")));

//讀取appsettings.json 方法2
var DBConfig = builder.Configuration.GetSection("DBConnectionData");
string DB_Server = DBConfig["DB_Server"];
string DB_Name = DBConfig["DB_Name"];
string DB_User = DBConfig["DB_User"];
string DB_Password = DBConfig["DB_Password"];

string ConnectionStr = $"Data Source={DB_Server};Database={DB_Name};TrustServerCertificate=True;User ID={DB_User};Password={DB_Password}";

builder.Services.AddDbContext<dbStudentsContext>(Options =>
    Options.UseSqlServer(ConnectionStr));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
