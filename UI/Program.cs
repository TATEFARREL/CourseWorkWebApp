using DAL;
using BLL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;
using UI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddAutoMapper(typeof(BLL.MappingProfile));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITourRepository, TourRepository>();
builder.Services.AddScoped<IAttractionRepository, AttractionRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<ITourApplicationRepository, TourApplicationRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ITourAttractionRepository, TourAttractionRepository>();
builder.Services.AddScoped<ITourAttractionService, TourAttractionService>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IAttractionService, AttractionService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITourApplicationService, TourApplicationService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();

var app = builder.Build();
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/AdminBus/Create") &&
        context.Request.Method == "POST")
    {
       
        var form = await context.Request.ReadFormAsync();
        foreach (var key in form.Keys)
        {
            Console.WriteLine($"Form key: {key}, value: {form[key]}");
        }
    }
    await next();
});
app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated &&
        !context.Request.Path.StartsWithSegments("/Account") &&
        !context.Request.Path.StartsWithSegments("/User") &&
        !context.Request.Path.StartsWithSegments("/Home") &&
        !context.Request.Path.StartsWithSegments("/Admin") &&
        !context.Request.Path.StartsWithSegments("/AdminTour") &&
        !context.Request.Path.StartsWithSegments("/AdminBus") &&
        !context.Request.Path.StartsWithSegments("/AdminAttraction") &&
        !context.Request.Path.StartsWithSegments("/AdminCity") &&
        !context.Request.Path.StartsWithSegments("/AdminCountry") &&
        !context.Request.Path.StartsWithSegments("/AdminTourComposition") &&
        !context.Request.Path.StartsWithSegments("/AdminTourApplication"))
    {
        context.Response.Redirect("/Account/Login");
        return;
    }

    await next();
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DbInitializer.InitializeAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.Run();