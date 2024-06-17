using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NDao.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels;

public class PageModelBase : PageModel
{
	private const string SigninPath = "/signin";

	public bool HasAccount => Authentication.Account is not null;

	public Account Account => Authentication.Account ?? throw new InvalidOperationException();

	public string To
	{
		get
		{
			string to = HttpContext.Request.Query[QueryStrings.To].ToString();
			to = string.IsNullOrEmpty(to) ? HttpContext.Request.Path.ToString() : to;

			return to;
		}
	}

	private AuthenticationFeature Authentication => HttpContext.Features.Get<AuthenticationFeature>()
		?? throw new InvalidOperationException();

	public override async Task OnPageHandlerExecutionAsync(
		PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
	{
		// Set feature

		Account? account = null;
		string? accountId = User.Claims.FirstOrDefault(claim => claim.Type == Claims.AccountId)?.Value;
		if (accountId is not null)
		{
			int parsed = int.Parse(accountId);
			ICrudDao<Account> accountCrud = GetService<ICrudDao<Account>>();
			account = accountCrud.Get(parsed);
		}
		AuthenticationFeature feature = new()
		{
			Account = account
		};
		context.HttpContext.Features.Set(feature);

		// Authenticate

		if (account is not null)
		{
			await next();

			return;
		}

		List<Type> attributeTypes = context.ActionDescriptor.EndpointMetadata
			.Select(metadata => metadata.GetType())
			.ToList();

		bool isAllowed = attributeTypes.Any(attributeType => attributeType == typeof(AllowAnonymousAttribute));
		if (isAllowed)
		{
			await next();

			return;
		}

		await context.HttpContext.SignOutAsync(Schemes.Cookie);

		string to = Uri.EscapeDataString(context.HttpContext.Request.Path);
		string signinPath = $"{SigninPath}?{QueryStrings.To}={to}";
		context.Result = new LocalRedirectResult(signinPath);
	}

	public async Task<IActionResult> OnPostSignoutAsync()
	{
		await HttpContext.SignOutAsync(Schemes.Cookie);

		return RedirectToPage(Paths.Pages.Index);
	}

	protected async Task IssueTicketAsync(int accountId)
	{
		Claim claim = new(Claims.AccountId, Convert.ToString(accountId));
		ClaimsIdentity identity = new([claim], Schemes.Cookie);
		ClaimsPrincipal principal = new(identity);
		await HttpContext.SignInAsync(Schemes.Cookie, principal);
	}

	protected TService GetService<TService>()
	{
		TService service = HttpContext.RequestServices.GetService<TService>()
			?? throw new InvalidOperationException();

		return service;
	}

	protected class AuthenticationFeature
	{
		public Account? Account { get; set; }
	}

	protected class Claims
	{
		public const string AccountId = nameof(Entities.Account.AccountId);
	}

	protected class QueryStrings
	{
		public const string To = "to";
	}

	protected class Schemes
	{
		public const string Cookie = CookieAuthenticationDefaults.AuthenticationScheme;
	}
}
