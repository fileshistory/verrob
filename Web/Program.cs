using Domain.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Helpers.Auth;
using Infrastructure.Identity.RoleManager;
using Infrastructure.Identity.UserManager;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Web.Extensions;
using Web.Middlewares.ErrorsHandler;

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

builder.AddAuth();

builder.Services.AddScoped<CardsServices>();
builder.Services.AddTransient<JwtHelpers>();

builder.Services.AddControllers();

builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorsHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();