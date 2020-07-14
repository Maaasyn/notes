# Formularze

Formularzee tworzymy kiedy chcemy aby jakies dane z zewnatrz dochodzily do naszego serwera, na przykład chcemy przyjąć adres klienta itp.

W aspnet-core robimy to za pomoca tzw `tag-helpers`, natomiast w wersji którą obecnie studiuje, wciąż robi się to tzw. `html-helpers`.

Możemy zauważyć że metoda `Html.BeginForm` jest obiektem który jest `Disposable` więc możemy użyć dyrektywy using, aby go zamknąć. Pozatym jego dwa argumenty, `actionName` i `controllerName` informują nas, gdzie nasze dane trafią, po wysłaniu formularza. 

Kod sam się tłumaczy. Od siebie dodam że w pierwszej kolejności piszemy label, póżniej nasz faktyczny element formularza.



```csharp
@model aspnet_vidly.Models.Customer
@{
    ViewBag.Title = "New";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>New Customer</h2>

@using (Html.BeginForm("Create", "Customers")) //arg: actionName, controllerName;
{
    <div class="form-group">
        @Html.LabelFor(c => c.Name)
        @Html.TextBoxFor(c => c.Name, new { @class = "form-control" }) //arg: labda, atrybutHtml
    </div>
    <div class="form-group">
        @Html.LabelFor(c => c.Birthday)
        @Html.TextBoxFor(c => c.Birthday, new { @class = "form-control" })
    </div>
    <div class="checkbox">
        <label>
            @Html.CheckBoxFor(c => c.IsSubscribedToNewsletter) Subscribed to newsletter. //bootstrapowa skladnia jest inna dla checkboksow to tak i chuj
        </label>
    </div>
}
```

---

### Label

model / customer.cs

```csharp
[Display(Name = "Date of Birth")]
public DateTime? Birthdate {get;set;}
```

Zmieni on to. jak wyświetlane sa labele w formularzach jeżeli zdecydujemy się daną właściwość uzyć w takim celu.

Problemem tej metody jest to że za każdym razem, musimy rekompilować nasz kod.

Alternatywą dla tej metody jest wdupić label bezpośrednio w kod .cshtml na przykład tak:

```csharp
<label for="Birthday">Date of Birth</label>
@Html.TextBoxFor(c => c.Birthday, new { @class = "form-control" })
```

Z kolei tutaj pojawia się inny problem, jak zmienimy właściwość Birthday w klasie Customer to nowy label w ogóle nie będzie się na m wyświetlał. 

Nie ma tu idealnego rozwiązania. 

### Dropdown list

Aby używać w _context jakiegoś modelu, musimy go dodać do dbContext.

Okej, w tym przypadku chcemy do naszego widoku dodać kilka informacji. Pierwsza z nich jest nasz klient dla którego robimy formularz, a drugą to opcje na różnego rodzaju MembershipTypes. Aby to działało płynnie i ładnie, buduje się w takim wypadku ViewModel który uwzględnia oba te modele. 

Kiedy nie potrzebujemy korzystać z żadnych zawaansowanych funkcji listy, zamiast używać list powinniśmy użyć `Ienumerable`, umożliwia on nam iteracje po danej kolekcji, bez funcjonalności dodawania do niej nowych elementów. 

```csharp
    <div class="form-group">
        @Html.LabelFor(c => c.Customer.MembershipTypeId)
        @Html.DropDownListFor(c => c.Customer.MembershipTypeId, new SelectList(Model.MembershipTypes, "Id", "Name"), "", new { @class = "form-control" })
    </div>
```

Składnia tej metody jest super egzotyczna.

Argumenty. 

1. Do czego ma byc ten dropdown, to znaczy jaki parametr naszej klasy ma byc z nim zbindowany.
2. Lista argumentów dla tego dropdowna
   - Lista elementów
   - nazwa właściwości, która przechowuje wartość dla każdego elementu
   - nazwa właściwości która przechowuje tekst dla każdego argumentu.
3. Domyślny argument który ma zostać wyświetlony jeżeli żaden inny nie jest.
4. atrybut html który ma zostac dodany.

### Binding

Formularze **zawsze** wsyłamy postem, dlatego metoda która ma to obsłużyc musi mieć adnotacje `[HttpPost]`:

```csharp
[HttpPost]
        public ActionResult Create(Customer viewModel)
        {
            return View();
        }
```

Jak widzimy, nasza metoda przyjmuje argument `customer`, mogłaby też przyjmować argument np `NewCustomerViewModel` i z racji, że ta klasa ma w sobie właściwość `Customer`, nasza metoda Create z łatwością zbindowałaby obie potrzebne jej elementy. 

Tak  kolei wygląda działający formularz:

```csharp
@using (Html.BeginForm("Create", "Customers"))
{
    <div class="form-group">
        @Html.LabelFor(c => c.Customer.Name)
        @Html.TextBoxFor(c => c.Customer.Name, new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(c => c.Customer.Birthday)
        @*<label for="Birthday">Date of Birth</label>*@
        @Html.TextBoxFor(c => c.Customer.Birthday, new { @class = "form-control" })
    </div>
    <div class="checkbox">
        <label>
            @Html.CheckBoxFor(c => c.Customer.IsSubscribedToNewsletter) Subscribed to newsletter.
        </label>
    </div>
    <div class="form-group">
        @Html.LabelFor(c => c.Customer.MembershipTypeId)
        @Html.DropDownListFor(c => c.Customer.MembershipTypeId, new SelectList(Model.MembershipTypes, "Id", "Name"), "Choose type", new { @class = "form-control" })
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
}

```

### Saving data.

Aby zapisać dane możemy użyć do tego:

```csharp
[HttpPost]
public ActionResult Create(Customer customer)
{
	_context.Customers.Add(customer);
	_context.SaveChanges();
	return RedirectToAction("Index", "Customers");
}
```

W tym przypadku nasza metoda najpierw dodaje do naszego contextu `customera`. I tu jest haczyk.

> Dane zostają zapisane tymczasowo w pamięci, nie zostaje wysłane żadne query do bazy. Aby wysłać dane do bazy konieczne jest użycie `_context.SaveChanges()`

na samym końcu redirectujemy naszego klienta, w tym przypadku do akcji index w kontrolerze Customers. 

### Edit

Czasem zdarzają się przypadki że chcemy wyświetlić inny widok, niż ten, którego nazwa jest akcja naszego knotrolera, w takich wypadkach robimy coś takiego:

```csharp
return View("New");
```

Label textform może przyjmować przeciążenie, które pozwalanam na formatowanie tresci widniejącej w danym textboxie. W tym wypadku formatuje godzinę. 

```csharp
 @Html.TextBoxFor(c => c.Customer.Birthday, "{0:d MMM yyyy}", new { @class = "form-control" })
```

Jeden View, może służyc naraz do edytowania, jak i tworzenia nowego obiektu.

### Update

W naszym przypadku robimy to tak:

```csharp
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                TryUpdateModel(customerInDb);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

```

Jest problem z tą metodą, mimo że jest to sugerowany sposób z dokumentacji Microsoftu., gdyż zawiera w sobie wiele luk bezpieczeństwa.

Jednym z obejść jest :

```csharp
TryUpdateModel(customerInDb, "", new string[]{"Name", "Email"});
```

Problemem jest brak możliwości refaktoryzacji tego kodu ze względu na tzw. "magic strings".

Lepszym i bezpieczniejszym sposobem jest użycie:

```csharp
customerInDb.Name = customer.Name;
customerInDb.Birthday = customer.Birthday;
customerInDb.MembershipTypeId = customer.MembershipTypeId;
customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

```

Jest więcej klepania ale jest bezpieczniej. Zawsze można użyć do tego specjalnej bliblioteki jak `AutoMappper` która zrobi to za nas.

Wiec zamiast tego, nasz kod mógłby wyglądać tak:

```csharp
Mapper.Map(customer, customerInDb);
```

Ze względow bezpieczeństwa, często nie przyjmuje się jako argument bezpośrednio instancji modelu, a przyjmuje się instancje makiety modelu, która zawiera tylko niektóre składowe. 

Konwencją jest nazywac takie makiety np `UpdateCustomerDto` *(Data transfer object)*.