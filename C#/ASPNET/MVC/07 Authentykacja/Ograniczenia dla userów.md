# Ogranicznia dla userów

Jeżeli zrobimy wszystkie kroki wyżej, możemy potem badać role userów, i na tej podstawie wrzucać im prawa do jakiegoś kontentu.

```csharp
 public ActionResult Index()
        {
            if (User.IsInRole("CanManageMovie"))
            {
                return View("List");
            }
            else
            {
                return View("ReadOnlyList");
            }

```

Aby autoryzować userów na tylko okreslone przez nas akcje kontrolerów, robimy taki myk:

```csharp
[Authorize(Roles = "CanManageMovie")]
        public ActionResult New()
        {
            var viewModel = new MoviesFormViewModel
            {
                .....
                   ....
```

Teraz tylko użytkownicy z rolą `CanManageMovie` mogą widziec dany kontent.

> Problem

Zaczyna nam rosnąć ilość Magic strings, które ciężko modernizować. 

Najlepszym sposobem jest zrobić nową klase statyczną która jako pole będzie posiadała wartość naszej wybtanej roli. Demonstruje to poniższy przykład:

```csharp

namespace aspnet_vidly.Models
{
    public static class RoleName
    {
        public const string CanManageMovies = "CanManageMovie";
    }
}
```

I wszedzie gdzie mamy magiczny string, w to miejsce wrzucamy nasza klase statyczną. 

```csharp
[Authorize(Roles = RoleName.CanManageMovies)]
```

---

# Adding profile data

Nasz bazowy register, ssie dupę. Zmieniamy go bo chcemy mieć więcej info o naszym uzytkowniku. 

Zawsze zazynamy od domeny.

![image-20200320164918570](I:\Notatki\C#\ASPNET\MVC\Fotografie z notatek\adding-new-fields-to-user)

Może to wyglądać tak:

```csharp
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string DrivingLicence { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
```

Z racji że zmodyfikowaliśmy naszą domenę musimy zrobić migrację.

Skoro dodajemy nowe wymagane pole do naszych userów, co w sytuacji kiedy mamy już jakąś bazę?

> W takiej sytuacji EF zrobi w tym polu empty string, żeby nie było nulla.

### Widok

Nastepnie w widoku dodajemy pole do formularza z naszym driving licence. Widzimy brak intelsensa, dlaczego?

- Bo Model w naszym widoku jest zbudowany za pomocą viewModelu w którym nie ma właściwej deklaracji na potrzebny nam atrybut. Więc dodajemy go do ViewModelu.

Następnie w kontrolerze dodajemy pole w Akcji Register, o tak:

```csharp
var user = new ApplicationUser 
                    { 
                        UserName = model.Email,
                        Email = model.Email,
                        DrivingLicence = model.DrivingLicense
                    };
```