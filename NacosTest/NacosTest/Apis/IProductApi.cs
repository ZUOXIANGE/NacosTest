using Refit;

namespace NacosTest.Apis;

public interface IProductApi
{
    [Post("/product/add")]
    Task<Guid> AddProductAsync([Body] ProductDto req);
}

public class ProductDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }
}