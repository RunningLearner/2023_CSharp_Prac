using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Graph;
using Azure.Core;

namespace GraphApi;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // application 실행
        host.Run();
    }

    static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;

                var clientId = GetValueOrThrowIfNullOrEmpty(configuration, "AzureAd:ClientId", "appsettings.json에 ClientId가 비어있습니다.");
                var tenantId = GetValueOrThrowIfNullOrEmpty(configuration, "AzureAd:TenantId", "appsettings.json에 TenantId가 비어있습니다.");
                var certFile = GetValueOrThrowIfNullOrEmpty(configuration, "AzureAd:CertFile", "appsettings.json에 CertFile이 비어있습니다.");

                CheckCertfileExists(certFile);

                // 인증서 패스워드 없음
                var clientCertificate = new X509Certificate2(certFile, string.Empty, X509KeyStorageFlags.MachineKeySet);
                var clientCertCredential = new ClientCertificateCredential(tenantId, clientId, clientCertificate);

                PrintTokenInfo(clientCertCredential);

                // Graph API 등록
                var graphServiceClient = new GraphServiceClient(clientCertCredential);
                services.AddSingleton(graphServiceClient);

                // cache 설정
                services.AddMemoryCache();

                services.AddHostedService<GraphService>();
            })
            .UseSerilog((hostContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
            });
    }

    private static string GetValueOrThrowIfNullOrEmpty(IConfiguration configuration, string key, string errorMessage)
    {
        var value = configuration.GetValue<string>(key) ?? string.Empty;

        if (string.IsNullOrEmpty(value))
        {
            Log.Error("{key} 부재", key);
            throw new ArgumentException(errorMessage, key);
        }

        return value;
    }

    private static void CheckCertfileExists(string certFile)
    {
        if (!File.Exists(certFile))
        {
            Log.Error("Certfile 파일 부재");
            throw new FileNotFoundException("인증서가 존재하지 않습니다.", nameof(certFile));
        }
    }

    private static void PrintTokenInfo(ClientCertificateCredential clientCertCredential)
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };
        var tokenSource = new CancellationTokenSource();
        var cancelToken = tokenSource.Token;

        try
        {
            // Get the access token
            var token = Task.Run(async () =>
            {
                var result = await clientCertCredential.GetTokenAsync(new TokenRequestContext(scopes));
                return result;
            }).GetAwaiter().GetResult();

            Log.Information("Access Token: {Token}", token.Token);
            Log.Information("Expires On: {ExpiresOn}", token.ExpiresOn);
        }
        catch (Exception ex)
        {
            Log.Error("Error: {Exception}", ex);
            return;
        }
        finally
        {
            tokenSource.Dispose();
        }
    }
}
