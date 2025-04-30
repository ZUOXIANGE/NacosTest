namespace NacosTest.Dtos;

public class WeChatPayConfig
{
    /// <summary>
    /// 微信应用ID
    /// </summary>
    public string? AppId { get; set; }

    /// <summary>
    /// 商户号
    /// </summary>
    public string? MchId { get; set; }

    /// <summary>
    /// API密钥
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 支付结果通知URL
    /// </summary>
    public string? NotifyUrl { get; set; }
}