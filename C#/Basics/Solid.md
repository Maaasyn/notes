## SOLID



Jest to zbiór zasad jak ładnie produkować kod. Stworzone przez c Martina.



##### S - Single Responsibility Principle

Dana metoda, klasa, cecha naszego obiektu powinna być odpowiedzialna za jak najmniej rzeczy, co pozwali nam pisać w chuj więcej kodu jak jakaś małpa, ale za to będzie wszystko dużo dużo czytelniejsze. 

Dla przykładu klasa ` Dziennik` nie powinna zawierać w sobie metody `zapisz do pdf` . Zadanie dziennika kończy się na posiadaniu kartek, notatek itp. Zapisywać powinna klasa odpowiedzialna za zapisywanie.

##### O - Open-Closed Principle.

Klasa powinna mieć możliwość bycia rozwijaną, ale nie zmieniana wewnętrznie.

`otwarcie` na dodawanie funkcjunalności

`zamkniecie` na modyfikowanie.



##### Liskov Substitution Principle



##### Interface Segregation Principle



##### Dependency Inversion Principle