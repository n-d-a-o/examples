using Microsoft.AspNetCore.Mvc;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.Settings;

public class HomeModel : PageModelBase
{
	private readonly IBlogDao blogDao;

	public bool HasBlogs { get; private set; }

	public HomeModel(IBlogDao blogDao)
	{
		this.blogDao = blogDao;
	}

	public IActionResult OnGet()
	{
		List<Blog> blogs = blogDao.GetBlogsByAccountId(Account.AccountId);
		HasBlogs = 1 <= blogs.Count;

		return Page();
	}
}
