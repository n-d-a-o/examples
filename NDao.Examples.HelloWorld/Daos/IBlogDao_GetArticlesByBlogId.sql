select
	 ArticleId
	,BlogId
	,Title
	,AccountId
	,substr(Content, 1, 20) as Content
	,Version
	,CreatedDate
	,UpdatedDate
from
	Articles
where
	BlogId = /*@blogId*/1
order by
	ArticleId desc
;
