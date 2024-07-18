using Microsoft.AspNetCore.Authentication.Cookies;
using NDao.Daos;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		builder.Logging.ClearProviders();
		builder.Logging.AddSimpleConsole(options => options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff ");
		builder.Services.AddDefaultDaos<HelloWorldConnector>();
		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.Cookie.Name = "auth";
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
			});
		builder.Services.AddRazorPages();

		WebApplication app = builder.Build();
		app.Services.PreloadDefaultDaos<HelloWorldConnector>();
		CreateTables(app);
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

	private static void CreateTables(WebApplication app)
	{
		IInitializerDao initializerDao = app.Services.GetRequiredService<IInitializerDao>();
		initializerDao.CreateAccounts();
		initializerDao.CreateBlogs();
		initializerDao.CreateArticles();
		initializerDao.CreatePersons();

		ICrudDao<Person> personCrud = app.Services.GetRequiredService<ICrudDao<Person>>();
		List<Person> persons = personCrud.GetList();
		if (persons.Count < 1)
		{
			persons =
			[
				new Person { Id = 1, Name = "Ashitaka", Age = 25, Occupation = "Software Developer" },
				new Person { Id = 2, Name = "Beni", Age = 30, Occupation = "Chef" },
				new Person { Id = 3, Name = "Chiyo", Age = 35, Occupation = "Teacher" },
				new Person { Id = 4, Name = "Daiki", Age = 40, Occupation = "Architect" },
				new Person { Id = 5, Name = "Eri", Age = 45, Occupation = "Lawyer" },
			];

			foreach (Person person in persons)
			{
				personCrud.Insert(person);
			}
		}
	}
}
