using AutoMapper;
using ConsumptionRecord.Api.Helpers;
using ConsumptionRecord.Data.Dto.Records;
using ConsumptionRecord.Data.Entities;
using ConsumptionRecord.Domain.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsumptionRecord.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WaitController(ApplicationContext dbContext, IMapper mapper) : Controller
{
    [HttpGet(nameof(GetStat))]
    public async Task<ActionResult<ServiceResponse<WaitStatDto>>> GetStat()
    {
        try
        {
            var total = await dbContext.Waits.CountAsync();
            var finish = await dbContext.Waits.CountAsync(wait => wait.IsDone);
            return Ok(
                ServiceResponse<WaitStatDto>.ReturnResultWith200(
                    new WaitStatDto { TotalCount = total, FinishCount = finish }
                )
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<WaitStatDto>.Return500());
        }
    }

    [HttpPost(nameof(AddWait))]
    public async Task<ActionResult<ServiceResponse<WaitInfoDto>>> AddWait(WaitInfoDto waitInfoDto)
    {
        try
        {
            var wait = mapper.Map<Wait>(waitInfoDto);
            wait.CreateTime = DateTime.UtcNow;
            dbContext.Waits.Add(wait);
            if (await dbContext.SaveChangesAsync() < 1)
                throw new Exception("保存待办失败");

            return Ok(
                ServiceResponse<WaitInfoDto>.ReturnResultWith201(mapper.Map<WaitInfoDto>(wait))
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<WaitInfoDto>.Return500());
        }
    }

    [HttpGet(nameof(GetWaits))]
    public async Task<ActionResult<ServiceResponse<List<WaitInfoDto>>>> GetWaits()
    {
        try
        {
            var waitList =
                from wait in dbContext.Waits
                where wait.IsDone == false
                select new WaitInfoDto
                {
                    Id = wait.Id,
                    Title = wait.Title,
                    Content = wait.Content,
                    IsDone = wait.IsDone,
                    CreateTime = wait.CreateTime,
                };
            return Ok(
                ServiceResponse<List<WaitInfoDto>>.ReturnResultWith200(await waitList.ToListAsync())
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<List<WaitInfoDto>>.Return500());
        }
    }

    [HttpGet(nameof(GetWaitsWithFilter))]
    public async Task<ActionResult<ServiceResponse<List<WaitInfoDto>>>> GetWaitsWithFilter(
        string? queryStr,
        int? status
    )
    {
        try
        {
            var query = dbContext.Waits.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                query = query.Where(w =>
                    w.Title.Contains(queryStr) || w.Content.Contains(queryStr)
                );
            }

            if (status is > 0 and < 3)
                query = query.Where(wait => wait.IsDone == (status != 1));

            var result = await query
                .Select(wait => new WaitInfoDto
                {
                    Id = wait.Id,
                    Title = wait.Title,
                    Content = wait.Content,
                    IsDone = wait.IsDone,
                    CreateTime = wait.CreateTime,
                })
                .ToListAsync();

            return Ok(ServiceResponse<List<WaitInfoDto>>.ReturnResultWith200(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<List<WaitInfoDto>>.Return500());
        }
    }

    [HttpPut(nameof(UpdateWaitStatus))]
    public async Task<ActionResult<ServiceResponse<WaitInfoDto>>> UpdateWaitStatus(
        WaitInfoDto waitInfoDto
    )
    {
        try
        {
            var wait = await dbContext.Waits.FirstOrDefaultAsync(wait => wait.Id == waitInfoDto.Id);
            if (wait == null)
                return BadRequest(ServiceResponse<WaitInfoDto>.Return404());
            wait.Title = waitInfoDto.Title;
            wait.Content = waitInfoDto.Content;
            wait.IsDone = waitInfoDto.IsDone;
            if (await dbContext.SaveChangesAsync() < 1)
                throw new Exception("更新待办失败");

            return Ok(
                ServiceResponse<WaitInfoDto>.ReturnResultWith200(mapper.Map<WaitInfoDto>(wait))
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse<WaitInfoDto>.Return500());
        }
    }
}
