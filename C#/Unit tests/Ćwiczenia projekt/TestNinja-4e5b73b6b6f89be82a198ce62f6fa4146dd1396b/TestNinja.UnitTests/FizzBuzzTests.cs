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
    class FizzBuzzTests
    {
        //private FizzBuzz _fizzBuzz;
        //[SetUp]
        //public void SetUp()
        //{
        //    _fizzBuzz = new FizzBuzz();
        //}

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
    }
}
