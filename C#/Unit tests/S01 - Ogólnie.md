# Testy



### Testy automatyczne

Testy automatyczne to nic innego niż skrypty, które ktoś piszę, a później je włącza, zamiast manualnie sprawdzać czy coś się wykonało, albo przeklikiwać interfejs aplikacji nad którą się pracuje (projekt). Również testy automatyczne praktyka pisania kodu i jednocześnie testów. Cechą takich testów jest ich powtarzalność i to że są dużo szybsze niż testy manualne. 

Dzięki testom łatwiej jest później refaktoryzować kod (zmieniać nazwy, albo zachowanie naszych funkcji), bo możemy sobie sprawdzić czy dana zmiana nie wysypała nam kodu w innym miejscu.

### Podział Testów

Rozróżniamy w ogólnie przyjętej kryterii trzy rodzaje testów: 

* `Unit test-` Testuje naszą aplikację bez zewnętrznych zależności takich jak łączenia z bazą, pliki itp. Są tanie i szybkie do napisania i uruchomienia. Wadą jest że nie dają pewności 100%.

* `Integration Test`-  Testuje aplikacje z jej extenal dependencies. Czyli z bazami itp. Dają więcej pewności. Zwykle też testuje się pare klas naraz.

* `E2E endtoend Test`- Testy finalne. Z user interface. <u>Selenial</u> to framework do takick do takich testów..

Każdy z tych testów posiada swoje narzędzia którymi się takie testy wykonuje.

Unit testów powinno być **najwięcej**, później Integration a na końcu E2E tests.

### Unit testy

Unit testy powinny mieć jedną odpowiedzialnośc i być krótkie, raczej nie większe jak 10 linijek.

Testy **nie** powinny :

* mieć logiki takiej jak  `if` , `else` ` foreach` itd. 
* znać stanu innego testu.
* być zbyt generyczne. 

### Dobry test

Unit testy to first-class citizents. Wszystkie dobre praktyki programowania, powinny być zawarte w dobrze napisanym teście.

### Co testować?

Niemalże wszystko. Trudno się testuje źle zakonstruowany kod. 

Testy powinny testować `output` jakieś funkcji.

W programowaniu mamy dwa rodzaje funkcji, queries i command. 

- Queries zawacaja wartość. W ich sprawdzamy wszystkie zwracane drogi. 
- Commands zwracają akcje, takie jak calling a webserwerm comunikacja z bazą albo zmiana jaieś właściwości w jakimś obiekcie. Testuje się w takich przypadkach outputy i czy nowy obiekt jest opdowiednio zmieniony. A jak mamy jakies exteral depedieces to musimy to sprawdzic.

### Nie sprawdzamy 

Funkcji języka, 3rd parties code, bo zakładamy z góry że wszystkie działają. (chyba że są tak problematyczne jak mapper)

### Test

Metody w naszym teście powinny być publiczne, oraz nie powinny nic zwracać, mają się tylko wykonać. (Wszystkie metody testu są `public void`)

Nasze testy to nasza darmowa dokumentacja bo opisują nad dokładnie jak działa nasz program.

Test składa się z trzech części.

1. **Arrange**- W tej sekcji inicjalizujemy nasze obiekty które będziemy testować.
2. **Act**- tutaj jest nasza akcja, czyli po prostu wzywamy jakąś metodę naszego obiektu.
3. **Assert**- Sprawdzamy czy metoda wykonuje się poprawnie (czy zwraca odpowiednią wartość, czy wchodzi w interakcje ze swoimi polami itp).

### Trustworthy tests

Testy powinny być godne zaufania. 

Żeby takie pisać dobrze jest zacząć np od **Test-driven Development** bo piszemy nieprzechodzący test, i piszemy produktcyjny kod aż przejdzie testy. 

Mozna też komentować linie produkcyjnego kodu, i sprawdzać czy po zmianie warunków nasz test dalej jest przechodzony, jeżeli tak, możliwe że coś jest źle z naszym kodem produkcyjnym.

### Narzędzia/ Frameworki do Unit testów w C#

1. **NUnit**- Stary i jary, dobrze wspierany przez community, najpopularniejszy
2. **MSTest** - majkrosoftowy
3. **XUnit** - Rosnąca poularność

Każdy framework daje nam test runnera i biblioteki pomocnicze.

### Test Driven Development

Jest to sposób wytwarzania oprogramowania, silnie zorientowany na tworzeniu testów. Ten sposób jest dość lubiany gdyż mimo jego czasochłonności, pozwala nam mieć testy, i wszystkie komponenety naszego projektu przetestowane.

Podczas Test Driven Development zwykle:

1. Piszemy najpierw test.
2. Piszemy najprostsze rozwiązanie
3. Refaktoryzuj w miare potrzeby. 

### Konwencje

Dla każdego projektu powinniśmy mieć projekt testowy. Konwencją jest że dla projektu:

`Projekt` powinniśmy mieć `Projekt.UnitTest`.

Zanim pushniemy nasz kod do repozytorium, powinniśmy sprawdzić czy nam kod przechodzi testy, zwykle w tym celu używa się specjalnych narzędzi, można też ustawić naszego githuba aby podczas pulla i/ bądź pusha wykonywał wybrane przez nas akcje, na przykład testy jednostkowe.

W projekcie z testami jednostkowymi powinniśmy mieć lustrzane odbicia testowanych przez nas klas. Dla przykładu:

`Reservation` powinniśmy mieć `ReservationTests`.

Dobrą konwencją jest nazywanie metod w naszym teście:

```csharp
MethodName_Scenario_ExpectedBehaviour(){}
NazwaMetody_Scenariusz_OczekiwaneZachowanie(){}
```

czyli dla przykładu:

```csharp
[TestMethod]
public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
{
}

[TestMethod]
public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
{
}
```

W każdym teście powinny być osobne Arrange, nie powinniśmy na początku klasy testującej definiować prywatnego pola instancji klasy którą chcemy używać, gdyż mogłoby to zaburzyć działanie testów.

Frameworki UnitTestowe zawsze patrzą na atrybuty dołączone do naszeg kodu, o atrybutach jest szerzej w następnej sekcji.

### NUnit

Najpopularniejszy framework do testów jednostkowych w C#.

Zamiast atrybutów `TestClass`  i `TestMethod` używamy atrybutow `TestFixture` i `Test`.

W sekcji **Assert** możemy upewniać się na **trzy** sposoby:

```csharp
Assert.IsTrue(result);
Assert.That(result, Is.True);
Assert.That(result == true);

//Rekomendowany
Assert.That(result, Is.True);
```

