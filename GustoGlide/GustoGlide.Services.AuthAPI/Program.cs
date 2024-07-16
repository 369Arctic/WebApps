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
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions")); // получаем доступ к настройкам, которые у нас есть в appsettings.json 

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddEntityFrameworkStores<AppDbContext>()
// по умолчанию используется <IdentityUser, IdentityRole>, но когда хотим добавить свои поля в БД, то нужно использовать <ApplicationUser, IdentityRole> 
.AddDefaultTokenProviders(); // настроили идентификацию личности 

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
app.UseAuthentication(); // аутентификация должна быть до авторизации
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();

void ApplyMigration()   // если есть новые миграции в БД, то будет обновлять БД тут, а не командой update-database
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