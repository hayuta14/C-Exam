using Exam_C_2.Context;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Sử dụng MySQL
builder.Services.AddDbContext<ApplicationContextDb>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("connectionDatabaseString"),
        new MySqlServerVersion(new Version(8, 0, 33))
    // Ghi đúng version của MySQL bạn đang dùng
    );
});

var app = builder.Build();

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
