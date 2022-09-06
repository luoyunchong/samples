// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;

namespace console_di;

internal class App
{
    private readonly IFreeSql _fsql;
    private readonly ILogger<App> _logger;

    public App(IFreeSql fsql, ILogger<App> logger)
    {
        _fsql = fsql;
        _logger = logger;
    }

    public async Task RunAsync(string[] args)
    {
        bool isok = await _fsql.Ado.ExecuteConnectTestAsync();
        _logger.LogInformation("test:" + isok);
    }
}