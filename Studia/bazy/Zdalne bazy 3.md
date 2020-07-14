# Zadania powtórkowe

> 1. Napisać funkcję zwracającą wartość zamówień realizowanych z kraju (parametr) i kategorii (parametr) w danym roku (parametr).

```mysql
create function Zadanie1(@country Varchar(Max),@category Varchar(max),@year int)
returns int
begin
return (select sum(od.UnitPrice*od.Quantity) as 'Suma' 
from Customers c join orders o on c.CustomerID=o.CustomerID 
join [Order Details] od on o.OrderID=od.OrderID 
join products p on od.ProductID=p.ProductID 
join categories ca on p.CategoryID=ca.CategoryID where country=@country AND CategoryName=@category and Year(OrderDate)=@year
 ) 
end

```

##### Test

```mssql
select dbo.zad1a('USA','Beverages',1997) as 'Suma'
```

> 2. Napisać funkcję zwracającą średnią liczbę dni pomiędzy zamówieniem i wysyłką dla danego pracownika (parametr – nazwisko pracownika) i roku (parametr) - należy wziąć pod uwagę atrybuty orderdate i shippeddate.

```mysql
create function Zadanie2(@surname Varchar(Max),@year int)
returns int
begin
return (Select AVG(datediff(dd,OrderDate,ShippedDate)) from Orders o
join Employees em on em.EmployeeID=o.EmployeeID
where Year(OrderDate)=@year and em.LastName=@surname
 ) 
end
```

##### Test

```mysql
Select dbo.Zadanie2('Buchanan',1997)
```

> 3. Napisać funkcję zwracającą wartość zamówień realizowanych z kraju (parametr) i kategorii (parametr) w danym roku (parametr).

```mysql
create function Zadanie3(@year int)
returns table
as
return (Select MAX(freight) as najwieksza,MIN(freight) as najmniejsza,AVG(Freight) as srednia 
from Orders where year(OrderDate)=@year
 );
```

##### Test

```mysql
select * from Zadanie3(1996)
```

> 4. Napisać funkcję zwracającą n najlepszych (najdroższych) zamówień w danym roku (parametr) – uwaga – należy wykorzystać klauzulę TOP n

```mysql
create function Zadanie4(@year int,@quantity int)
returns table
as
return (Select top(@quantity)
(UnitPrice*Quantity) as zamowienie from Orders o
join [Order Details] od on od.OrderID=o.OrderID
where year(OrderDate)=@year
order by UnitPrice*Quantity desc); 
```

##### test

```mysql
 select * from Zadanie5(1997,11)
```

# Procedury

> Przykład 1.

```mysql
create procedure pierwsza
as
print 'procedura sie wykonala'
```

> Przykład 2.

```mysql
create procedure druga @ile int
as
print @ile

exec druga 10
```

> Przykład 3

```mysql
create procedure dane_matek
as
select * from studenci
where plec='K' and liczba_dzieci>0

exec dane_matek
```

> Przykład 4

```mysql
create procedure dodaj_dziecko @miasto varchar(50) @liczba_dzieci int
as
update studenci set liczba_dzieci=liczba_dzieci +@liczba_dzieci
where miasto = @miasto

exec dodaj_dziecko2 'Katowice' ,3
create procedure dodaj_dziecko @miasto varchar(50)
as
update studenci set liczba_dzieci=liczba_dzieci +1
where miasto = @miasto
```

> Zadanie 101 – zmodyfikować przykład 4, aby można było do procedury przekazać również liczbę dzieci, które mają być dodane.

```mysql
create procedure dodaj_dziecko2 @miasto varchar(50), @liczba_dzieci int
as
update studenci set liczba_dzieci=liczba_dzieci +@liczba_dzieci
where miasto = @miasto

```

> Zadanie 102 – zmodyfikować poprzednie zadanie, aby przed aktualizacją danych nastąpiła walidacja liczby dzieci (nie za duża, nie za mała

```mysql

Create procedure dodaj_dziecko102 @miasto varchar(50), @liczba_dzieci int
as
UPDATE studenci
SET liczba_dzieci = CASE
        WHEN @liczba_dzieci <=0 THEN 'błąd' 
        WHEN @liczba_dzieci>=10 THEN 'błąd' 
        ELSE liczba_dzieci +@liczba_dzieci
      END
Where miasto=@miasto
exec dodaj_dziecko102 'Katowice',9
Select * from studenci
```

> Zadanie 103 – napisać procedurę wyświetlającą zamówienia z danego miesiąca (parametr) danego roku (parametr). W procedurze należy walidować poprawność przekazywanych wartości parametrów.

```mysql
select * from Orders where Year(OrderDate)=1998 --and Month(OrderDate)=7

alter procedure zad103 @rok int, @miesiac int
as
IF (@rok between (Select MIN(YEAR(OrderDate)) from Orders )  and (Select MAX(YEAR(OrderDate)) from Orders))
And (@miesiac between 1  and 12 )
select *
from Orders 
where Year(OrderDate)=@rok and Month(OrderDate)=@miesiac
ELSE
SELECT 'BRAK danych'
exec zad103 1993,2
```

> Zadanie 104 – procedurę z zadania poprzedniego zmodyfikować tak, aby miesiąc można było podać polską nazwą.

```mysql
SET LANGUAGE Polish
alter procedure zad104 @rok int, @miesiac varchar(30)
as
IF (@rok between (Select MIN(YEAR(OrderDate)) from Orders )  and (Select MAX(YEAR(OrderDate)) from Orders))
And (@miesiac between 1  and 12 )
select *
from Orders 
where Year(OrderDate)=@rok and datename(mm,OrderDate)=@miesiac
ELSE
SELECT 'BRAK danych'
exec zad104 1998,czerwiec
```

> Zadanie 105 – napisać procedurę dodającą nowego klienta (companyname) do tabeli customers w bazie Northwind i jednocześnie wyświetlającą klientów, którzy niczego nie kupili.

```mssql
alter procedure zad105 @nazwa varchar(max),@id varchar(max)
as
Insert into Customers(CustomerID,CompanyName) VALUES (@nazwa,@id)
SELECT * from customers where CustomerID not in (Select CustomerID from Orders)

exec zad105 6,7
```

