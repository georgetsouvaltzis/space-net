namespace Movies.Infrastructure.Settings;

public class ApiSettings
{
    public const string KeyName = nameof(ApiSettings);
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public string BaseUrl { get; set; }
}
