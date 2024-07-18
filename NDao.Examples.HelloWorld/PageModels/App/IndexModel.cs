using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDao.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.App;

[AllowAnonymous]
public class IndexModel : PageModelBase
{
	private readonly ICrudDao<Blog> blogCrud;

	public List<Blog> Blogs { get; private set; } = default!;

	public Blog Header { get; private set; } = default!;

	public IndexModel(ICrudDao<Blog> blogCrud)
	{
		this.blogCrud = blogCrud;
	}

	public IActionResult OnGet()
	{
		Blogs = blogCrud.GetList();

		return Page();
	}
}
