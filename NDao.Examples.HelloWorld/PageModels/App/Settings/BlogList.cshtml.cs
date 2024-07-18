using Microsoft.AspNetCore.Mvc;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.App.Settings;

public class BlogListModel : PageModelBase
{
	private readonly IBlogDao blogDao;

	public List<Blog> Blogs { get; private set; } = default!;

	public Blog Header { get; private set; } = default!;

	public BlogListModel(IBlogDao blogDao)
	{
		this.blogDao = blogDao;
	}

	public IActionResult OnGet()
	{
		Blogs = blogDao.GetBlogsByAccountId(Account.AccountId);

		return Page();
	}
}
