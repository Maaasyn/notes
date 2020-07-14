# Action parameters

Dane do kontrolera mogą być wysłane na trzy sposoby.

- Wbudowane w URL:

  ```
  strona.com/movies/edit/1
  ```

  

* Jako zapytanie w URL:

  ```
  strona.com/movies/edit?id=1
  ```

* Jako dane w formularzu

  ```
  id = 1
  ```

  

  

W pliku `RouteConfig.CS` znajduje sie opis jak działa nasz routing, to znaczy na przykład że jako trzeci forward-slash w naszym URL możemy użyć opcjonalnego parametru id.

`String` w csharpie jest nullable *z urzędu.* wieć nie musimy dawać przy nim znaku `?`. 

Przykład kodu z lekcji

```csharp
public ActionResult Index(int? pageIndex, string sortBy)
        {
            pageIndex = pageIndex ?? 1;
            sortBy = sortBy ?? "Name";
            return Content($"{pageIndex} a sortBy to {sortBy}");
        }
```

