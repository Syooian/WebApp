using System.Net;
using Microsoft.AspNetCore.Mvc.Authorization;

using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(//options =>
                                         //options.Filters.Add(new AuthorizeFilter())  //用Filter的方式套用全域驗證
);

//1.2.4 在Program.cs內以依賴注入的寫法註冊讀取連線字串的服務(food panda、Uber Eats)
//※注意程式的位置必須要在var builder = WebApplication.CreateBuilder(args);這句之後且在var app = builder.Build();之前
builder.Services.AddDbContext<GuestBookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GuestBookConnection")));

//5.2.6 在 Program.cs 註冊 Cookie Authentication
builder.Services.AddAuthentication("ManagerLogin").AddCookie("ManagerLogin", options =>
    {
        options.LoginPath = "/Login/Login"; // 設定登入頁面路徑(若需登入而未登入則強制導到此路徑)
        options.LogoutPath = "/Login/Logout"; // 設定登出頁面路徑
        options.AccessDeniedPath = "/Home/Index"; // 設定存取拒絕頁面路徑(若已登入但角色權限不符,則強制導到此路徑)
    });




/////////////////////////////////////////////////////////////////////
var app = builder.Build();

//1.3.4 在Program.cs撰寫啟用Initializer的程式
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;

    SeedData.Initialize(service);
}

//6.1.3 修改 Program.cs中的程式碼，將是否在開發模式下的錯誤處理判斷註解掉
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //6.2.1 在Program.cs中的程式碼註冊處理HttpNotFound(404)錯誤的Error Handler
    app.UseStatusCodePagesWithReExecute("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
