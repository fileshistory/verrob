using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity.UserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests.Accounts;
using Web.Controllers.Base;

namespace Web.Controllers;

public class AccountsController : ApiController
{
    private readonly IBaseUserManager _userManager;
    private readonly DataContext _dbContext;
    
    public AccountsController(IBaseUserManager userManager, DataContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    private double HardCalculationsBasedOnDeepStatisticsAndPersonalData()
    {
        return Random.Shared.NextDouble() * 100;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateAccountRequestModel request)
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
}