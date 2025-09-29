using AutoMapper;
using ConsumptionRecord.Api.Helpers;
using ConsumptionRecord.Data.Dto;
using ConsumptionRecord.Data.Entities;
using ConsumptionRecord.Domain.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsumptionRecord.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ApplicationContext dbContext, IMapper mapper) : Controller
{
    [HttpPost(nameof(Registered))]
    public async Task<IActionResult> Registered(UserInfoDto userInfo)
    {
        try
        {
            var user = await dbContext
                .Users.Where(user => user.Email == userInfo.Email)
                .FirstOrDefaultAsync();
            if (user is not null)
                return BadRequest(ServiceResponse<string>.Return409("邮箱已注册"));

            user = mapper.Map<User>(userInfo);
            user.CreatedDate = DateTime.UtcNow;
            user.IsAdmin = false;

            dbContext.Users.Add(user);
            if (await dbContext.SaveChangesAsync() > 0)
                return Ok(ServiceResponse<UserInfoDto>.ReturnResultWith201(userInfo));
            return BadRequest(ServiceResponse<string>.Return500());
        }
        catch (Exception ex)
        {
            // Logger
            return BadRequest(ServiceResponse<string>.Return500());
        }
    }
}
