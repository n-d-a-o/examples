create table if not exists Persons
(
	Id			integer		primary key,
	Name		text 		not null,
	Age			integer		not null,
	Occupation	text 		not null
)
;
