using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Models.Entities;

namespace Infrastructure.Services;

public class CardsServices
{
    private readonly IDataContext _dbContext;

    public CardsServices(IDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TransactionDetails>?> GetLatestTransactionsAsync(Guid ownerId, Guid cardId)
    {
        int limit = AppSettings.ResponseOptions.Transactions.Limit;
        
        var card = await _dbContext
            .Set<CardEntity>()
            .Include(card => card.Transactions)
            .FirstOrDefaultAsync(card => card.OwnerId == ownerId && card.Id == cardId);
        
        if (card == null)
        {
            throw new Exception("Unauthorized");
        }

        var transactions = card.Transactions?
            .OrderByDescending(transaction => transaction.ProcessingDate)
            .Take(limit)
            .Adapt<IEnumerable<TransactionDetails>>();

        return transactions;
    }

    public async Task<double> GetBalanceAsync(Guid ownerId, Guid cardId)
    {
        var card = await _dbContext
            .Set<CardEntity>()
            .FirstOrDefaultAsync(card => card.Id == cardId && card.OwnerId == ownerId);
    
        if (card == null)
        {
            throw new Exception("Unauthorized");
        }

        return card.Balance;
    }

    public async Task<int> GetPointsAsync(Guid ownerId, Guid cardId)
    {
        var card = await _dbContext
            .Set<CardEntity>()
            .Include(card => card.Transactions)
            .FirstOrDefaultAsync(card => card.OwnerId == ownerId && card.Id == cardId);
        
        if (card == null)
        {
            throw new Exception("Unauthorized");
        }

        int points;

        if (card.PointsUpdatedAt.Day == DateTime.Today.Day)
        {
            points = card.Points;
        }
        else
        {
            points = PointsHelpers.Calculate(DateTime.Today);

            card.Points = points;
            card.PointsUpdatedAt = DateTime.Today;

            _dbContext.Set<CardEntity>().Update(card);
            await _dbContext.SaveChangesAsync();
        }

        return points;
    }
}