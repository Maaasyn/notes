# Asserts.

Różne sytuacje wymagają różnych rozwiązań. Czasem chcemy sprawdzić, czy metoda naszej klasy zwraca określoną warość, czasem chcemy upewnić się że metoda naszej klasy wchodzi w interakcje z jej zewnętrzną zależnością (wstrzykniętą), możemy również chcieć sprawdzić czy dla danego argumentu podanego w naszej metodzie, zwracany jest odpowiedzi wyjątek. Ta sekcja będzie to rozświetlać.



### Testowanie zwracanych warości

Zwykle nasze metody zwracają określoną warość, wtedy używamy najprostrzych assertów jak:

```csharp
//Arrange
var calc = new Calculator();
//Act
var result = calc.Add(2,2);
//Assert
Assert.That(result, Is.EqualTo(4));
```

Klasa statyczna `Is` posiada kilka opcji, na większość podstawowych assertów sprawdzających zwracaną warość.

### Testowanie stringów

Mamy różne opcje

```csharp
//Specific
Assert.That(result, Is.EqualTo("<strong>abc</strong>"));
```

Czasem nie zależy nam żeby zwracana wartość byla case-sensitive, wtedy:

```csharp
Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);
```

Precyzyjne Asserty warto zostawić tam, gdzie jesteśmy pewni że dany ciąg się nie zmieni, nie zostanie zrefaktoryzowany, bezpieczniejszą opcją będzie:

bardziej generalnie:

```csharp
//General
Assert.That(result, Does.StartWith("<strong>"));
Assert.That(result, Does.EndWith("</strong>"));
Assert.That(result, Does.Contain("abc"));
```

### Testowanie kolekcji

Przy testowaniu metody zwracającej kolekcje mamy pare opcji, od najbardziej generycznych do najbardziej specyficznych. 

```csharp
//Najbardziej generalne
Assert.That(result, Is.Not.Empty);

//Bardziej szczególowe
Assert.That(result.Count(), Is.EqualTo(3));

//Sprawdzamy czy zawiera, ale nie liczy się dla nas kolejność
Assert.That(result, Does.Contain(1));
Assert.That(result, Does.Contain(3));
Assert.That(result, Does.Contain(5));

//To samo co wyzej ale lepiej
Assert.That(result, Is.EquivalentTo(new [] {1,3,5}));
```

Dodatkowe fajne opcje:

```csharp
Assert.That(result, Is.Ordered);
Assert.That(result, Is.Unique);
```

Przy testach kolekcji trzeba szukac wlasciwego balansu miedzy testami ktore sa zbyt generyczne a abyt specyficzne.



### Testowanie typów zwracanych przez metody

Sa dwie metody sprawdzania tego.

```csharp
Assert.That(result, Is.TypeOf<NotFound>());
```

Ta metoda działa w większości przypadków. 

albo

```csharp
Assert.That(result, Is.InstanceOf<NotFound>());
```

Różnica jest taka, że w `InstanceOf` dany zwracany obiekt może być podanym obiektem jak i obiektem dziedziczonym po klasie tego obiektu, kiedy `TypeOf` sprawdza czy dokladnie jest tą klasą.

### Testowanie void methods

Generalnie metody void są tzw commans, nic nie zwracają, za to np komunikują się z bazą, logują coś, zmieniają stan jakiegoś obiektu itd. 

```csharp
Assert.That(errorLogger.LastError, Is.EqualTo("test"));
```

### Testowanie metod wyrzucających wyjątki

Znów dwie opcje. 

```csharp
Assert.That(() => logger.Log(error), Throws.ArgumentNullException);

//lub
Assert.That(() => logger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>());
```

Gdzie w drugim przypadku możemy być bardziej szczególowi do naszych exceptions.

Tak wygląda cały kod

```csharp
[Test]
[TestCase(null)]
[TestCase("")]
[TestCase(" ")]
public void Log_InvalidErrorMessage_ThrowArgumentNullException(string error)
        {
            var logger = new ErrorLogger();

Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
        }
```



### Metody które raise an event

Aby przetestować metode która wznosi event, musimy subskrybować do tego eventu w swojej metodzie testowej przed fazą *Act*.

W swoim **eventHandlerze**  bedziemy mieli jakąś wartość, więc warto ją przypisać do jakieś zmiennej lokalnej. 

```csharp
[Test]
        public void Log_validError_raiseErrorLoggedEvent()
        {
            var logger = new ErrorLogger();
            var id = Guid.Empty;
            logger.ErrorLogged += (sender, args) => { id = args; };
            logger.Log("a");
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
```

### Testowanie prywatnych i protected metod

Metod prywatnych nie testujemy. Można metody prywatne przyrównać do odtwarzacza dvd, guziczki z zewnatrz to nasze api, implemendation detail jest płytą główną.

### Kiedy jest wystarczająca ilość testów

Mamy do tego tzw: `Code Covarage Tools`. 

* Visual Studio Enterprise Edition
* ReSharper Ultimate
  * DotCover (to unit tester i covarage tool w jednym)
* NCover

### Real-life scenario

* Legacy code. Ciężko je testować, może to być gra niewarta świeczki. 

* Startup. Może nie być czasu wszystko przetestować, ale warto testować najważniejsze elementy aplikacji. 
* Kiedy jesteś jedyną osobą która ich chce i uważa je za potrzebne. 