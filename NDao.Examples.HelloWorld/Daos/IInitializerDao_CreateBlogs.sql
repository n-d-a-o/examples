create table if not exists Blogs
(
	BlogId			integer		primary key autoincrement,
	AccountId		integer		not null,
	BlogName		text 		not null,
	Description		text 		not null,
	CreatedDate		text		not null default (datetime('now')),
	UpdatedDate		text		not null default (datetime('now')),
	foreign key (AccountId) references Accounts(AccountId)
)
;
