namespace ImageProService.Infrastructure.Configurations;

public class BlobSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = "profile-pics";
    public string CdnEndpoint { get; set; } = string.Empty;
}