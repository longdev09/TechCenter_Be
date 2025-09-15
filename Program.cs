using Microsoft.EntityFrameworkCore;
using TechCenter.Models;

var builder = WebApplication.CreateBuilder(args);

//cấu hình Sql EF Core

builder.Services.AddDbContext<TechCenterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// thêm các services ở đây



var app = builder.Build();


Action checkDatabaseConnection = () =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TechCenterContext>();
        try
        {
            dbContext.Database.OpenConnection();
            Console.WriteLine("Đã kết nối đến database");
        }
        catch (Exception)
        {
            Console.WriteLine("Chưa kết nối đến database");
        }
    }
};


// Gọi action để kiểm tra kết nối khi ứng dụng khởi động
checkDatabaseConnection();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
