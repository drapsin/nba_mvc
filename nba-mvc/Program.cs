using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using nba_mvc.Data;
using nba_mvc.Services;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();


var imageStorage = builder.Configuration["ImageStorage"];

if (imageStorage == "Cloudinary")
{
    builder.Services.AddSingleton<IImageUploader, CloudinaryImageUploader>();
    builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();
    builder.Services.AddSingleton(provider =>
    {
        var config = provider.GetRequiredService<IConfiguration>();
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );
        return new Cloudinary(account);
    });
}
else if (imageStorage == "Local")
{
    builder.Services.AddSingleton<IImageUploader, LocalImageUploader>();
}
else
{
    throw new InvalidOperationException("Invalid value for 'ImageStorage' in appsettings.json. Use 'Cloudinary' or 'Local'.");
}


var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await nba_mvc.Data.DbInitializer.SeedRoles(roleManager);
} - this will be used later*/

using (var scope = app.Services.CreateScope())
{
    // Seed roles
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await nba_mvc.Data.DbInitializer.SeedRoles(roleManager);

    // Admin user for testing
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var adminEmail = "admin@example.com";
    var adminPassword = "Admin123!";

    var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
    if (existingAdmin == null)
    {
        var newAdmin = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(newAdmin, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

await app.RunAsync();
