using INacosNamingService = Nacos.V2.INacosNamingService;
using NacosConstants = Nacos.V2.Common.Constants;

namespace NacosExtensions;

public class NacosDiscoveryHttpClientHandler : HttpClientHandler
{
    private readonly INacosNamingService _namingService;
    private readonly string _groupName;
    private readonly string _cluster;

    public NacosDiscoveryHttpClientHandler(
        INacosNamingService namingService,
        string group = "",
        string cluster = "")
    {
        _namingService = namingService;
        _groupName = string.IsNullOrEmpty(group) ? NacosConstants.DEFAULT_GROUP : group;
        _cluster = string.IsNullOrEmpty(cluster) ? NacosConstants.DEFAULT_CLUSTER_NAME : cluster;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.RequestUri = await LookupServiceAsync(request.RequestUri);

        var res = await base.SendAsync(request, cancellationToken);
        return res;
    }

    private const string Http = "http://";
    private const string Https = "https://";
    private const string Secure = "secure";

    internal async Task<Uri> LookupServiceAsync(Uri? reqUri)
    {
        //因为uri.Host是小写,所以服务名必须注册为小写
        var serviceName = reqUri?.Host;
        if (string.IsNullOrEmpty(serviceName))
        {
            throw new ArgumentException("serviceName is empty");
        }

        var instance = await _namingService
            .SelectOneHealthyInstance(serviceName, _groupName, [_cluster], true);
        if (instance == null) throw new ArgumentException("no health instance");

        var host = $"{instance.Ip}:{instance.Port}";

        //如果元数据包含secure项将使用https
        var baseUrl = instance.Metadata.TryGetValue(Secure, out _)
            ? $"{Https}{host}"
            : $"{Http}{host}";

        var uriBase = new Uri(baseUrl);
        return new Uri(uriBase, reqUri?.PathAndQuery);
    }

}