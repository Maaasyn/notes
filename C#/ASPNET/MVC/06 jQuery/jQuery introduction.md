# jQuery

No także ten. Troche archaizm, raczej legacy code. W jQuery było okej kiedy był 2012 i przeglądarki nie były ustandaryzowane i nie istniało jendo wygodne narzędzie do tego, aby na wszystkich przeglądarkach wyświatlała się wersja kodu jaką byśmy sobie życzyli. 

Obecnie wszycy odchodza od starego, poczciwego jQuery na rzecz. chociażby poczciwego js, który w ostatnich latach dostał bardzo dużo super funkcjonalności, które wcześniej były zapewniane przez jQuery, a których trudność manualnej implemendacji była wysoka. 

Ponizej umieszczam kod, który jest jquerowy. Miłego czytania szambonurku.

```csharp
@section scripts
{
    <script>
        $(document).ready(function() {
            $("#customers .js-delete").on("click", function () {

                var button = $(this);

                if (confirm("Are you sure you want to delete this customer?")) {
                    $.ajax({
                        url: "/api/customers/" + button.attr("data-customer-id"),
                        method: "DELETE",
                        success: function () {
                            button.parents("tr").remove();
                        }
                    })
                }
            })
        })
    </script>
}

```

### Bootbox

Bootbox t oabstrakcja nad bootstrapem. 

Aby go dodać, robimy w `pmc`:

```
install-package bootbox -version:4.3.0
```

> W tym wypadku używamy tej wersji bo mosh jej używa i chuj.

Optymalizacja naszego jsa i dodanie bootboxa:

```csharp

@section scripts
{
    <script>
        $(document).ready(function() {
            $("#customers").on("click",". .js-delete", function () {
                var button = $(this);

                bootbox.confirm("Are you sure you want to delete this customer?", function() {
                    if (result) {
                        $.ajax({
                            url: "/api/customers/" + button.attr("data-customer-id"),
                            method: "DELETE",
                            success: function () {
                                button.parents("tr").remove();
                            }
                        })
    
                    }
                })
            })
        })
    </script>
}

```

### Jquery.datatables

Kolejna biblioteka, tym razem do tworzenia tablic.

```
PM> install-package jquery.datatables -version:1.10.11
```

Przy okazji mosh tłumaczy jak opiekować się bibliotekami jsowymi i jak wiązać je.

> Zamiast:

```csharp
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required: false)
```

> To:

```csharp
    @Scripts.Render("~/bundles/lib")
```

> I to:

```csharp
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/respond.js"));
```

> Działające

```csharp
@section scripts
{
    <script>
        $(document).ready(function () {
            $("#customers .js-delete").on("click",
                function () {
                    var button = $(this);

                    if (confirm("Are you sure")) {
                        $.ajax({
                            url: "/api/customers/" + button.attr("data-customer-id"),
                            method: "DELETE",
                            success: function () {
                                button.parents("tr").remove();
                            }
                        });
                    }
                })
        });
    </script>

```

Po tym jak dodaliśmy `bootboxa`

```csharp
@section scripts
{
    <script>
        $(document).ready(function() {
            $("#customers .js-delete").on("click",
                function() {
                    var button = $(this);

                    bootbox.confirm("are u sure?",
                        function(result) {
                            if (result) {
                                $.ajax({
                                    url: "/api/customers/" + button.attr("data-customer-id"),
                                    method: "DELETE",
                                    success: function() {
                                        button.parents("tr").remove();
                                    }
                                });
                            }
                        })
                })
        });
    </script>
}
```

Do sprawnego działania naszej biblioteki tablicowej wymagane jest, aby <table> był odpowiednio zdefiniowany, w innym wypadku pojawi nam się błąd. Masa błędów. Jebana lawina gówna.

```csharp

@section scripts
{
    <script>
        $(document).ready(function () {
            $("#customers").DataTable();

            $("#customers").on("click", ".js-delete",
                function () {
                    var button = $(this);

                    bootbox.confirm("are u sure?",
                        function (result) {
                            if (result) {
                                $.ajax({
                                    url: "/api/customers/" + button.attr("data-customer-id"),
                                    method: "DELETE",
                                    success: function () {
                                        button.parents("tr").remove();
                                    }
                                });
                            }
                        })
                })
        });
    </script>
}

```

