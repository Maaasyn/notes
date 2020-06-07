##  Entity Framework



Uzywamy tego frameworku żeby łatwo odpytywać baze danych. Ma w sobie dużo metod które z automatu zamykają strumień do bazy, i mapuje nasze obiekty na entities z bazy.



### Workflows

W EF mamy 3 sposoby pracy z baza.

`Database first` najpierw sobie robimy baze danych, a potem generujemy na jej podstawie clasy, bazując na bazie. 

`Code first` najpierw tworzymy sobie klasy, potem one są przetwarzane na tabele w bazie.

`Model first`  w tej metodzie budujemy sobie nasze encje z pomocą UML diagramu, i bazując na tym diagramie budowane są tabele i clasy.

### Database first

Uwaga, żeby to było w þełni sprawne to musimy pracowanić na netframeworku, nie na wersji corowej.

Aby zainstalowac `EF` w naszym projekcie, najpierw w nuget menagerze, pobieramy `EF`. 

```
install-package EntityFramework
```

Do naszego projektu dodajemy `Ado.NET Entity Data Model` i on zmapuje nam wszystko. 

 Po przeklikaniu i podłączeniu się do bazy widzimy przed sobą wygenerowany kod, z postfixem `edmx`. (edit data model X)

Gdy rozwiniemy sobie ten plik, widzimy w nim pliki z rozszerzeniem `tt`. 

`tt` oznacza tu T4 template. Jest to sposob generwania kodu przy uzyciu szablonu. 

W wynerowanym kodzie możemy zobaczyć że stworzyło nam clase która dziedziczy po `DbContext`. <- Jest ona wymagana do działania tego frameworku.



```csharp
var context = new DatabaseFirstWorkflowEntities();
            var post = new Post()
            {
                Body = "Body",
                DatePublished = DateTime.Now,
                PostId = 1,
                Title = "Title"
            };

context.Post.Add(post);
context.SaveChanges();
```

 

W powyższym kodzie widzimy że najpierw tworzona jest zmienna context. W ramie trzymane są wartości, i EF sprytnie tymi danymi operuje. Potem widzimy, że nasza tablica Post jest polem naszej klasy, i możemy wywołać na niej metode `Add` która przyjmmuje jako argument model. Narazie wszystko jest w ramie. Dopiero jak wywołamy metode `SaveChanges` zmiana jest 'Querowana'. 



### Code first

W kode first wpierwej klepiemy kod, a z tego generowany jest nasz sql.

```csharp
namespace Demo_Code_first
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime DatePublished { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class BlogDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}
```

Nastepnie w pliku `App.config` Musimy dodać np takie coś: 

```xml
<connectionStrings>
    <add name="BlogDbContext" connectionString="data source=localhost; initial catalog=CodeFirstDemo; integrated security=SSPI" providerName="System.Data.SqlClient"/>
  </connectionStrings>
```

potem, w nugmenagerze 

```
enable-migrations
```

raz na cały projekt odpalamy ten skrypt ^^.

#### Najgorsze za nami.

Teraz już normalnie możemy z łatwością dodawać nasz kod, i będzie on generowany na sql. Na przykład zainicjowaliśmy sobie nasze modele. Należy wtedy w nugmenagerze wpisać:

```
add-migration <NazwaMigracji>
```

i od ręki mamy w naszym projekcie szybciutko wygenerowany właściwy kod;

Gdy dokonujemy wewnętrznej miany w naszym modelu, albo dodajemy jakiś kolejny, to wykonujemy powyższą komende ponownie.

Koniecznym będzie też dodatnie nowego modelu do naszego `DbContext`.

Kiedy finalnie chcemy wrzucić naszą migrację na serwer SQLowy, wklepujemy w konsoli:

```bash
Update-Database
```

### CodeFirst > DBFirst

Zalety 

- Łatwiej wrócić do poprzedniej wersji.
- Compatybilne z innymi bazami, łatwiej będzie wygenerować sql na tamte bazy.
