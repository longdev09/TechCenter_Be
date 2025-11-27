using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using TechCenter.Middleware;
using TechCenter.Models;
using TechCenter.Services;
using TechCenter.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

//cấu hình Sql EF Core

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// thêm các services ở đây
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
builder.Services.AddScoped<IVaiTroService, VaiTroService>();
builder.Services.AddScoped<IHocVienService, HocVienService>();
builder.Services.AddScoped<IKhoaHocService, KhoaHocService>();
builder.Services.AddScoped<ILopHocService, LopHocService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IThanhToanService, ThanhToanService>();
builder.Services.AddScoped<IDangKyHocService, DangKyHocService>();
builder.Services.AddScoped<ILichHocService, LichHocService>();
builder.Services.AddScoped<ITaiLieuService, TaiLieuService>();
builder.Services.AddScoped<IUploadAnhService, UploadAnhService>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddScoped<IBaiThiService, BaiThiService>();
builder.Services.AddScoped<IGiaoVienService, GiaoVienService>();



// Cho phép CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // FE Vite
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});


// Add Cloudinary config
builder.Services.AddSingleton(new Cloudinary(new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
)));



var app = builder.Build();



//kiểm tra kết nối
Action checkDatabaseConnection = () =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
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

app.UseCors("AllowFrontend");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
