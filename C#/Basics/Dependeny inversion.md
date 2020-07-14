# Odwracanie zależności

Wyobraźmy sobie sytuacje, w której chcemy dodać do naszej listy jakiś obiekt. Powiedzmt że do listy wyników strzału z łuku dopisujemy 10.

W takim wypadku nasza lista wyników dodaje do siebie samej 10.

Ale my chcemy dodać 10, do naszej listy. Więc zamiast

```csharp
var listaWyników = List<int>();

listaWyników.Add(10);
```

Chcemy uzyskać np.

```csharp
10.AddToScoreList(listawyników);
```

W takim wypadku możemy się posłużyć taką konstrukcją:

```csharp
public static class ExtentionMethod
{
    public static T AddTo<T>(this T self, params List<T>[] parametry)
    {
        foreach (var list in parametry)
        {
            list.Add(self);
        }
        return self;   
    }
}
    class Program
{
    static void Main(string[] args)
    {
        List<int> listaLiczb1 = new List<int>();
        List<int> listaLiczb2 = new List<int>();
        List<int> listaLiczb3 = new List<int>();

        10.AddTo(listaLiczb1,listaLiczb2);


        Console.WriteLine("Hello World!");
    }
}
```

