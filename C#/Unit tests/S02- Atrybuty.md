# Atrybuty



### [SetUp] - Dont repeat yourself 

Podczas testowania wybranej klasy, zdarzy nam się sytuacja, ze w każdym teście tworzymy identyczny scenariusz i inicjalizujemy instancję naszej klasy, albo robimy czynność powtarzalną w sekcji **Arrange**.

W NUnitcie mamy specjalne atrybuty, żeby za każdym razem nie musieć robić takiego samego arrange. Jest to klasa `SetUp` i `TearDown`. 

```csharp
[TestFixture]
class MathTests
{
    private Math _math;

    [SetUp]
    public void SetUp()
    {
        _math = new Math();
    }

    [Test]
    public void Add_WhenCalled_ReturnSumOfArguments()
    {
        //Act
        var result = _math.Add(2, 1);

        //Assert
        Assert.That(result, Is.EqualTo(3));
    }
}
```

Jak widzimy, nasz test nie musi za każdym razem mieć w Arrangu robionego nowego obiektu, bo może mieć to w metodzie `SetUp` (nazwa dowolna ale musi zawierać atrybut setup).

W act za to odwolujemy się do naszej prywatnej zmiennej, która działa w spektrum całej klasy.

### [TestCase] - Czystrze testy

NUnit oferuje wygodny atrybut do testowania różnych argumentów tego samego testu, w tym wypadku są to `[TestCase(x,y,expectedResult)]`. Koniecznym jest wtedy dodać argumenty do metody naszego testu. 

Tak prezentuje się nasz potworek:

```csharp
[Test]
[TestCase(2,1,2)]
[TestCase(1,2,2)]
[TestCase(2,2,2)]
public void Max_WhenCalled_ReturnsGreaterArgument(int a, int b, int expectedResult)
{
    var result = _math.Max(a,b);

    Assert.That(result, Is.EqualTo(expectedResult));
}
```

> To akuat duży feature NUnit, gdyż np MSoftowy framework do pisania testów słabo działa, gdyż trzeba te *TestCase* inicjalizować w osobnym pliku, traci się wtedy dużo na czytelności kodu.

### [Ignore]- Tymczasowe ignorowanie testów

Czasem się zdarzy że test jest do poprawienia, nie działa, coś się zepsuła, trwa trzy lata, wymaga refaktoryzacji i milion innych powodów. Do tego mamy atrybut `[Ignore("Message")]`.

```csharp
[Test]
[Ignore("Because I wanted to!")]
public void Add_WhenCalled_ReturnSumOfArguments()
{
    var result = _math.Add(2, 1);
    Assert.That(result, Is.EqualTo(3));
}
```
