using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Dtos;
using Infrastructure.Helpers;
using Infrastructure.Settings;
using PuppeteerSharp;

namespace Infrastructure.Services;

public class BrowserService : IBrowserService
{
    private IBrowser Browser;
    private string CurrentProxyString;
    private readonly IList<string> _proxyStrings;
    private readonly IChromiumSettings _chromiumSettings;
    private string Message;

    public BrowserService(IProxySettings proxySettings, IChromiumSettings chromiumSettings)
    {
        _chromiumSettings = chromiumSettings;
        _proxyStrings = proxySettings.ProxyConnectionStrings.ToList();
    }

    public async Task<ResultDto<IPage>> Launch()
    {
        SetCurrentCurrentProxyString();
        
        var ip = ProxiesSettingsHelper.GetIp(CurrentProxyString);
        var port = ProxiesSettingsHelper.GetPort(CurrentProxyString);
        var login = ProxiesSettingsHelper.GetUserName(CurrentProxyString);
        var password = ProxiesSettingsHelper.GetUserPassword(CurrentProxyString);
        var browserInitializationArguments = new [] {$"--proxy-server={ip}:{port}"};

        var browserFetcher = new BrowserFetcher(new BrowserFetcherOptions());

        await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

        Browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = _chromiumSettings.Headless,
            Args = browserInitializationArguments
        });

        var page = await Browser.NewPageAsync();

        var credentials = new Credentials()
        {
            Username = login,
            Password = password
        };
        
        await page.AuthenticateAsync(credentials);

        await page.SetViewportAsync(new ViewPortOptions
        {
            Width = _chromiumSettings.ViewPortWidth,
            Height = _chromiumSettings.ViewPortHeight
        });

        page.DefaultTimeout = _chromiumSettings.Timeout;

        return new ResultDto<IPage>
        {
            Data = page,
            IsSuccess = true
        };
    }

    public async Task<ResultDto<bool>> Close()
    {
        if (Browser is null)
        {
            return new ResultDto<bool>
            {
                IsSuccess = false,
                Message = "Could not close browser. No instance of browser has been found."
            };
        }

        await Browser.CloseAsync();
        return new ResultDto<bool>
        {
            IsSuccess = true,
            Message = "Browser successfully closed."
        };
    }

    private void SetCurrentCurrentProxyString()
    {
        if (CurrentProxyString is null or "")
        {
            CurrentProxyString = _proxyStrings.FirstOrDefault();
            return;
        }

        var currentIndex = _proxyStrings.ToList().FindIndex(r => r == CurrentProxyString);

        var lastIndex = _proxyStrings.Count;
        
        if(lastIndex <= currentIndex)
        {
            CurrentProxyString = _proxyStrings[currentIndex];
            return;
        }

        Message = "Number of executions has exceeded a number available. Going back to beginning of the list";
        CurrentProxyString = "";
    }
}