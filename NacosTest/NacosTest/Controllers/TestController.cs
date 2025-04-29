using AutoCtor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nacos.V2;
using NacosTest.Apis;
using NacosTest.Dtos;

namespace NacosTest.Controllers;

[ApiController]
[Route("[controller]")]
[AutoConstruct]
public partial class TestController : ControllerBase
{
    private readonly INacosNamingService _namingService;
    private readonly ILogger<TestController> _logger;
    private readonly IOrderApi _orderApi;
    private readonly IProductApi _productApi;
    private readonly IConfiguration _configuration;
    private readonly IOptionsMonitor<PaymentConfig> _paymentConfig;

    [HttpGet("ping")]
    public string Ping()
    {
        var summaries = new[] { "Freezing", "Cool", "Mild", "Warm", "Balmy", "Hot", "Scorching" };
        return summaries[Random.Shared.Next(summaries.Length)];
    }

    [HttpGet("getInstances")]
    public async Task GetAsync()
    {
        var all = await _namingService.GetAllInstances("product-api");
        _logger.LogInformation("GetAllInstances:{@all}", all);
        var instance = await _namingService.SelectOneHealthyInstance("order-api");
        _logger.LogInformation("SelectOneHealthyInstance:{@instance}", instance);
    }

    /// <summary>
    /// 测试Refit通过服务发现调用
    /// </summary>
    /// <returns></returns>
    [HttpGet("refit")]
    public async Task RefitAsync()
    {
        var order = await _orderApi.GetOrderAsync(Guid.NewGuid());
        _logger.LogInformation("订单信息:{order}", order);
        var id = await _productApi.AddProductAsync(new ProductDto
        {
            Name = "测试名称",
            Description = "商品描述123"
        });
        _logger.LogInformation("商品:{id}", id);
    }

    /// <summary>
    /// 测试配置,json和yaml
    /// </summary>
    /// <returns></returns>
    [HttpGet("config")]
    public async Task ConfigAsync()
    {
        var connectionString = _configuration.GetConnectionString("MySql");
        _logger.LogInformation("Mysql:{connectionString}", connectionString);
        await Task.Delay(1);

        _logger.LogInformation("支付配置:{@config}", _paymentConfig.CurrentValue);
    }
}
