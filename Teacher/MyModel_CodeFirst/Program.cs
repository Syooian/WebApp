using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//1.2.4 在Program.cs內以依賴注入的寫法註冊讀取連線字串的服務(food panda、Uber Eats)
//※注意程式的位置必須要在var builder = WebApplication.CreateBuilder(args);這句之後且在var app = builder.Build();之前
builder.Services.AddDbContext<GuestBookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GuestBookConnection")));


/////////////////////////////////////////////////////////////////////
var app = builder.Build();

//1.3.4 在Program.cs撰寫啟用Initializer的程式
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;

    SeedData.Initialize(service);
}


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
