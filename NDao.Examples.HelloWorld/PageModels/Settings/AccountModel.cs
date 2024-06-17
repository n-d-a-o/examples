using Microsoft.AspNetCore.Mvc;
using NDao.Daos;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.Settings;

public class AccountModel : PageModelBase
{
	private readonly IAccountDao accountDao;

	private readonly ICrudDao<Account> accountCrud;

	[BindProperty]
	public AccountFormModel AccountForm { get; set; } = default!;

	public AccountModel(IAccountDao accountDao, ICrudDao<Account> accountCrud)
	{
		this.accountDao = accountDao;
		this.accountCrud = accountCrud;
	}

	public IActionResult OnGet()
	{
		AccountForm = new AccountFormModel
		{
			Name = Account.Name,
		};

		return Page();
	}

	public IActionResult OnPostUpdate()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		Account? used = accountDao.GetAccountByName(AccountForm.Name);
		if (used is not null)
		{
			ModelState.AddModelError(string.Empty, "The name is already in use.");

			return Page();
		}

		Account account = Account;
		account.Name = AccountForm.Name;
		accountCrud.Update(account);

		return RedirectToPage(Paths.Pages.Settings.Home);
	}

	public class AccountFormModel
	{
		public string Name { get; set; } = default!;
	}
}
