using API.Data;
using API.Entities;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));  
});

builder.Services.AddCors();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddIdentityApiEndpoints<User>(opt=>{
    opt.User.RequireUniqueEmail=true;
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<StoreContext>();
var app = builder.Build();
await DbInitializer.InitDb(app);
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(opt=>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:3000","https://localhost:3001");
});
// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGroup("api").MapIdentityApi<User>();

app.Run();
