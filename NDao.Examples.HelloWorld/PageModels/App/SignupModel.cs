using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDao.Daos;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.App;

[AllowAnonymous]
public class SignupModel : PageModelBase
{
	private readonly IAccountDao accountDao;

	private readonly ICrudDao<Account> accountCrud;

	[BindProperty]
	public SignupFormModel SignupForm { get; set; } = default!;

	public SignupModel(IAccountDao accountDao, ICrudDao<Account> accountCrud)
	{
		this.accountDao = accountDao;
		this.accountCrud = accountCrud;
	}

	public IActionResult OnGet()
	{
		SignupForm = new SignupFormModel();

		return Page();
	}

	public async Task<IActionResult> OnPostSignupAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		Account? used = accountDao.GetAccountByName(SignupForm.Name);
		if (used is not null)
		{
			ModelState.AddModelError(string.Empty, "The name is already in use.");

			return Page();
		}

		Account account = new()
		{
			Name = SignupForm.Name,
		};
		account = accountCrud.Insert(account);

		await IssueTicketAsync(account.AccountId);

		return RedirectToPage(Routes.App.Settings.Home);
	}

	public class SignupFormModel
	{
		public string Name { get; set; } = default!;
	}
}
