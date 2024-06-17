using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NDao.Examples.HelloWorld.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.Settings;

public class ArticleListModel : PageModelBase
{
	private readonly IBlogDao blogDao;

	public List<SelectListItem> BlogListItems { get; private set; } = default!;

	public BlogFormModel BlogForm { get; set; } = default!;

	public Article Header { get; private set; } = default!;

	public List<Article> Articles { get; private set; } = default!;

	public ArticleListModel(IBlogDao blogDao)
	{
		this.blogDao = blogDao;
	}

	public IActionResult OnGet(int? blogId)
	{
		List<Blog> blogs = blogDao.GetBlogsByAccountId(Account.AccountId);
		if (blogs.Count < 1)
		{
			return NotFound();
		}

		Blog? selectedBlog = blogId is null
			? blogs[0] // default blog
			: blogs.FirstOrDefault(blog => blog.BlogId == blogId);
		if (selectedBlog is null)
		{
			return NotFound();
		}

		BlogListItems = blogs
			.Select(blog => new SelectListItem(blog.BlogName, blog.BlogId.ToString()))
			.ToList();

		BlogForm = new BlogFormModel
		{
			BlogId = selectedBlog.BlogId
		};

		Articles = blogDao.GetArticlesByBlogId(selectedBlog.BlogId);

		return Page();
	}

	public class BlogFormModel
	{
		public int? BlogId { get; set; }
	}
}
