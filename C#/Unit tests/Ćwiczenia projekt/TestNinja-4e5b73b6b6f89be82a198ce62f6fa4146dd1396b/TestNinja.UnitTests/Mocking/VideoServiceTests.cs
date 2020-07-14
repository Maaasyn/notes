using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _fileReaderMock;
        private Mock<IVideoRepository> _videoRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _videoRepositoryMock = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReaderMock.Object, _videoRepositoryMock.Object);
        }


        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReaderMock.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
        {
            _videoRepositoryMock.Setup(v => v.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideos_ReturnsStringWithIdOfUnprocessedVideos()
        {
            _videoRepositoryMock.Setup(v => v.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video(){Id = 1},
                new Video(){Id = 2},
                new Video(){Id = 3},
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
