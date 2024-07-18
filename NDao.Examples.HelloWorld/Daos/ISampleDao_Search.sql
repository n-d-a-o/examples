select
	*
from
	Persons
where
	true = true

--# if (@name is not null) {
	and Name like /*@name*/'%A%'
--# }

--# if (@age is not null) {
	and Age = /*@age*/20
--# }

--# if (@occupation is not null) {
	and Occupation like /*@occupation*/'Developer'
--# }

order by
	Name
;
