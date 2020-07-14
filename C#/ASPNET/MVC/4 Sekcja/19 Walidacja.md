# Walidacja

W aspnet walidacja jest trzy stopniowym procesem. 

1. Wpierwej dodajemy adnotacje do naszych entities.

```csharp
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
```

> W tym przykładzie widzimy że nasze anotacje wymagają aby właściwość name nie była dłuższa niż 255 znaków.

2. Dodaj `ModelState.IsValid` żeby sprawdzić czy obiekt przekazywany w formularzu jest właściwy. 

```csharp
 public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
 }
```

To rozwiązanie powoduje nam kilka błędów, gdyż informacja o Customer.Id nie jest przesyłana w naszym ukrytym formularzu. Rozwiązaniem na tą bolączkę jest:

```csharp
    if (Model.Customer == null)
    {
        @Html.HiddenFor(m => m.Customer.Id, new { Value = 0 })
    }
    else
    {
        @Html.HiddenFor(m => m.Customer.Id)
    }
```

Drugi sposob na rozwiązanie tego problemu to zainicjowanie właściwości Customer naszej klasy `CustomerFormViewModel`

```csharp
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var ViewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", ViewModel);
        }

```



3. Dodanie informacji zwrotnej do naszych formularzy. 

Można to zrobić za pośrednictwem html-helpera (w asp.net-core tag-helepra)

```csharp
        @Html.ValidationMessageFor(c=> c.Customer.Name)
```

> Tak prezentuje się kod z przykładem uzycia metody walidacyjnej

```csharp
    <div class="form-group">
        @Html.LabelFor(c => c.Customer.Name)
        @Html.TextBoxFor(c => c.Customer.Name, new { @class = "form-control" })
        @Html.ValidationMessageFor(c=> c.Customer.Name)
    </div>
```

### Data annotation

Klika z nich to:

```csharp
[Required]
[StringLength(255)]
[Range(1,10)]
[Compare("OtherProperty")]
[Phone]
[Email]
[Url]
[RegularExpression("...")]
```

Każde z tych annotacji ma domyslną wiadomość zwrotną. Działają w wiekszości przypadków, jednak jeżeli chcemy to możemy je sobie pozamieniać. 

```csharp

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
```

> Przykład zastosowania nadpisania domyślnego komunikatu zwrotnego.

### Podkręcona piłka

W naszej bazie mamy różne zasady, jedną z nich jest to ż aby mieć możliwość posiadania subskrybcji innej niż *Pay as you go* musimy mieć powyżej 18 lat. Przyjrzyjmy się zatem jak wyglądałby kod który wykonuje takie zadanie:

Wpierw tworzymy sobie nowy model który określa naszą funkcje. W moim przypadku jest to `Min18YearsIfAMember`

```csharp
 public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            if ( customer.MembershipTypeId == 0|| customer.MembershipTypeId == 1)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthday == null)
            {
                return new ValidationResult("Birthday is required");
            }

            var age = DateTime.Today.Year - customer.Birthday.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
```

musimy nadpisać metode IsValid. Metoda ta ma dwa przeciążenia, 

- pierwsze przyjmuje obiekt
- drugie przymuje również kontekst

Aby móc stosować ten atrybut, do naszego entity dodajemy:

```csharp
[Display(Name = "Date of Birth")]
[Min18YearsIfAMember]
public DateTime? Birthday { get; set;}
```

### Refaktoryzacja magicznych numerków.

W kodzie wyżej mamy

```csharp
 var customer = (Customer) validationContext.ObjectInstance;
            if ( customer.MembershipTypeId == 0|| customer.MembershipTypeId == 1)
            {
                return ValidationResult.Success;
            }
```

ale nie wiemy co oznaczają numerki 1 i 0, jeżeli nie pracowalismy z kodem. Lepszym rozwiązaniem jest nazwac te numerki jako zmienna.

Na przykład tak:

```csharp

        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYoGo= 1;

```

Powstanie nam z tego dużo czytelniejszy kod, dla osoby  zewnątrz:

```csharp
var customer = (Customer) validationContext.ObjectInstance;
            if ( customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYoGo)
            {
                return ValidationResult.Success;
            }
```

---

### jQueryvalidation

Aby wypluc podsumowanie wszystkich niedziałających pól formularza możemy użyć 

```csharp
    @Html.ValidationSummary()
```

Możemy też przeciążyć tą metodę, żeby wyświetlała jakis message kiedy jakies pola nie sa poprawnie uzupełnione. 

```csharp
@Html.ValidationSummary(true, "Please, fill all required forms")
```

### Client side validation

Mosh robi walidacje po stronie klienta za pomocą jQuery więc troche chujowo i torche 2k15. 

Nasze bundles znajdziemy w **App_Start/BundleConfig.cs**.

Możemy zobaczyc jak są wczytane w : **Views/Shared/_Layout.cshtml**

Aby samemu wczytać np jqueryval musimy w widoku który ma używać danego skryptu, zdefiniować sobie:

```csharp
@section scripts
{
    @Scripts.Render("~bundles/jqueryval")
}

```

Przy walidacji lokalnej, użytkownicy nie wysyłają pakietu informacji do serwera co pozwala znacznie zaoszczędzić resources.

Nasze atrybuty walidujące działają również na client side rendering. (Ale tylko te standardowe, naszych customowych nie zrozumie)

### AntyForgetry Token

**CSRF-** Cross0site Request Forgery

Aby się przed tym zabespieczyc używamy AntiForgeryToken przy formularzach które chcemy zabezpierczyc. 

```csharp
@Html.AntiForgeryToken();
```

Nie musimy z łapy pisać kodu do weryfikcji czy to oby na pewno użytkownik wysłał za siebie dany formularz. Zamiast tego korzystamy z atrybutu `[ValidateAntiForgeryToken]` o tak:

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewM...
                    ....
```

