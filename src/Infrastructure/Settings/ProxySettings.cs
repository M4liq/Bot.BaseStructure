using System.Collections.Generic;

namespace Infrastructure.Settings;

public interface IProxySettings
{
    public IEnumerable<string> ProxyConnectionStrings { get; set; }
}

public class ProxySettings : IProxySettings
{
    public IEnumerable<string> ProxyConnectionStrings { get; set; }
}