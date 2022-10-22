namespace Infrastructure.Settings;

public interface IChromiumSettings
{
    public bool Headless { get; set; }
    public int ViewPortWidth { get; set; }
    public int ViewPortHeight { get; set; }
    public int Timeout { get; set; }
}

public class ChromiumSettings : IChromiumSettings
{
    public bool Headless { get; set; }
    public int ViewPortWidth { get; set; }
    public int ViewPortHeight { get; set; }
    public int Timeout { get; set; }
}