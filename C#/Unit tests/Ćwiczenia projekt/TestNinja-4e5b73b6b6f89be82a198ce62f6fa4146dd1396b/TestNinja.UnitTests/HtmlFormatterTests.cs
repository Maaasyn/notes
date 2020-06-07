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
    class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBoldnazwa_WhenCalled_ShouldEnclosedStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("abc");

            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

        }
    }
}
