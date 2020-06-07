# Ćwiczenia

1. Zrobić testy dla metod w VideoService.cs

> Problemy

* Ta klasa miała w sobie klase VideoContext która działała na EntityFramework, aby ekstraktować taką zależność, używa się repository pattern. (Mosh przedstawił to w swoim kursie do Entity Framework).

Tak wygląda takie Repository

```csharp
namespace TestNinja.Mocking
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }

    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            using (var context = new VideoContext())
            {
                var videos =
                    (from video in context.Videos
                     where !video.IsProcessed
                     select video).ToList();

                return videos;
            }
        }
    }
}
```

Kiedy nasz konstruktor do dependency injection przyjmuje za duzo argumentów, coś możliwe że robimy źle. Jeżeli w naszej klasie tylko jedna metoda implementuje potrzebną nam zależność, warto ją wtedy wstrzyknąć jako argument metody.

2. Zrobić unitesty do DownloadInstaller.cs

> poza ekstrakcja dodatkowej klasy, z czym sobie poradziłem, problemem okazał się brak interfejsu IWebService, i musiałem na sztywno zakodowac Webservice. 

> Framework Moq działa tak, że kiedy je programujemy w Setup, ich zachowanie działa dla dokładnie tych argumentów, podanych w metodzie, czasem musimy zrobić tak:

```csharp
[TestFixture]
    class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloaderMock;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            //arrange
            _fileDownloaderMock = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloaderMock.Object);

        }

        [Test]
        public void DownloadInstaller_OnError_ReturnsFalse()
        {

            //act
            _fileDownloaderMock.Setup(f => f.DownloadFile(It.IsAny<string>(),
                    It.IsAny<string>()))
                .Throws<WebException>();
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            //assert
            Assert.That(result, Is.False);
        }
    }
```

 

3. Employee Tests

> Repository pattern,  Asertowanie zwracanych typów, wstrzykiwanie zależności, nie sprawdzanie prywatnych metod.