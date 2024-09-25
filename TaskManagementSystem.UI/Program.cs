using TaskManagementSystem.Application.Mappings;
using TaskManagementSystem.Infrastructure.Configurations;
using TaskManagementSystem.Infrastructure.Services;
using TaskManagementSystem.UI.Services;
using TaskManagementSystem.UI.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// configure database
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// configure identity
builder.Services.AddIdentityConfiguration();

// configure JWT Authentication 
builder.Services.AddJwtAuthentication(builder.Configuration);

// configure lifetime for services
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

// configure automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.SeedDatabase();
app.Run();
