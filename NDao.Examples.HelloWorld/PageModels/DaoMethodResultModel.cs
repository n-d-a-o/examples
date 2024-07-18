using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Dtos;

namespace NDao.Examples.HelloWorld.PageModels;

[AllowAnonymous]
public class DaoMethodResultModel : PageModel
{
	private readonly ISampleDao sampleDao;

	public string TextString { get; private set; } = default!;

	public string TextDateTime { get; private set; } = default!;

	public string TextTuple { get; private set; } = default!;

	public string TextDto { get; private set; } = default!;

	public DaoMethodResultModel(ISampleDao sampleDao)
	{
		this.sampleDao = sampleDao;
	}

	public IActionResult OnGet()
	{
		return Page();
	}

	public IActionResult OnGetString()
	{
		string result = sampleDao.GetString();
		TextString = result;

		return Page();
	}

	public IActionResult OnGetDateTime()
	{
		DateTime result = sampleDao.GetDateTime();
		TextDateTime = result.ToString("yyyy-MM-dd HH:mm:ss.fff");

		return Page();
	}

	public IActionResult OnGetTuple()
	{
		(int id, string name) = sampleDao.GetTuple();
		TextTuple = $"| Id : {id} | Name : {name} |";

		return Page();
	}

	public IActionResult OnGetDto()
	{
		SampleDto result = sampleDao.GetDto();
		TextDto = $"| Id : {result.Id} | Name : {result.Name} |";

		return Page();
	}
}
