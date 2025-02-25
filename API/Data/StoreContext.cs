
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Basket> Baskets {get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
        .HasData(
            new IdentityRole {Id="28d29ad0-5271-46a1-8a4b-325d2bcec816" ,Name="Member",NormalizedName="MEMBER"},
            new IdentityRole {Id ="2ce758b4-8fdc-441c-86d6-6bd2ca9adb0a", Name="Admin",NormalizedName="ADMIN"}
        );
    }
}
