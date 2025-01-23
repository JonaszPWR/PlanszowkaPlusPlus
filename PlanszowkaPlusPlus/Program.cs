using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login";
});


var connectString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectString, ServerVersion.AutoDetect(connectString)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
    
builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if(!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Can't connect to database");
    }
}

app.UseAuthentication(); 
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.Run();
