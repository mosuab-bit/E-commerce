using System;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DbInitializer
{
public static async Task InitDb(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedData(context, userManager, roleManager);
}



  private static async Task SeedData(StoreContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    await context.Database.MigrateAsync();

    // 🟢 إنشاء الأدوار إذا لم تكن موجودة
    var roles = new List<string> { "Admin", "Member" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 🟢 إضافة المستخدمين فقط إذا لم يكونوا موجودين
    if (!userManager.Users.Any())
    {
        var user = new User
        {
            UserName = "bob@test.com",
            Email = "bob@test.com"
        };

        var result = await userManager.CreateAsync(user, "Pa$$word");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Member");
        }

        var admin = new User
        {
            UserName = "admin@test.com",
            Email = "admin@test.com"
        };

        var adminResult = await userManager.CreateAsync(admin, "Pa$$word");
        if (adminResult.Succeeded)
        {
            await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
        }
    }

    // 🟢 التحقق من البيانات قبل إضافتها
    if (!context.Products.Any())
    {
        var products = new List<Product>
        {
            new() {
                Name = "Angular Speedster Board 2000",
                Description = "وصف المنتج...",
                Price = 20000,
                PictureUrl = "/images/products/sb-ang1.png",
                Brand = "Angular",
                Type = "Boards",
                QuantityInStock = 100
            }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}


}
