using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Models.Responses;
using Web.Controllers.Base;

namespace Web.Controllers;

public class CardsController : ApiController
{
    private readonly CardsServices _cardsServices;

    public CardsController(CardsServices cardsServices)
    {
        _cardsServices = cardsServices;
    }

    [HttpGet("{cardId:guid}/transactions")]
    [Authorize]
    public async Task<ActionResult<TransactionsResponseModel>> GetLatestTransactions([FromRoute] Guid cardId)
    {
        var latestTransactions = await _cardsServices.GetLatestTransactionsAsync(GetAuthorizedUsedId(), cardId);

        return new TransactionsResponseModel
        {
            Items = latestTransactions
        };
    }
    
    [HttpGet("{cardId:guid}/balance")]
    [Authorize]
    public async Task<ActionResult<GetCardBalanceResponseModel>> GetCardBalance([FromRoute] Guid cardId)
    {
        var balance = await _cardsServices.GetBalanceAsync(GetAuthorizedUsedId(), cardId);
        var limit = AppSettings.Card.Limit;
        var available = limit - balance;
        
        return new GetCardBalanceResponseModel
        {
            Balance = balance,
            BalanceFormatted = $"${balance}",
            Available = available,
            AvailableFormated = $"${available}"
        };
    }
    
    [HttpGet("{cardId:guid}/points")]
    [Authorize]
    public async Task<ActionResult<GetPointsResponseModel>> GetPoints([FromRoute] Guid cardId)
    {
        var points = await _cardsServices.GetPointsAsync(GetAuthorizedUsedId(), cardId);
        
        return new GetPointsResponseModel
        {
            Points = points,
        };
    }
}