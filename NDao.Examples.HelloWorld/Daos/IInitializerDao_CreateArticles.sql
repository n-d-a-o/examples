create table if not exists Articles
(
	ArticleId		integer		primary key autoincrement,
	BlogId			integer		not null,
	AccountId		integer		not null,
	Title			text 		not null,
	Content			text 		not null,
	Version			integer		not null,
	CreatedDate		text		not null default (datetime('now')),
	UpdatedDate		text		not null default (datetime('now')),
	foreign key (BlogId) references Blogs(BlogId),
	foreign key (AccountId) references Accounts(AccountId)
)
;
