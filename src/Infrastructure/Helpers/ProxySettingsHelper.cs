namespace Infrastructure.Helpers;

public static class ProxiesSettingsHelper
{
    public static string GetIp(string proxyString) =>
        proxyString.Split(':')[0];
    public static string GetPort(string proxyString) =>
        proxyString.Split(':')[1];
    public static string GetUserName(string proxyString) =>
        proxyString.Split(':')[2];
    public static string GetUserPassword(string proxyString) =>
        proxyString.Split(':')[3];
}