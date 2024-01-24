using Domain.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity.RoleManager;
using Infrastructure.Identity.UserManager;
using Microsoft.EntityFrameworkCore;

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
app.UseAuthorization();

app.MapControllers();

app.Run();