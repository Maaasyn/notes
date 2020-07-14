# State-base vs Interaction testing

Kiedy testujemy czy nasze metody zwracają odpowiednia wartość albo ustawiają właściwy stan obiektu, nazywamy je wtedy **State-Base**.

Czasem jednak musimy sprawdzić czy klasa którą testujemy wchodzi z inną klasą w interakcje w odpowiedni sposób. Nazywamy to wtedy **Interaction Test**.

Dla przykładu

```csharp
public class OrderService
{
    public void PlaceOrder (Order order)
    {
        _storage.Storage(order);
        ...
    }
}
```

W przedstawionej wyżej metodzie widzimy że metoda `PlaceOrder` nakazuje obiektowi `_storage` użycie metody, nie interesuje nas natomiast, gdzie, co i jak zostanie zapisane, tylko czy zapis miał miejsce, czy oczekiwana interakcja się udała, czy nie został wyrzucony nieodpowiedni wyjątek itd. 

Testy mają testować zewnętrzne zachowanie, a nie implemendacje. 

### Testing Interaction between Two Objects

```csharp
 [TestFixture]
    class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            //Arrange
            var storageMock = new Mock<IStorage>();
            var orderService = new OrderService(storageMock.Object);
            
            //Act
            var order = new  Order();
            orderService.PlaceOrder(order);

            //Assert
            storageMock.Verify(st => st.Store(order));
        }
```

> Testujemy w tym przykładzie czy odpowiednia metoda została wezwana na naszym obiekcie.

Zaleca się używania moków tylko na zewnętrznych zasobach.

### Kto pisze testy

Software developer. Unit testy powinno się pisać, mimo że boli i nie jest to zadanie dla testerów. 

* Unit Testy - Developer
* Integration Testy - Developer
* E2E Testy - Tester

