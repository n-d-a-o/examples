using Microsoft.AspNetCore.Authentication.Cookies;
using NDao.Examples.HelloWorld.Daos;

namespace NDao.Examples.HelloWorld;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Logging.ClearProviders();
		builder.Logging.AddSimpleConsole(options => options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff ");

		builder.Services.AddDefaultDaos<SqliteHelloWorldConnector>();

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.Cookie.Name = "auth";
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
			});

		builder.Services.AddRazorPages();

		WebApplication app = builder.Build();

		app.Services.PreloadDefaultDaos<SqliteHelloWorldConnector>();

		IInitializerDao initializerDao = app.Services.GetRequiredService<IInitializerDao>();
		initializerDao.CreateAccounts();
		initializerDao.CreateBlogs();
		initializerDao.CreateArticles();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
		}

		app.UseStaticFiles();

		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapRazorPages();

		app.Run();
	}
}
