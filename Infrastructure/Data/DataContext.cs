using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext :
    IdentityDbContext<UserEntity, RoleEntity, Guid>,
    IDataContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}
