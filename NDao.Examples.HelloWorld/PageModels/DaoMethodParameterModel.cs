using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels;

[AllowAnonymous]
public class DaoMethodParameterModel : PageModel
{
	private readonly ISampleDao sampleDao;

	[BindProperty(SupportsGet = true)]
	public FormSearchModel FormSimple { get; set; } = default!;

	[BindProperty(SupportsGet = true)]
	public FormSearchModel FormComplex { get; set; } = default!;

	public Person TableHeader { get; private set; } = default!;

	public List<Person> TableRowsSimple { get; private set; } = default!;

	public List<Person> TableRowsComplex { get; private set; } = default!;

	public DaoMethodParameterModel(ISampleDao sampleDao)
	{
		this.sampleDao = sampleDao;
	}

	public IActionResult OnGet()
	{
		TableRowsSimple = [];
		TableRowsComplex = [];

		return Page();
	}

	public IActionResult OnGetSimple()
	{
		string? name		= FormSimple.Name;
		int? age			= FormSimple.Age;
		string? occupation	= FormSimple.Occupation;

		name = name is not null ? $"%{name}%" : null;
		occupation = occupation is not null ? $"%{occupation}%" : null;

		TableRowsSimple = sampleDao.Search(name, age, occupation);
		TableRowsComplex = [];

		return Page();
	}

	public IActionResult OnGetComplex()
	{
		string? name		= FormComplex.Name;
		int? age			= FormComplex.Age;
		string? occupation	= FormComplex.Occupation;

		SampleDaoSearchByComplexParameterCondition condition = new()
		{
			Name		= name is not null ? $"%{name}%" : null,
			Age			= age,
			Occupation	= occupation is not null ? $"%{occupation}%" : null,
		};

		TableRowsSimple = [];
		TableRowsComplex = sampleDao.SearchByComplexParameter(condition);

		return Page();
	}

	public class FormSearchModel
	{
		public string? Name { get; set; }

		public int? Age { get; set; }

		public string? Occupation { get; set; }
	}
}
