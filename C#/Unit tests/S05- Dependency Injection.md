# Dependency Injection

W dependency Injection staramy się wszystkie zależności wtrzyknąć do naszej klasy, jako interfejsy, żeby mieć loosly coupled application, which makes our app testable. 

Zależności możemy wstrzykiwać na trzy sposoby:

1. Przez argumenty metod (by methods paramethers)
2. Przez właściwości
3. Przez konstruktory

Kiedy robimy testy, ważnym jest aby później zamiast właściwej zależności, wrzucić podrabianą, sztuczną, fejkową. Konwencją jest nazywać taka klasę Mock, Stub albo Fake (chociaz Mosh w ogóle ich nie nazywał).



### Przez argumenty metod

Problemem wstrzykiwania zależności poprzez argument metody, jest to że dana metoda może być użyta wielokrotnie, w danym segmencie kodu co może powodować że trzeba będzie się napisać i może to spowodować więcej problemów.

Niektóre Dependency Injection framework nie radzą sobie ze wstrzykiwaniem zależności przez metody.

Tak wygląda dobrze skonstruowana klasa zależna od interfacu.

```csharp
 public interface IFileReader
    {
        string Read(string path);
    }

    class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
```

A to nasza Mockowa implemendacja na potrzebe testu

```csharp
class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
```

A to jej wstrzyknięcie

```csharp
public string ReadVideoTitle(IFileReader fileReader)
{
    var str = fileReader.Read("video.txt");
    var video = JsonConvert.DeserializeObject<Video>(str);
    if (video == null)
        return "Error parsing the video.";
    return video.Title;
}
```

### Przez właściwość

```csharp
public IFileReader FileReader { get; set; }

public VideoService()
{
    FileReader = new FileReader();
}

public string ReadVideoTitle()
{
    var str = FileReader.Read("video.txt");
    var video = JsonConvert.DeserializeObject<Video>(str);
    if (video == null)
        return "Error parsing the video.";
    return video.Title;
}

```

> test

```csharp
[TestFixture]
    class VideoServiceTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var service = new VideoService();
            service.FileReader = new FakeFileReader();

            var result = service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
```

### Przez konstruktor

To chyba najpopularniejsza metoda. 

```csharp
 private IFileReader _fileReader;

        public VideoService(IFileReader FileReader)
        {
            _fileReader = FileReader;
        }

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }
```

Problemem w tym wypadku będzie to że zmieniliśmy sygnature naszego konstruktora, przez co gdzieniegdzie nasz kod sie wysypie.

Żeby tego nie wysypać, można, lecz nie powinno się użyć: 

> uwaga. Jest to tak zwany Poor man dependency injection (nie robi się tak normalnie)

```csharp
private IFileReader _fileReader;

        public VideoService(IFileReader fileReader = null)
        {
            _fileReader = fileReader ?? new FileReader();
        }

```

Przez co mamy dwie pieczenie na jednym ogniu. Nie rozwaliliśmy zależności, i mamy wmiare loosly coupled application.

Wtedy w faktycznej implemendacji naszej metody nie musimy podawać koniecznej dependency (lecz możemy i powiniśmy) a w teście podajemy.

> Test

```csharp
[Test]
public void ReadVideoTitle_EmptyFile_ReturnError()
{
    var service = new VideoService(new FakeFileReader());
    var result = service.ReadVideoTitle();

    Assert.That(result, Does.Contain("error").IgnoreCase);
}
```



### Dependency Injection Frameworks

Dzieki frameworkom omijamy ten **poor man dependency injection**. 

Framework taki weźmie na siebie tworzenie i inicjalizowanie obiektów w run time.

Dlatego zamiast tego:

```csharp
private IFileReader _fileReader;

public VideoService(IFileReader fileReader = null)
{
    _fileReader = fileReader ?? new FileReader();
}
```

mamy po prostu to:

```csharp
private IFileReader _fileReader;

public VideoService(IFileReader fileReader)
{
    _fileReader = fileReader
}
```

Popularne frameworki do **DI** to:

* **NInject**
* StructureMap
* Spring.NET
* **Autofac**
* **Unity**
* i inne

W każdym z nich z grubsza chodzi o to samo.

> asp net core w wersji 3.0+ posiada wbudowany DI framework który jest, i powinien być defaultowo używany dla nowych projektów.

Mamy w nich `kontener`, Ten kontener to registry, w którym znajdują się nasze interfejsy i  ich implementacje.

Kiedy nasza aplikacja jest uruchamiana, nasz framework z automatu zadba aby tworzyć obiect-graphs bazując na interfejsach i typach w kontenerze. 

### Mocking Framework

Okej, powodem istnienia Mockowych frameworków jest potrzeba robienia fejkowych klas które implemendują interfejsy, w sposób zautomatyzowany, bez powielania kodu. MockFrameworki to abstrakcja nad klepaniem z łapy po sto bardzo podobnych do siebie fejkowych klas, która pozwala nam znacznie skrócić czas pisania kodu.

Kilka z nich to: 

- **Moq**
- NSubstitute
- FakeItEasy
- Rhino Mocks

Moq trzeba pobrać, jest to nuget. Ma troche śmieszną składnie, i dobrą dokumentację.

```csharp
[Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock.Setup(fr => fr.Read("video.txt")).Returns("");

            var service = new VideoService(fileReaderMock.Object);
            var result = service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
```

> test zawierający implemendacje moqów.

Ze względu na ich wage i koszt (sa resourcesożerne), stosujemy je dla zewnętrznych zależności.

A tak wygląda cały test tylko ładnie podany:

```csharp
[TestFixture]
    class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _fileReaderMock;

        [SetUp]
        public void SetUp()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReaderMock.Object);
        }


        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReaderMock.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
```

