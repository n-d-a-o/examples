using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NDao.Examples.HelloWorld.PageModels;

[AllowAnonymous]
public class IndexModel : PageModel
{
	public IActionResult OnGet()
	{
		return Page();
	}
}
