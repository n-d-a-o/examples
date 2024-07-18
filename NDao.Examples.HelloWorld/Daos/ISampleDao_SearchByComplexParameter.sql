select
	*
from
	Persons
where
	true = true

--# if (@condition.Name is not null) {
	and Name like /*@condition.Name*/'%A%'
--# }

--# if (@condition.Age is not null) {
	and Age = /*@condition.Age*/20
--# }

--# if (@condition.Occupation is not null) {
	and Occupation like /*@condition.Occupation*/'Developer'
--# }

order by
	Name
;
