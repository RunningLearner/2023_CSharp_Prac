using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph.Models;
using Microsoft.Graph;
using Microsoft.Extensions.Logging;

namespace GraphApi;

public sealed class GraphService : IHostedService, IDisposable
{
    private readonly GraphServiceClient _graphClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<GraphService> _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    public GraphService(
        ILogger<GraphService> logger,
        GraphServiceClient graphClient,
        IMemoryCache cache,
        IHostApplicationLifetime appLifetime)
    {
        _graphClient = graphClient;
        _cache = cache;
        _logger = logger;
        _appLifetime = appLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var users = await GetUsers();
        PrintUsers(users);

        var groups = await GetGroups();
        PrintGroups(groups);

        await GetUserByUpn();

        _appLifetime.StopApplication();
    }

    private async Task<List<User>> GetUsers()
    {
        var usersResponse = await _graphClient.Users.GetAsync(requestConfiguration =>
        {
            requestConfiguration.QueryParameters.Select = new[] { "id", "displayName", "userPrincipalName", "mail" };
        }) ?? throw new ArgumentNullException("usersResponse", "No users were found.");

        var userList = new List<User>();
        // 모든 유저를 가져오는 이터레이터를 만든 후 평가
        var pageIterator = PageIterator<User, UserCollectionResponse>
            .CreatePageIterator(_graphClient, usersResponse, (user) =>
            {
                var cacheKey = user.UserPrincipalName ?? string.Empty;
                var cacheValue = user;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10)); // 10분 동안 유지
                _cache.Set(cacheKey, cacheValue, cacheEntryOptions);

                userList.Add(user);
                return true;
            });
        await pageIterator.IterateAsync();
        return userList;
    }

    private void PrintUsers(List<User> userList)
    {
        foreach (var user in userList)
        {
            var userData = new
            {
                user.Id,
                user.DisplayName,
                user.UserPrincipalName,
                user.Mail
            };
            _logger.LogInformation("User data : {@UserData}", userData);
        }
    }

    private async Task<List<Group>> GetGroups()
    {

        var groupsResponse = await _graphClient.Groups.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Select = new[] { "id", "displayName", "mail" };
                }) ?? throw new ArgumentNullException("groupsResponse", "No groups were found.");

        var groupList = new List<Group>();
        // 모든 유저를 가져오는 이터레이터를 만든 후 평가
        var pageIterator = PageIterator<Group, GroupCollectionResponse>
            .CreatePageIterator(_graphClient, groupsResponse, (group) =>
            {
                groupList.Add(group);
                return true;
            });
        await pageIterator.IterateAsync();

        return groupList;
    }

    private void PrintGroups(List<Group> groups)
    {
        if (groups is null)
        {
            throw new ArgumentNullException(nameof(groups));
        }

        foreach (var group in groups)
        {
            var groupData = new { group.Id, group.DisplayName, group.Mail };
            _logger.LogInformation("Group Data: {@GroupData}", groupData);
        }
    }

    private async Task GetUserByUpn()
    {
        Console.WriteLine("검색하실 UPN을 입력해주세요.");
        var userPrincipalName = Console.ReadLine()?.Trim() ?? string.Empty;

        if (_cache.TryGetValue(userPrincipalName, out User? user))
        {
            var userData = new
            {
                user?.Id,
                user?.DisplayName,
                user?.UserPrincipalName,
                user?.Mail
            };

            _logger.LogInformation("User data : {@UserData}", userData);
        }
        else
        {
            _logger.LogInformation("캐시 내 User UPN '{UserPrincipalName}' 부재", userPrincipalName);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("GraphService 종료");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _cache.Dispose();
        _graphClient.Dispose();
        _logger.LogInformation("GraphService 해제");
    }
}
