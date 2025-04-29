using System.Net.Http.Headers;
using Nacos.AspNetCore.V2;
using Nacos.YamlParser;
using NacosExtensions;
using NacosTest.Apis;
using NacosTest.Dtos;

Console.Title = "Test";
var builder = WebApplication.CreateBuilder(args);

//Nacos配置
builder.Host.UseNacosConfig("NacosConfig", YamlConfigurationStringParser.Instance);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PaymentConfig>(builder.Configuration.GetSection("Payment"));

//注册Nacos服务
builder.Services.AddNacosAspNet(builder.Configuration, "Nacos");

//注册Refit客户端调用,通过Nacos动态发现服务
builder.Services.AddNacosDiscoveryTypedClient<IOrderApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("http://order-api");
        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });

builder.Services.AddNacosDiscoveryTypedClient<IProductApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("http://product-api");
        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
