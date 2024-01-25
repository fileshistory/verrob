using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Transactions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext :
    IdentityDbContext<UserEntity, RoleEntity, Guid>,
    IDataContext
{
    public DbSet<CardEntity>? Cards { get; set; }
    public DbSet<TransactionEntity>? Transactions { get; set; }
    public DbSet<TransactionTypeEntity>? TransactionTypes { get; set; }
    public DbSet<TransactionStatusEntity>? TransactionStatuses { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}
