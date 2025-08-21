using HotelSystem.Access.Data;
using HotelSystem.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add<LogFilter>();
});


builder.Services.AddDbContext<HotelSysDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelSysDBConnection")));


builder.Services.AddDbContext<HotelSysDBContext2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelSysDBConnection")));


builder.Services.AddAuthentication("MemberLogin").AddCookie("MemberLogin", options =>
{
    options.LoginPath = "/User/Login/Login"; // 設定登入頁面路徑(若需登入而未登入則強制導到此路徑)
    options.LogoutPath = "/User/Login/Logout"; // 設定登出頁面路徑
    options.AccessDeniedPath = "/Home/Index"; // 設定存取拒絕頁面路徑(若已登入但角色權限不符,則強制導到此路徑)
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();





// 1. 先註冊特殊路由
//多層次路由
app.MapControllerRoute(
    name: "EmployeeRole",
    pattern: "Manager/{action=Index}/{id?}",
    defaults: new {area="Admin", controller = "EmployeeRoles" });



//自訂靜態路徑
app.MapControllerRoute(
    name: "about",
    pattern: "about-us",
    defaults: new {  controller = "Home", action = "Privacy" });



// 帶有多個參數的路由
app.MapControllerRoute(
    name: "order",
    pattern: "order/{orderId}/{productId}",
    defaults: new { controller = "Order", action = "Detail" });




//2. 再註冊區域路由
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

// 3. 最後註冊預設路由

app.MapControllerRoute(
    name: "aaa",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



//app.MapControllerRoute(
//    name: "default",
//    pattern: "{area}/{controller}/{action}/{id?}");





app.Run();
