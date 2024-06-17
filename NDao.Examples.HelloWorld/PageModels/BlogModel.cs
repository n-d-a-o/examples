using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDao.Daos;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels;

[AllowAnonymous]
public class BlogModel : PageModelBase
{
	private readonly ICrudDao<Blog> blogCrud;

	private readonly IBlogDao blogDao;

	public Blog Blog { get; private set; } = default!;

	public List<Article> Articles { get; private set; } = default!;

	public BlogModel(ICrudDao<Blog> blogCrud, IBlogDao blogDao)
	{
		this.blogCrud = blogCrud;
		this.blogDao = blogDao;
	}

	public IActionResult OnGet(int blogId)
	{
		Blog? blog = blogCrud.Get(blogId);
		if (blog is null)
		{
			return NotFound();
		}

		Blog = blog;
		Articles = blogDao.GetArticlesByBlogId(blogId);

		return Page();
	}
}
