# Autentykacja

Mamy w asp net z urzędu 3 rodzaje authentication.

* Pojedyncze osobiste konta poszczególnych użytkowników. (*Najpopularniejsze*)
* Organizacyjne, ograniczające do wybranych emailów.
* Windows authentication ograniczajace do kont uzytkowników windowsa w danej sieci intranet.
* Jest też do wyboru brak authentication.

Całość tej Autentykacji zarządzana jest przez abstrakcję nazwaną `ASP.NET Identity`

Poniższy diagram prezentuje jak rozwijał się ten framework

#### ![image-20200320145745146](I:\Notatki\C#\ASPNET\MVC\Fotografie z notatek\Historia-identity)



Architektura Identity daje nam pare klas które ułatwiają nam zarządzaniem rolami.

![image-20200320145927280](I:\Notatki\C#\ASPNET\MVC\Fotografie z notatek\api-aspnet-identity)

Kiedy pierwszy raz inicjowaliśmy nasze tabele, na początku inicjalizowaliśmy też z urzędu modele tego frameworka.

> Dlatego znajdziemy w naszych migracjach klasy takie jak ``IdentityUser`` oraz `ApplicationDbContext`.

W `AcconutController.cs` możemy się przyjrzeć jak wygląda cały ten famework od środka.

---

Mamy taki atrybut jak `[Authorize]`, nazywany czasem filtrem. Może byc uzyte na akcji i wezwane przez mvc framework przed i po tej akcji. A więc jak nakładamy ten filtr na akcje, zanim akcja jest wykonana atrybut sprawdzi czy aktualny użytkownik jest zalogowany czy nie. Jeżeli nie, przekieruje klienta do strony logowania, 



Atrybut ten, możemy dać na pojedyną metodę naszego kontrolera, tak jak w przypadku poniżej:

```csharp
 [Authorize]
        public ActionResult Index()
        {
            return View();
        }
```

Bądź na cały kontroler, wtedy wszystkie metody naszego kontrolera będą dostępne dopiero po zalogowaniu.

```csharp
 [Authorize]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        ...
        ...
            
    }

```

Może być też aplikowane globalnie, kiedy wszystkie funkcjonalności naszej aplikacji zależne są od tego, czy dany użytkownik jest zalogowany.

![image-20200320152715962](I:\Notatki\C#\ASPNET\MVC\Fotografie z notatek\filtr-globalny)

```csharp
filters.Add(new AuthorizeAttribute());
```

Dzięki temu wszystko jest zablokowane.

Aby odblokować jakiś kontroler dla niezalogowanych użytkowników, dodajemy nad wybranym kontrolerem atrybut `[AllowAnonymous]`. 

---

Teraz mosh pokazuje jak przydzielać rolę. Ja zrobiłem takie role:

```
user:
passwd:

guest@vidly.com
Niebieski1!
```

później Mosh edytuje role userów w `AccountController` w metodzie

```csharp
public async Task<ActionResult> Register(RegisterViewModel model)
```

```csharp

//Temp code
var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
var roleManager = new RoleManager<IdentityRole>(roleStore);
await roleManager.CreateAsync(new IdentityRole("CanManageMovie"));
await UserManager.AddToRoleAsync(user.Id, "CanManageMovie");
```

Później stworzyłem sobie tą postać

```
user:
passwd:

admin@vidly.com
Niebieski1!
```

Później zaseedowałem moją bazę tworząc nową migrację danych.

<tip>

Aby łatwo stworzyć sobie skrypt populujący daną bazę, możemy łatwo i przyjemnie zrobić ją klikacjąc w `SQL Server Object Explorer` i tam wybrac tabele -> view data, następnie wybrać odpowiednie dla nas wiersze i kliknąć script. To stowrzy nad dokładny skrypt, jak zrobić danego delikwenta.

Potrzebne rekordy znajdziemy w:

* AspNetUserRoles
* AspNetRoles
* AspNetUsers

```csharp
  public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5d0af03a-1baa-4c6d-a6dd-775e2943cf37', N'guest@vidly.com', 0, N'AG8rXLFlYQeRJuDxiM3S5uESde8IfHnEYVbcLATIvg7uQCsqM6aITA1j5i1cD/caRg==', N'a776ec62-3c6b-4c6c-b333-6adbaf0a1151', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8402c395-d15b-41f6-baf0-53cf2b1d09ab', N'admin@vidly.com', 0, N'AJ2IJ93M5NpBzKT8o3JQG3ulSs9htokD8WkGSBqxc8qYDEkxGNCKtbAaL5+FbvJajw==', N'1d605ae5-d1ed-4bca-b8ca-f753fe0dce86', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'307e38c8-a06d-4805-96c6-b035b685dc80', N'CanManageMovie')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8402c395-d15b-41f6-baf0-53cf2b1d09ab', N'307e38c8-a06d-4805-96c6-b035b685dc80')

");
        }
```

To sprawi że nasze tabele samę się uzupełnią właściwym kontentem.

Mosh również tłumaczył wyższość tej metody nad tą, sugerowaną przez MS. Powiedział że lepiej ta.

Później Mosh zmienił