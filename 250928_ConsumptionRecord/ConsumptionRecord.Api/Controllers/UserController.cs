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
    public async Task<ActionResult<ServiceResponse<UserInfoDto>>> Registered(UserInfoDto userInfo)
    {
        try
        {
            var user = await dbContext
                .Users.Where(user => user.Email == userInfo.Email)
                .FirstOrDefaultAsync();
            if (user is not null)
                return BadRequest(ServiceResponse<UserInfoDto>.Return409("邮箱已注册"));

            user = mapper.Map<User>(userInfo);
            user.CreatedDate = DateTime.UtcNow;
            user.IsAdmin = false;

            dbContext.Users.Add(user);
            if (await dbContext.SaveChangesAsync() > 0)
                return Ok(ServiceResponse<UserInfoDto>.ReturnResultWith201(userInfo));
            return BadRequest(ServiceResponse<UserInfoDto>.Return500());
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<UserInfoDto>.Return500());
        }
    }

    [HttpPost(nameof(Login))]
    public async Task<ActionResult<ServiceResponse<UserInfoDto>>> Login(UserInfoDto userInfo)
    {
        try
        {
            var user = await dbContext
                .Users.Where(user => user.Email == userInfo.Email)
                .FirstOrDefaultAsync();
            if (user is null)
                return NotFound(ServiceResponse<UserInfoDto>.Return404("用户不存在"));

            if (user.Password != userInfo.Password)
                return BadRequest(ServiceResponse<UserInfoDto>.Return400("密码错误"));

            return Ok(
                ServiceResponse<UserInfoDto>.ReturnResultWith200(mapper.Map<UserInfoDto>(user))
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<UserInfoDto>.Return500());
        }
    }
}
