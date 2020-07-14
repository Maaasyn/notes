> 1. Na podstawie danych z tabeli ‘studenci’ - napisać funkcję, zwracającą sumę liczby dni życia osób, które mają liczbę dzieci – przekazaną w parametrze funkcji. Długość życia należy odnieść do bieżącej daty. 

```mysql
create function iloscdnizycia(@ldzieci int)

returns table

return(

select imie,nazwisko,DATEDIFF(dd,data_urodzenia,getdate())as 'dni zycia' from studenci where liczba_dzieci=@ldzieci)
select * from dbo.iloscdnizycia(2)

```

> 2. Na podstawie danych z tabeli ‘studenci’ napisać funkcję, zwracającą dane osób, które będą obchodziły urodziny w najbliższych n dniach (n powinno być parametrem funkcji). 

```mysql
create function najblizszysolenizant(@dni int)

returns table

return (select imie,nazwisko,data_urodzenia from studenci 

where datepart(dy,data_urodzenia)>DATEPART(dy,GETDATE()) and datepart(dy,data_urodzenia)<DATEPART(dy,GETDATE())+@dni)


select * from dbo.najblizszysolenizant(20)
```

> 3. Napisać procedurę zwiększającą liczbę dzieci osobie o podanym w parametrze nazwisku. Drugi parametr to liczba dzieci, o którą należy zwiększyć dotychczasową wartość. 

```mysql
create procedure dodaj_dziecko_nazwisko @nazwisko varchar(50), @ile int

as 

update studenci set liczba_dzieci=liczba_dzieci+@ile

where nazwisko=@nazwisko

exec dodaj_dziecko_nazwisko 'Wiejski',6
```

> 4. Napisać wyzwalacz, który uniemożliwi dopisanie nowej osoby, która posiada dzieci a jednocześnie nie ma 15 lat. Sprawdzić działanie wyzwalacza. 

```mysql
create trigger nieletnirodzic
on studenci
for insert
as 
if (select liczba_dzieci from inserted)>0 and (select data_urodzenia from inserted)>getdate()-Year(18)
begin
print('Nie za wcześnie na dzieci?! DO KSIĄŻEK !!!')
rollback
end
insert into studenci(imie,data_urodzenia,liczba_dzieci) values('Mati','2019-06-21',1)
```

> 5. Zadanie powyższe zrealizować tworząc odpowiednią strukturę tabeli (create table).

```mysql
 CREATE TABLE nowistudenci(

 "imie" varchar(40) default NULL,

 "nazwisko" varchar(40) default NULL,

 "data_urodzenia" datetime default NULL,

 "plec" char(1) default NULL,

 "miasto" varchar(40) default NULL,

 "liczba_dzieci" int default NULL, 

 check( (liczba_dzieci=0 and (datediff(year,data_urodzenia,getdate()))<15) 

 or   (liczba_dzieci>=0 and(datediff(year,data_urodzenia,getdate()))>15)) )
```

> 6. Napisać wyzwalacz zapisujący w tabeli ‘logi’ – datę i czas oraz informacje o usuwanym studencie. 

```mysql
create trigger usunieci

on studenci 

for delete

as

begin

insert into logitable values((select imie from deleted),(select nazwisko from deleted),getdate())

end
```

> 7. Utworzyć tabelę [osoby]={id, nazwisko, pesel}W tej tabeli utworzyć wyzwalacz uniemożliwiający wpisanie do bazy niepoprawnego numeru pesel. Kod weryfikujący pesel zrobiliśmy na zajęciach w murach uczelni. 

![7](I:\Notatki\Studia\bazy 4 wm\7.png)

> 8. Utworzyć funkcję odwracającą tekst podany jako parametr 

![8](I:\Notatki\Studia\bazy 4 wm\8.png)

> 9. Utworzyć funkcję inicjały – zwracającą inicjały osoby, które imię i nazwisko zostanie przekazane jako parametry. Sprawdzić działanie funkcji w tabeli ‘studenci_mysql’. 

![9](I:\Notatki\Studia\bazy 4 wm\9.png)

> 10. Utworzyć funkcję zwracającą połączenie dwóch ciągów znaków rozdzielonych spacją. Pierwszy ciąg powinien być pisany wielkimi literami, drugi ciąg znaków powinien rozpoczynać się wielką literą, a reszta liter – małe. Taką funkcję można wykorzystać do przygotowywania czytelnych wydruków podając jako argument nazwisko i imię. Przykładowy wynik funkcji: np. ‘KOWALSKI Jan’. Sprawdzić działanie funkcji w tabeli ‘studenci_mysql’. 

![10](I:\Notatki\Studia\bazy 4 wm\10.png)

> 11. Utworzyć funkcję zwracającą płeć na podstawie imienia przekazanego jako argument funkcji. 

![11](I:\Notatki\Studia\bazy 4 wm\11.png)

