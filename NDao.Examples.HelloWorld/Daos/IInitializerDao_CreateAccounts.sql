create table if not exists Accounts
(
	AccountId		integer		primary key autoincrement,
	Name			text 		not null,
	CreatedDate		text		not null default (datetime('now')),
	UpdatedDate		text		not null default (datetime('now'))
)
;
