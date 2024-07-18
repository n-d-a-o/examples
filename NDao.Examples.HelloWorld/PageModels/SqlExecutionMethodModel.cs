using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NDao.Daos;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;
using NDao.Exceptions;

namespace NDao.Examples.HelloWorld.PageModels;

[AllowAnonymous]
public class SqlExecutionMethodModel : PageModel
{
	private readonly ISampleDao sampleDao;

	private readonly ICrudDao<Person> personCrud;

	[BindProperty(SupportsGet = true)]
	public FormSingleModel FormSingle { get; set; } = default!;

	[BindProperty(SupportsGet = true)]
	public FormSingleOrDefaultModel FormSingleOrDefault { get; set; } = default!;

	public Person TableHeader { get; private set; } = default!;

	public List<Person> TableRows { get; private set; } = default!;

	public string Textffect
	{
		get => (TempData[nameof(Textffect)] as string) ?? string.Empty;
		private set => TempData[nameof(Textffect)] = value;
	}

	public string TextSingle { get; private set; } = default!;

	public string TextSingleOrDefault { get; private set; } = default!;

	public string TextQuery { get; private set; } = default!;

	public SqlExecutionMethodModel(ISampleDao sampleDao, ICrudDao<Person> personCrud)
	{
		this.sampleDao = sampleDao;
		this.personCrud = personCrud;
	}

	public IActionResult OnGet()
	{
		TableRows = personCrud.GetList();

		return Page();
	}

	public IActionResult OnPostAffect()
	{
		int result = sampleDao.Affect();
		Textffect = $"Affected : {result}";

		return RedirectToPage();
	}

	public IActionResult OnGetSingle()
	{
		TableRows = personCrud.GetList();

		try
		{
			string result = sampleDao.Single(FormSingle.Id);
			TextSingle = $"Name : {result}";
		}
		catch (InvalidSqlResultException exception)
		{
			TextSingle = $"{nameof(InvalidSqlResultException)} : {exception.Message}";
		}

		return Page();
	}

	public IActionResult OnGetSingleOrDefault()
	{
		TableRows = personCrud.GetList();

		string? result = sampleDao.SingleOrDefault(FormSingleOrDefault.Id);
		TextSingleOrDefault = $"Name : {result ?? "null"}";

		return Page();
	}

	public IActionResult OnGetQuery()
	{
		TableRows = personCrud.GetList();

		List<string> result = sampleDao.Query();
		TextQuery = $"Names : {string.Join(" , ", result)}";;

		return Page();
	}

	public class FormSingleModel
	{
		public int Id { get; set; }
	}

	public class FormSingleOrDefaultModel
	{
		public int Id { get; set; }
	}
}
