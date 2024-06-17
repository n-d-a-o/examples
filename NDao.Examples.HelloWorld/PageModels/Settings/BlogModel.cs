using Microsoft.AspNetCore.Mvc;
using NDao.Daos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.PageModels.Settings;

public class BlogModel : PageModelBase
{
	private readonly ICrudDao<Blog> blogCrud;

	public Blog? Blog { get; private set; } = default!;

	[BindProperty]
	public BlogFormModel BlogForm { get; set; } = default!;

	public BlogModel(ICrudDao<Blog> blogCrud)
	{
		this.blogCrud = blogCrud;
	}

	public IActionResult OnGet(int? blogId)
	{
		if (blogId is not null)
		{
			Blog? blog = blogCrud.Get(blogId);
			if (blog is null)
			{
				return NotFound();
			}

			Blog = blog;

			BlogForm = new BlogFormModel
			{
				BlogName	= blog.BlogName,
				Description	= blog.Description,
			};
		}
		else
		{
			BlogForm = new BlogFormModel();
		}

		return Page();
	}

	public IActionResult OnPostInsert()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		Blog blog = new()
		{
			AccountId	= Account.AccountId,
			BlogName	= BlogForm.BlogName,
			Description	= BlogForm.Description,
		};
		blogCrud.Insert(blog);

		return RedirectToPage(Paths.Pages.Settings.BlogList);
	}

	public IActionResult OnPostUpdate(int blogId)
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		Blog? blog = blogCrud.Get(blogId);
		if (blog is null)
		{
			return NotFound();
		}

		blog.BlogName		= BlogForm.BlogName;
		blog.Description	= BlogForm.Description;
		blogCrud.Update(blog);

		return RedirectToPage(Paths.Pages.Settings.BlogList);
	}

	public IActionResult OnPostDelete(int blogId)
	{
		Blog? blog = blogCrud.Get(blogId);
		if (blog is null)
		{
			return NotFound();
		}

		blogCrud.Delete(blog);

		return RedirectToPage(Paths.Pages.Settings.BlogList);
	}

	public class BlogFormModel
	{
		public string BlogName { get; set; } = default!;

		public string Description { get; set; } = default!;
	}
}
