using DemoSite.Configurations;
using DemoSite.Ports;
using DemoSite.Repositories;
using DemoSite.Services.File;
using DemoSite.Services.Post;
using DemoSite.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var databaseSetting = configuration.GetRequiredSection("Postgres").Get<PostgresConfig>();
    if (databaseSetting == null) throw new Exception("The database setting is null.");
    options.UseNpgsql(databaseSetting.ConnectionString);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileEntryRepository, FileEntryRepository>();
builder.Services.AddScoped<IFileRepository, LocalFileRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddSingleton(configuration.Get<ApplicationInfoConfig>()!);
builder.Services.AddSingleton(configuration.GetRequiredSection("DataPath").Get<DataPathOptions>()!);
builder.Services.AddSingleton<DataPathConfig>();
builder.Services.AddSingleton(configuration.GetRequiredSection("FilesHosting").Get<FilesHostingOption>()!);
builder.Services.AddSingleton<FilesHostingConfig>();

builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<FileServiceBaseDependency>();
builder.Services.AddScoped<IAvatarQueryService, AvatarQueryService>();
builder.Services.AddScoped<IAvatarStoreService, AvatarStoreService>();
builder.Services.AddScoped<ICreatePostService, CreatePostService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.AccessDeniedPath = "/api/NoAuthorization";
    options.LoginPath = "/api/NoAuthentication";
    options.Cookie.SameSite = SameSiteMode.None;
}).AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    //options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();