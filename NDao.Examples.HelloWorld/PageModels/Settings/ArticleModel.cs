using Microsoft.AspNetCore.Mvc;
using NDao.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.Settings;

public class ArticleModel : PageModelBase
{
	private readonly ICrudDao<Article> articleCrud;

	private readonly ICrudDao<Blog> blogCrud;

	public Article? Article { get; private set; } = default!;

	[BindProperty]
	public ArticleFormModel ArticleForm { get; set; } = default!;

	public ArticleModel(ICrudDao<Article> articleCrud, ICrudDao<Blog> blogCrud)
	{
		this.articleCrud = articleCrud;
		this.blogCrud = blogCrud;
	}

	public IActionResult OnGet(int? articleId, int? blogId)
	{
		if (articleId is not null)
		{
			Article? article = articleCrud.Get(articleId);
			if (article is null)
			{
				return NotFound();
			}

			Article = article;

			ArticleForm = new ArticleFormModel
			{
				BlogId	= article.BlogId,
				Title	= article.Title,
				Content	= article.Content,
			};
		}
		else
		{
			if (blogId is null)
			{
				return BadRequest();
			}

			Blog? blog = blogCrud.Get(blogId);
			if (blog is null)
			{
				return BadRequest();
			}

			ArticleForm = new ArticleFormModel
			{
				BlogId	= blogId.Value,
			};
		}

		return Page();
	}

	public IActionResult OnPostInsert()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		Blog? blog = blogCrud.Get(ArticleForm.BlogId);
		if (blog is null)
		{
			return BadRequest();
		}

		Article article = new()
		{
			AccountId	= Account.AccountId,
			BlogId		= ArticleForm.BlogId,
			Title		= ArticleForm.Title,
			Content		= ArticleForm.Content,
		};
		article = articleCrud.Insert(article);

		return RedirectToPage(Paths.Pages.Settings.ArticleList, new { blogId = article.BlogId });
	}

	public IActionResult OnPostUpdate(int articleId)
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		Article? article = articleCrud.Get(articleId);
		if (article is null)
		{
			return NotFound();
		}

		article.Title	= ArticleForm.Title;
		article.Content	= ArticleForm.Content;
		article = articleCrud.Update(article);

		return RedirectToPage(Paths.Pages.Settings.ArticleList, new { blogId = article.BlogId });
	}

	public IActionResult OnPostDelete(int articleId)
	{
		Article? article = articleCrud.Get(articleId);
		if (article is null)
		{
			return NotFound();
		}

		article = articleCrud.Delete(article);

		return RedirectToPage(Paths.Pages.Settings.ArticleList, new { blogId = article.BlogId });
	}

	public class ArticleFormModel
	{
		public int BlogId { get; set; }

		public string Title { get; set; } = default!;

		public string Content { get; set; } = default!;
	}
}
