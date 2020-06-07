# OAuth

To protokół otwartej autentyfikacji. Jest to uniwersalny zbiór zasad z którego korzystają wszystkie większe platformy. Komunikacja jest za pośrednictwem Kluczy, po protokole https. To istotne żeby strona była https, w innym wypadku nie bedzie działać. 

Żeby móc się autoryzować przez fejsa, musimy włączyć `SSL` oraz zarejestrować naszą aplikację w fejsbooku.

Aby włączyć `SSL` w naszym projekcie, klikamy na naszą solucję, następnie `F4` 

![image-20200324143945897](I:\Notatki\C#\ASPNET\MVC\Fotografie z notatek\SSl)

Następnie klikamy na nasz projekt `PPM`, wchodzimy w `Właściwości`. 

I jeżeli to pole nie jest https, to zmieniamy je na takie.

![image-20200324144710952](I:\Notatki\C#\ASPNET\MVC\Fotografie z notatek\https)

Jak zobaczymy, nasza aplikacja jest wciąż dostępna po zwykłym http. Aby uniemożliwiśc taką rzecz robimy: 

`FilterConfig.cs`

```csharp
filters.Add(new RequireHttpsAttribute());
```

I z fb pozyskujemy nasz appId, appSecret. Później wklejamy to w `Startup.Auth.cs`. 

Dzieki temu możemy już z łatwością zalogować się do naszej aplikacji za pomocą Oauth.



Gdy później próbujemy przejśc dalej, zobaczymy że mamy problem z tym, ze nasze dodatkowe pole (driving licece ) nie jest kompatybilne z logowaniem się przez np FB. Aby to naprawić wchodzimy do:

![image-20200324163901372](I:\Notatki\C#\ASPNET\MVC\Fotografie z notatek\fb oath add)

Dodajemy nasz label i texbox to do tego pliku. Jak wyskakuje nam błąd że model nie ma zdefiniowanego takiego pola to dodajemy je do modelu. 

A następnie dodajemy wybrane przez nas pole w kontrolerze.

Następnie dane te są zapisywane w dwóch tablicach, aspNetUserLogins i AspNetUsers.