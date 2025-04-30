using System.Text.Json;
using AutoCtor;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers;

[ApiController]
[Route("order")]
[AutoConstruct]
public partial class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    [HttpGet("getById")]
    public async Task<string> GetOrderAsync(Guid id)
    {
        _logger.LogInformation("获取订单信息");
        await Task.Delay(Random.Shared.Next(100));
        return JsonSerializer.Serialize(new
        {
            Id = id,
            Name = "测试",
            Time = DateTime.Now,
        });
    }
}