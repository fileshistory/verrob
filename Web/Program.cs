using System.Text;
using Domain.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Helpers.Auth;
using Infrastructure.Identity.RoleManager;
using Infrastructure.Identity.UserManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<IDataContext, DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services
    .AddIdentity<UserEntity, RoleEntity>()
    .AddEntityFrameworkStores<DataContext>()
    .AddRoleManager<BaseRoleManager>()
    .AddUserManager<BaseUserManager>();

builder.Services.AddScoped<IBaseRoleManager, BaseRoleManager>();
builder.Services.AddScoped<IBaseUserManager, BaseUserManager>();

string? secretString = builder.Configuration["Authentication:Jwt:Secret"];
byte[] secret = Encoding.ASCII.GetBytes(secretString);
var signingKey = new SymmetricSecurityKey(secret);

builder.Services.AddTransient(_ => signingKey);

builder.Services.AddTransient<JwtHelpers>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = signingKey,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();