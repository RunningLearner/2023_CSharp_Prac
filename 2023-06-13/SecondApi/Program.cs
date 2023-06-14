using Microsoft.EntityFrameworkCore;
using SecondApi.Data;
using SecondApi.Services;
using Serilog;

namespace SecondApi;

internal static class Program
{
    private static void Main(string[] args)
    {
        // Serilog 구성
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders(); // 기존 로그 제공자 제거
        builder.Logging.AddSerilog(); // Serilog 로그 제공자 추가
        builder.Services.AddControllers();
        builder.Services.AddDbContext<TodoDbContext>(opt =>
            opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<TodoItemService>();

        var app = builder.Build();

        EnsureDatabaseUpdated(app);

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
        var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        // 애플리케이션 시작 시 데이터베이스에 대해 보류 중인 마이그레이션을 적용
        // 앱을 시작할 때마다 실행되므로, 이를 프로덕션 환경에서 사용할 때는 주의
        context.Database.Migrate();
    }
}