using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
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

        [Test]
        public void DownloadInstaller_OnSuccessfulConnection_ReturnsTrue()
        {
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            //assert
            Assert.That(result, Is.True);
        }
    }
}
