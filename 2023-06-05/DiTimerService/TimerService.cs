using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace DiTimerService;

public sealed class TimerService : IHostedService, IDisposable
{
    private Timer? _timer;
    private int _count;
    private readonly TimeSpan _period = TimeSpan.FromSeconds(10);
    private readonly ILogger<TimerService> _logger;

    public TimerService(ILogger<TimerService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, _period);
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        try
        {
            _count++;
            _logger.LogInformation("출력 횟수: {Count}", _count);
        }
        catch (System.Exception)
        {
            throw new Exception("출력 오류!");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, TimeSpan.Zero);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}