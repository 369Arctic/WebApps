using GustoGlide.Services.AuthAPI.Data;
using GustoGlide.Services.AuthAPI.Models;
using GustoGlide.Services.AuthAPI.Service;
using GustoGlide.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions")); // �������� ������ � ����������, ������� � ��� ���� � appsettings.json 

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddEntityFrameworkStores<AppDbContext>()
// �� ��������� ������������ <IdentityUser, IdentityRole>, �� ����� ����� �������� ���� ���� � ��, �� ����� ������������ <ApplicationUser, IdentityRole> 
.AddDefaultTokenProviders(); // ��������� ������������� �������� 

builder.Services.AddControllers();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // �������������� ������ ���� �� �����������
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();

void ApplyMigration()   // ���� ���� ����� �������� � ��, �� ����� ��������� �� ���, � �� �������� update-database
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}