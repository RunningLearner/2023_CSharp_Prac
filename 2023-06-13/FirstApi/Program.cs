using FirstApi.Data;
using FirstApi.Services;
using Microsoft.EntityFrameworkCore;

namespace FirstApi;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<PizzaService>();

        // DbContext SQLite 추가
        builder.Services.AddDbContext<PizzaDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        EnsureDatabaseUpdated(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void EnsureDatabaseUpdated(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
        // 애플리케이션 시작 시 데이터베이스에 대해 보류 중인 마이그레이션을 적용
        // 앱을 시작할 때마다 실행되므로, 이를 프로덕션 환경에서 사용할 때는 주의
        context.Database.Migrate();
    }
}