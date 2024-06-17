using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Dtos;

namespace NDao.Examples.HelloWorld.PageModels;

[AllowAnonymous]
public class ArticleModel : PageModelBase
{
	private readonly IBlogDao blogDao;

	public ArticleDto Article { get; private set; } = default!;

	public ArticleModel(IBlogDao blogDao)
	{
		this.blogDao = blogDao;
	}

	public IActionResult OnGet(int articleId)
	{
		ArticleDto? article = blogDao.GetArticleById(articleId);
		if (article is null)
		{
			return NotFound();
		}

		Article = article;

		return Page();
	}
}
