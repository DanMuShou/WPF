using AutoMapper;
using ConsumptionRecord.Api.Helpers;
using ConsumptionRecord.Data.Dto.Users;
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
    public async Task<ActionResult<ServiceResponse<UserInfoDto>>> Registered(
        UserRegisterDto userRegister
    )
    {
        try
        {
            var user = await dbContext
                .Users.Where(user => user.Account == userRegister.Account)
                .FirstOrDefaultAsync();
            if (user is not null)
                return BadRequest(ServiceResponse<UserInfoDto>.Return409("邮箱已注册"));

            user = mapper.Map<User>(userRegister);

            dbContext.Users.Add(user);
            if (await dbContext.SaveChangesAsync() < 1)
                throw new Exception("保存用户信息失败");

            var userInfoDto = mapper.Map<UserInfoDto>(user);
            userInfoDto.ResponseTime = DateTime.Now;
            return Ok(ServiceResponse<UserInfoDto>.ReturnResultWith200(userInfoDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<UserInfoDto>.Return500());
        }
    }

    [HttpPost(nameof(Login))]
    public async Task<ActionResult<ServiceResponse<UserInfoDto>>> Login(UserLoginDto userLogin)
    {
        try
        {
            var user = await dbContext
                .Users.Where(user => user.Account == userLogin.Account)
                .FirstOrDefaultAsync();
            if (user is null)
                return NotFound(ServiceResponse<UserInfoDto>.Return404("用户不存在"));

            if (user.Password != userLogin.Password)
                return BadRequest(ServiceResponse<UserInfoDto>.Return400("密码错误"));

            var userInfoDto = mapper.Map<UserInfoDto>(user);
            userInfoDto.ResponseTime = DateTime.Now;
            return Ok(ServiceResponse<UserInfoDto>.ReturnResultWith200(userInfoDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<UserInfoDto>.Return500());
        }
    }
}
