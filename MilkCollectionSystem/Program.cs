using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MilkCollectionSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");

    // allow anonymous only for login/register
    options.Conventions.AllowAnonymousToPage("/Identity/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Identity/Account/Register");
});

builder.Services.AddRazorPages();

var app = builder.Build();


// ================= CREATE ROLES =================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Create Admin role
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var result = await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (result.Succeeded)
            Console.WriteLine("✓ Admin role created.");
        else
            Console.WriteLine("✗ Failed to create Admin: " +
                string.Join(", ", result.Errors.Select(e => e.Description)));
    }
    else
    {
        Console.WriteLine("✓ Admin role already exists.");
    }

    // Create Clerk role
    if (!await roleManager.RoleExistsAsync("Clerk"))
    {
        var result = await roleManager.CreateAsync(new IdentityRole("Clerk"));

        if (result.Succeeded)
            Console.WriteLine("✓ Clerk role created.");
        else
            Console.WriteLine("✗ Failed to create Clerk: " +
                string.Join(", ", result.Errors.Select(e => e.Description)));
    }
    else
    {
        Console.WriteLine("✓ Clerk role already exists.");
    }
}
// ================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();