namespace NDao.Examples.HelloWorld;

public class HelloWorldConnector : DaoConnector
{
	private readonly IConfiguration configuration;

	private readonly ILogger<HelloWorldConnector> logger;

	public HelloWorldConnector(IConfiguration configuration, ILogger<HelloWorldConnector> logger)
	{
		this.configuration = configuration;
		this.logger = logger;
	}

	public override void OnConfiguring(DaoConnectorSettings settings)
	{
		string connectionString = configuration.GetConnectionString("SqliteHelloWorld")
			?? throw new InvalidOperationException();
		settings.UseSqlite(connectionString);

		settings.LogWriter = message => logger.LogInformation("{Message}", message);
		settings.IsManuallyExecutableSqlLogging = true;
	}
}
