using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using DotNetEd.CoreAdmin;
using System.Security.Claims;
using PlanszowkaPlusPlus.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyCookieAuth";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
builder.Services.AddScoped<IPasswordHasher<Member>, PasswordHasher<Member>>();
builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();
builder.Services.AddScoped<ReservationReminderService>();

builder.Services.AddCoreAdmin(restrictToRoles: new[] { "Admin" });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!dbContext.Database.CanConnect())
    {
        throw new Exception("Database connection failed.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCoreAdminCustomAuth(async serviceProvider =>
{
    var context = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
    var result = await context.AuthenticateAsync("MyCookieAuth");
    return result.Succeeded && result.Principal.HasClaim(ClaimTypes.Role, "Admin");
});

app.MapRazorPages();
app.MapControllers();

app.Run();