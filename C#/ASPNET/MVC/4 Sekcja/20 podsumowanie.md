# Podsumowanie

**Cheatsheet-** [Implementing-Validation-Cheat-Sheet.pdf](..\..\..\..\..\Kursy\Programowanie\c#\udemy\Mosh\The Complete ASP.NET MVC 5 Course\[Tutsgalaxy.com] - The Complete ASP.NET MVC 5 Course\05 Implementing Validation\attached_files\058 Cheat Sheet\Implementing-Validation-Cheat-Sheet.pdf) 

Ćwiczenie:

1. Dodać walidacje filmowego formularza. 
2. Zainicjalizować zmienne Release Date i Number in stock do wartości domyślnej dla danej własciwości.
3. Wszyskie pola są wymagane
4. Walidacja Number in stock ma byc z przedzialu od 1 do 20.
5. Dodać client side validation
6. dodać AntiForgeryToken

Problemy:

Ta sekcja poszła u mine bezproblemow. jedyne co możaby było zmienić to pochować warości domyślne przekazywanych do formularza właściwości. Można to zrobić przyporządkując je na sztywno, albo w ViewModelu pozamieniać pare rzeczy tak aby zamiast np 

```csharp
public Customer Customer {get;set;}
```

Widniały wszyskie właściości klasy `Customer` wraz z jej atrybutami.

W tej sekcji robiłem:

- Data Annotation

- Custom Validation

- Client-side Validation

- Anti-forgery Tokens

  