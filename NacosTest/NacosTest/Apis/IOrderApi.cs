using Refit;

namespace NacosTest.Apis;

public interface IOrderApi
{
    [Get("/order/getById")]
    Task<string> GetOrderAsync(Guid id);
}