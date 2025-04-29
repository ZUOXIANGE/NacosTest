using System.Text.Json.Serialization;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace NacosTest.Dtos;

public class AlipayConfig
{
    /// <summary>
    /// 支付宝应用ID
    /// </summary>
    [JsonPropertyName("app_id")]//这几个设置别名的特性都不能生效
    [JsonProperty("app_id")]
    [YamlMember(Alias = "app_id")]
    public string? AppId { get; set; }

    /// <summary>
    /// 商户私钥
    /// </summary>
    public string? MerchantPrivateKey { get; set; }

    /// <summary>
    /// 支付宝公钥
    /// </summary>
    public string? AlipayPublicKey { get; set; }

    /// <summary>
    /// 支付结果通知URL
    /// </summary>
    public string? NotifyUrl { get; set; }

    /// <summary>
    /// 页面跳转同步通知页面路径
    /// </summary>
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// 是否沙箱环境
    /// </summary>
    public bool Sandbox { get; set; }
}