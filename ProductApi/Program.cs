using Nacos.AspNetCore.V2;

Console.Title = "Product";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNacosAspNet(x =>
{
    x.ServerAddresses = ["http://localhost:8848/"];
    x.Namespace = "dev";
    x.UserName = "nacos";
    x.Password = "nacos";
    x.ServiceName = "product-api";

    //本地调试时指定IP和端口
    x.Ip = "127.0.0.1";
    x.Port = 5066;
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
