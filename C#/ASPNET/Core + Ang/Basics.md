# Dotnet

info z konsoli 

```
dotnet --help

dotnet new --help
#lista opcji

dotnet new webapi --help
#dalej widziy co i jak.

dotnet new webapi -n {{nazwa}}
#tak sie robi nowe z nazwa
```

przydatne extensions do uzywania z vsc z c#

* C#

* C# Extensions (robienie klas z poziomu IDE itp)

* nuget package manager

  > generate assets w konsoli.

Raczej nie warto robić dotnet core z angularem bo angular tam jest stary, pozatym lepiej miec wszystko osobno, baze, front i backend. 

Niektóre settingsy są w pliku appsettings.json



```
dotnet watch run
//nie musimy restartowac za kazdym razem, kiedy wprowadzamy zmiany
```

W netcorze kiedy chcemy dodać jakąś zewnętrzną usługę, robimy to w `startup.cs`.

W moim wypadku dodaje sobie usługoe serwera Sqlite jako moja baza danych.

```csharp
   public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlite("Connectionstring"));
            services.AddControllers();
        }
```

Koniecznym było dodanie odpowiednich dependencies. 

Naspętpnie w appsettings.json dodajemy:

```json
"ConnectionStrings": {
    "DefaultConnection" :  "Data Source=DatingApp.db" 
  }
```

Nasz program jednak wciąż nie widzi tej bazy więc w startup.cs:

##### Angularowe extensions

* angular snippets
* angular files
* angular language service
* auto rename tag
* bracket pair colorizer 2
* debbuger for chrome
* (material icon theme)
* prettuer
* tslint
* angular2-switcher

Aby dodać możliwość obsługi zapytań http, dodajemy moduł http do importów naszej aplikacji. `HttpClientModule`

W świecie angulara nie używa się konstruktorów do niczego, poza wstrzykiwaniem wartości.

Kiedy pobieramy  dane z innego źródła, nasza przeglądarka będzie sie pluła, i kazała nam ustawić Cors policy.

### Ustawianie Cors

Aby dodać cors, w startup.cs w metozdie `ConfigureServices` dodajemy 

```csharp
services.AddCors();
```

a w `Configure`

```csharp
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//badz

```

### Git

Najpierw init, potem repo z githuba, potem połączenie tego tytorialem z gita.

### Security

Każde przyjęte hasło musimy hashować. Jest to pierwszy krok.

Później koniecznym jest aby nasz hash **saltować**. 

**Salting**- to proces dodawania losowości naszych haseł. 

Robimy sobie nasz model:

```csharp
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
```

### Repository pattern

Jest to potrzebne API żeby móc potem podmienić ormy. Dzieki temu, unika się powtarzania kodu, plus ma sie testowalny kod. 

Również ten pattern pozwala nam odłączyć zależność od frameworka.

> wrócić do lekcji 07. O hashowaniu

Wstrzykujemy do naszych serwisów nasze kombo.

```csharp
services.AddScoped<IAuthRepository, AuthRepository>();
```

### Dto



### JWS

JSON Web Tokens.

Insdustry standard. Self-contained.

Zrobiony jest z 3 czesci. 

* Header
* Payload (informacje w tokenie, zwykle sa tu informacje o autoryzacji, moze to byc tez username jak i inne.) ten i powyzzsze moga byc encodowane przez kazdego. 

![image-20200409180530339](I:\Notatki\C#\ASPNET\Core + Ang\Obrazy\JWS)

zwykle mamy tu tez takie pola jak nbfm, i exp.

nbf means not before- dzien od kiedy token moze byc uzywany, zazwyczaj jest to to samo co iat czyli issuad at, data wydania tokenu. Również widzimy tam exp, czyli do kiedy token moze byc uzywany. 

* secret, tam zachowana jest enrypcja, mowi nam o hashu, secret jest zachowoany na serwerze i nigdy nie jest podany dla klienta

![image-20200409180933405](I:\Notatki\C#\ASPNET\Core + Ang\Obrazy\token)

* pierwsza czesc to header, tutaj jest algorytm opisany. 
* druga czesc to payload
* trzecia to signatura. 

### Real life scenario

Klient wysyła swój username i hasło. 

klient —username & password—**>** serwer

klient **<**— Sprawdza i wysyla JWT— serwer

klient —Z kazdy zapytaniem wysyla JWT—> serwer

klient <— waliduje JWS i wysyla odpowiedz— serwer

### Secret storage

```csharp
dotnet user-secrets init
```

Uzywamy abu tworzyc klucz. 

### Summary moduły 3.

* Storing password safety in the database. 
* Repository pattern
* Creating auth controller
* dtos
* token authentication

# Modul 4

* Login and navbar
* angular template forms
* using services in angular
* conditionaly displaying elements on a page
* create a registe component
* component communication (with each other)

### Templaty w angularze

2 rodzaje:

* Reactive forms
* Template form (prostsze)

```html
<form #loginForm="ngForm" class="form-inline my-2 my-lg-0" (ngSubmit)="login()">
    <input class="form-control mr-sm-2" type="text" placeholder="Username" name="username" required
           [(ngModel)]="model.username">
    <input class="form-control mr-sm-2" type="text" placeholder="Password" name="password" required
           [(ngModel)]="model.password">
    <button [disabled]="!loginForm.valid" class="btn btn-success my-2 my-sm-0" type="submit">Login</button>
  </form>
```

### Services

```typescript
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }
}

```

Pole `providedIn` informuje nas z ktorego  modulu ten serwis jest provided from.

Koniecznym jest dodać do providers nasz service w app.module.ts.

### Summary of section 4

* Services 
* conditional display
* input properties
* output properties

### Exeptation handling

Albo walimy try-catcha z łapy albo globalny catcher.



w startup.css

```csharp
if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message);
                            }
                        });
                });
            }
```

> koniecznym bylo zrobienie dodatkowej klasy extentsin

```csharp
public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
```

Error handling był wytłumaczony rakietowo, i jest opisany w _services/error.interceptor.ts