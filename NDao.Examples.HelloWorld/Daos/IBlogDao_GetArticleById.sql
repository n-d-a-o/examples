select
	 a.Title
	,a.Content
	,a.UpdatedDate
	,b.BlogName
from
	Articles a
inner join
	Blogs b
on
	a.BlogId = b.BlogId
where
	a.ArticleId = /*@articleId*/1
;
