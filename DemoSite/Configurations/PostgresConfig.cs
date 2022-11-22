namespace DemoSite.Configurations;

public class PostgresConfig
{
    public required string Host { get; set; }
    public required string Database { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public required string IncludeErrorDetail { get; set; }

    public string ConnectionString =>
        $"Host={Host};Database={Database};Username={Username};Password={Password};Include Error Detail={IncludeErrorDetail}";
}