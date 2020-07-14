using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class HousekeeperServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IXtraMessageBox> _xtraMessageBoxMock;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IStatementGenerator> _statementGeneratorMock;
        private HousekeeperService _houseKeeperService;
        private DateTime _statementDate = new DateTime(2020, 01, 01);
        private Housekeeper _houseKeeper;
        private string _statementFilename;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            _houseKeeper = new Housekeeper()
            {
                Oid = 1,
                Email = "a",
                FullName = "b",
                StatementEmailBody = "c"
            };
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock
                .Setup(u => u.Query<Housekeeper>())
                .Returns(new List<Housekeeper>() { _houseKeeper }.AsQueryable);


            _statementFilename = "filename";
            _statementGeneratorMock = new Mock<IStatementGenerator>();
            _statementGeneratorMock
                .Setup(sg => sg.SaveStatement(
                    _houseKeeper.Oid,
                    _houseKeeper.FullName,
                    _statementDate))
                .Returns(() => _statementFilename);


            _emailSenderMock = new Mock<IEmailSender>();
            _xtraMessageBoxMock = new Mock<IXtraMessageBox>();

            //service
            _houseKeeperService = new HousekeeperService(
                _unitOfWorkMock.Object,
                _statementGeneratorMock.Object,
                _emailSenderMock.Object,
                _xtraMessageBoxMock.Object);
        }

        //Happy path :)
        [Test]
        public void SendStatementEmails_WhenCalled_ShouldGeneratesStatements()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            _statementGeneratorMock.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
        }

        //Sad path :c
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_WhenHouseKeeperEmailIsInvalid_ShouldNotGeneratesStatements(string invalidEmail)
        {
            _houseKeeper.Email = invalidEmail;

            _houseKeeperService.SendStatementEmails(_statementDate);

            _statementGeneratorMock.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        }

        //Happy path :)
        [Test]
        public void SendStatementEmails_WhenCalled_ShouldSendAnEmail()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        //Sad path :c
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileIsInvalid_ShouldNotSendAnEmail(string invalidFilename)
        {
            _statementFilename = invalidFilename;

            _houseKeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            //Arrange
            _emailSenderMock.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Throws<Exception>();

            //Act
            _houseKeeperService.SendStatementEmails(_statementDate);

            //Assert
            VerifyMessageBoxDisplayed();
        }

        private void VerifyMessageBoxDisplayed()
        {
            _xtraMessageBoxMock.Verify(mb => mb.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<MessageBoxButtons>()));
        }

        private void VerifyEmailNotSent()
        {
            _emailSenderMock.Verify(es => es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSenderMock.Verify(es =>
                es.EmailFile(
                    _houseKeeper.Email,
                    _houseKeeper.StatementEmailBody,
                    _statementFilename,
                    It.IsAny<string>()));
        }
    }
}
