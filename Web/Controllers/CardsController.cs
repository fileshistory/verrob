using Domain.Entities;
using Infrastructure.Data;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Models.Entities;
using Shared.Models.Responses;
using Web.Controllers.Base;

namespace Web.Controllers;

public class CardsController : ApiController
{
    private readonly IDataContext _dbContext;

    public CardsController(IDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{cardId:guid}/transactions")]
    public async Task<ActionResult<TransactionsResponseModel>> GetLatestTransactions([FromRoute] Guid cardId)
    {
        Guid userId = GetAuthorizedUsedId();

        int limit = AppSettings.ResponseOptions.Transactions.Limit;
        
        var card = await _dbContext
            .Set<CardEntity>()
            .Include(card => card.Transactions)
            .FirstOrDefaultAsync(card => card.OwnerId == userId && card.Id == cardId);

        if (card == null)
        {
            return BadRequest();
        }

        var latestTransactions = card.Transactions?
            .OrderByDescending(transaction => transaction.ProcessingDate)
            .Take(limit)
            .Adapt<IEnumerable<TransactionDetails>>();

        return new TransactionsResponseModel
        {
            Items = latestTransactions
        };
    }
}