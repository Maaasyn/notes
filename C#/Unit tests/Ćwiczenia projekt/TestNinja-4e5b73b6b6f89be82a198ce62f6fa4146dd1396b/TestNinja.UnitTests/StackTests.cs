using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class StackTests
    {
        //private Fundamentals.Stack<string> _stack;
        //[SetUp]
        //public void StartUp()
        //{
        //    var _stack = new Stack<string>();
        //}

        [Test]
        public void Count_EmptyStack_Returns0()
        {
            var _stack = new Stack<string>();

            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_ValidArg_AddedTheObjectToStack()
        {
            var _stack = new Stack<string>();
            _stack.Push("test");

            Assert.That(_stack.Count, Is.EqualTo(1));
            Assert.That(_stack.Peek(), Does.Contain("test"));
        }

        [Test]
        public void Push_ArgIsNull_ThrowsArgumentNullException()
        {
            var _stack = new Stack<string>();
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Pop_InvalidOperation_InvalidOperationException()
        {
            var _stack = new Stack<string>();

            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_PoppingLastObject_ReturnsLastObject()
        {
            //Arrange
            var _stack = new Stack<string>();
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Pop();

            Assert.That(result,Does.Contain("c"));
        }

        [Test]
        public void Pop_PoppingLastObject_RemovesObjFromTheStack()
        {
            //Arrange
            var _stack = new Stack<string>();
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            //Act
            var result = _stack.Pop();

            //Assert
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_StackLengthIs0_ThrowsInvalidOperationException()
        {
            var _stack = new Stack<string>();

            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_PeekingStackWithContent_ReturnsTheItemOnTopOfTheStack()
        {
            //Arrange
            var _stack = new Stack<string>();
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            //Act
            var result = _stack.Peek();

            //Assert
            Assert.That(result, Is.EqualTo("c"));
        }


        [Test]
        public void Peek_PeekingStackWithContent_DoesNotRemoveAnyItem()
        {
            //Arrange
            var _stack = new Stack<string>();
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            //Act
            var result = _stack.Peek();

            //Assert
            Assert.That(_stack.Count, Is.EqualTo(3));
        }

    }
}
