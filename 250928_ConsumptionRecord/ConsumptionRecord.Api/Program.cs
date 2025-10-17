using ConsumptionRecord.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "ConsumptionRecord API",
            Version = "v1",
            Description = "消费记录管理系统 API 接口文档",
            Contact = new OpenApiContact
            {
                Name = "技术支持",
                Email = "support@example.com", // 请替换为实际邮箱
                Url = new Uri("https://github.com/your-repo"), // 请替换为实际链接
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT"),
            },
        }
    );
});

builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
