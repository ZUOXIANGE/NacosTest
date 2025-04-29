using System.ComponentModel.DataAnnotations;
using AutoCtor;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;

[ApiController]
[Route("product")]
[AutoConstruct]
public partial class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    [HttpPost("add")]
    public async Task<Guid> AddProductAsync([FromBody] ProductDto req)
    {
        _logger.LogInformation("添加商品中");
        await Task.Delay(Random.Shared.Next(100));
        return Guid.NewGuid();
    }

}

public class ProductDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(64)]
    public string? Description { get; set; }
}