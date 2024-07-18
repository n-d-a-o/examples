using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NDao.Examples.HelloWorld.PageModels.App;

[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class ErrorModel : PageModelBase
{
	public string? RequestId { get; set; }

	public void OnGet()
	{
		RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
	}
}
