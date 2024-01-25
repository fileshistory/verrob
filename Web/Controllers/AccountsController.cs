using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Helpers.Auth;
using Infrastructure.Identity.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Requests;
using Shared.Models.Responses;
using Web.Controllers.Base;

namespace Web.Controllers;

public class AccountsController : ApiController
{
    private readonly IBaseUserManager _userManager;
    private readonly IDataContext _dbContext;
    private readonly JwtHelpers _jwtHelpers;
    
    public AccountsController(IBaseUserManager userManager, IDataContext dbContext, JwtHelpers jwtHelpers)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _jwtHelpers = jwtHelpers;
    }

    private double HardCalculationsBasedOnDeepStatisticsAndPersonalData()
    {
        return Random.Shared.NextDouble() * 100;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequestModel request)
    {
        var user = new UserEntity
        {
            UserName = request.Email,
            Email = request.Email,
        };
        
        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return UnprocessableEntity(result.Errors);
        }
        
        var card = new CardEntity
        {
            Balance = HardCalculationsBasedOnDeepStatisticsAndPersonalData(),
            Owner = user,
        };

        await _dbContext.Set<CardEntity>().AddAsync(card);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPost("user")]
    public async Task<ActionResult<AuthResponseModel>> GetUser([FromBody] AuthRequestModel request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return UnprocessableEntity();
        }

        var passwordValidated = await _userManager.CheckPasswordAsync(user, request.Password);

        if (passwordValidated == false)
        {
            return UnprocessableEntity();
        }

        return new AuthResponseModel
        {
            Token = _jwtHelpers.SerializeToken(user.Id.ToString(), "")
        };
    }
    
    [HttpGet("{cardId:guid}/balance")]
    [Authorize]
    public async Task<ActionResult<GetCardBalanceResponseModel>> GetCardBalance([FromRoute] Guid cardId)
    {
        var authorizedUsedId = GetAuthorizedUsedId();
        
        var card = await _dbContext
            .Set<CardEntity>()
            .FirstOrDefaultAsync(card => card.Id == cardId && card.OwnerId == authorizedUsedId);
    
        if (card == null)
        {
            return BadRequest();
        }
    
        var limit = 1500;
        var available = limit - card.Balance;
        
        return new GetCardBalanceResponseModel
        {
            Balance = card.Balance,
            BalanceFormatted = $"${card.Balance}",
            Available = available,
            AvailableFormated = $"${available}"
        };
    }
}