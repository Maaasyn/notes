# Ćwiczenia

1. Sprawdzić funkcje fizzBuzz

```csharp
[Test]
        public void GetOutput_GetsNumberDividedByOnlyThree_ReturnsFizz()
        {
            var result = TestNinja.Fundamentals.FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_GetsNumberDividedByOnlyFive_ReturnsBuzz()
        {
            var result = TestNinja.Fundamentals.FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_GetsNumberDividedByThreeAndFive_ReturnsFizzBuzz()
        {
            var result = TestNinja.Fundamentals.FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        [Test]
        public void GetOutput_GetsNumberNotDivisibleBy5or3_ReturnsBuzz()
        {
            var result = TestNinja.Fundamentals.FizzBuzz.GetOutput(2);

            Assert.That(result, Is.EqualTo("2"));
        }
```

2. Zrobic test dla punktów karnych.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new DemeritPointsCalculator();
        }
        [Test]
        [TestCase(0,0)]
        [TestCase(25,0)]
        [TestCase(65,0)]
        [TestCase(66,0)]
        [TestCase(70,1)]
        [TestCase(75,2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnsDemeritPoints(int speed, int expectedResult)
        {
            var result = _calculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_InputOutOfRange_ThrowsArgumentOutOfRangeExpectation(int speed)
        {
            Assert.That(() => _calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
```

 

3. Testy dla klasy stack

```csharp
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

```

