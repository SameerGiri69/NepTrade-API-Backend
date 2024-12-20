using Finshark_API.Data;
using Finshark_API.Interfaces;
using Finshark_API.Models;
using Finshark_API.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.WithOrigins("https://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Cookie";
    config.LoginPath = "/api/account/login";
});
builder.Services.AddScoped<IStockInterface, StockRepository>(); 
builder.Services.AddScoped<ICommentInterface, CommentRepository>();
builder.Services.AddScoped<ITokenInterface, TokenRepository>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IFMPService, IFMPRepository>();
builder.Services.AddHttpClient<IFMPService, IFMPRepository>();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
});


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
.AllowAnyMethod()
.AllowCredentials()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true));

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
