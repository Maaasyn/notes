> Zadanie powtórkowe

```mysql
create table osoby(
id int identity primary key,
nazwisko varchar(MAX),
pesel varchar(11)
)
create function sprawdz_pesel(@pesel varchar(11))
returns bit
begin
declare @wynik bit
set @wynik=0
if(len(@pesel)<>11)
set @wynik = 0
if(isnumeric(@pesel)!=1)
set @wynik = 0
declare @suma int
set @suma=(select cast(substring(@pesel,1,1) as int) * 1 +
cast(substring(@pesel,2,1) as int) * 3 +
cast(substring(@pesel,3,1) as int) * 7 +
cast(substring(@pesel,4,1) as int) * 9 +
cast(substring(@pesel,5,1) as int) * 1 +
cast(substring(@pesel,6,1) as int) * 3 +
cast(substring(@pesel,7,1) as int) * 7 +
cast(substring(@pesel,8,1) as int) * 9 +
cast(substring(@pesel,9,1) as int) * 1 +
cast(substring(@pesel,10,1) as int) * 3 )
declare @modulo int
set @modulo = @suma %10
if(cast (substring(@pesel,11,1) as int) = 10-@modulo)
set @wynik=1
return @wynik
end
create trigger czy_pesel
on osoby
for insert
as
if(select count(*) from inserted i where dbo.sprawdz_pesel(i.pesel) =
0) > 0
begin
print'niepoprawny pesel'
rollback
end
```

![](I:\Notatki\Studia\bazy 5\przygotowanie db.jpg)

![zad 2 - powtorkowe](I:\Notatki\Studia\bazy 5\zad 2 - powtorkowe.jpg)

![zad 3 - powtorkowe](I:\Notatki\Studia\bazy 5\zad 3 - powtorkowe.jpg)

![zad 4 - powtorkowe](I:\Notatki\Studia\bazy 5\zad 4 - powtorkowe.jpg)

![zad 101](I:\Notatki\Studia\bazy 5\zad 101.jpg)

![zad 102](I:\Notatki\Studia\bazy 5\zad 102.jpg)

![zad 103](I:\Notatki\Studia\bazy 5\zad 103.jpg)

![zad 104](I:\Notatki\Studia\bazy 5\zad 104.jpg)