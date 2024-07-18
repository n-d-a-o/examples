using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NDao.Daos;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels;

[AllowAnonymous]
public class TransactionModel : PageModel
{
	private const string xxx = "XXX";

	private readonly ISampleDao sampleDao;

	private readonly ICrudDao<Person> personCrud;

	private readonly DaoContext<HelloWorldConnector> context;

	public Person TableHeader { get; private set; } = default!;

	public List<Person> TableRows { get; private set; } = default!;

	public string TextCommit
	{
		get => (TempData[nameof(TextCommit)] as string) ?? string.Empty;
		private set => TempData[nameof(TextCommit)] = value;
	}

	public string TextRollback
	{
		get => (TempData[nameof(TextRollback)] as string) ?? string.Empty;
		private set => TempData[nameof(TextRollback)] = value;
	}

	public TransactionModel(ISampleDao sampleDao, ICrudDao<Person> personCrud,
		DaoContext<HelloWorldConnector> context)
	{
		this.sampleDao = sampleDao;
		this.personCrud = personCrud;
		this.context = context;
	}

	public IActionResult OnGet()
	{
		TableRows = personCrud.GetList();

		return Page();
	}

	public IActionResult OnPostReset()
	{
		sampleDao.Delete(xxx);

		return RedirectToPage();
	}

	public IActionResult OnPostCommit()
	{
		Console.WriteLine("Åö START OnPostCommit");
		StringBuilder log = new();

		using (DaoTransaction transaction = context.BeginTransaction())
		{
			log.AppendLine("BEGIN");

			InsertPerson(transaction, log);
			transaction.Commit();

			log.AppendLine("COMMIT");
		}

		TextCommit = log.ToString();
		Console.WriteLine("Åö END OnPostCommit");

		return RedirectToPage();
	}

	public IActionResult OnPostRollback()
	{
		Console.WriteLine("Åö START OnPostRollback");
		StringBuilder log = new();

		try
		{
			using (DaoTransaction transaction = context.BeginTransaction())
			{
				log.AppendLine("BEGIN");

				InsertPerson(transaction, log);
				int.Parse("X");
				transaction.Commit();

				log.AppendLine("COMMIT");
			}
		}
		catch (FormatException exception)
		{
			string message = $"{nameof(FormatException)} : {exception.Message}";
			log.AppendLine(message);
			Console.WriteLine(message);
		}

		TextRollback = log.ToString();
		Console.WriteLine("Åö END OnPostRollback");

		return RedirectToPage();
	}

	private void InsertPerson(DaoTransaction transaction, StringBuilder log)
	{
		List<Person> persons = personCrud.GetList(transaction);
		string get1 = $"GET LIST | {persons.Count} rows";
		log.AppendLine(get1);
		Console.WriteLine(get1);

		int id = persons.Count + 1;
		Person person = new()
		{
			Id = id,
			Name = $"Test{id}",
			Age = 20,
			Occupation = xxx,
		};
		personCrud.Insert(person, transaction);
		log.AppendLine("INSERT");

		persons = personCrud.GetList(transaction);
		string get2 = $"GET LIST | {persons.Count} rows";
		log.AppendLine(get2);
		Console.WriteLine(get2);
	}
}
