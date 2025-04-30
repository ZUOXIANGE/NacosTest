namespace NacosTest.Dtos;

/// <summary>
/// 支付配置
/// </summary>
public class PaymentConfig
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public WeChatPayConfig? WeChatPay { get; set; }

    /// <summary>
    /// 支付宝
    /// </summary>
    public AlipayConfig? Alipay { get; set; }
}