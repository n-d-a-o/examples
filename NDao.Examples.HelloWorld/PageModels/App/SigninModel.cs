using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.App;

[AllowAnonymous]
public class SigninModel : PageModelBase
{
	private readonly IAccountDao accountDao;

	[BindProperty]
	public SigninFormModel SigninForm { get; set; } = default!;

	public SigninModel(IAccountDao accountDao)
	{
		this.accountDao = accountDao;
	}

	public IActionResult OnGet(string? to)
	{
		SigninForm = new SigninFormModel
		{
			To = to
		};

		return Page();
	}

	public async Task<IActionResult> OnPostSigninAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		Account? account = accountDao.GetAccountByName(SigninForm.Name);
		if (account is null)
		{
			ModelState.AddModelError(string.Empty, "Sign-in failed.");

			return Page();
		}

		await IssueTicketAsync(account.AccountId);

		if (Url.IsLocalUrl(SigninForm.To))
		{
			return LocalRedirect(SigninForm.To);
		}

		return RedirectToPage(Routes.App.Settings.Home);
	}

	public class SigninFormModel
	{
		public string Name { get; set; } = default!;

		public string? To { get; set; }
	}
}
