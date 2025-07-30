using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPITest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//設定跨域存取政策
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("MyPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GoodStoreContext>(Options =>
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionStrings")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//啟動跨域存取政策
app.UseCors("MyPolicy");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
