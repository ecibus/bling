using System;
using Bling.Domain.Extension;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain.Extension
{
    [TestFixture]
    public class StringExtensionTest
    {
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_remove_html_tag_in_string()
        {
            string text = "hello<some tag> wo<tag>rld<br/>";

            Assert.That(text.RemoveHTMLTag(), Is.EqualTo("hello world"));
        }

        [Test]
        public void Should_be_able_to_convert_string_to_integer()
        {
            Assert.That("7".ToInteger(), Is.EqualTo(7));
        }

        [Test]
        public void Should_be_able_to_capitalize_first_letter()
        {
            Assert.That("TEST".Capitalize(), Is.EqualTo("Test"));
            Assert.That("test".Capitalize(), Is.EqualTo("Test"));
        }

        [Test]
        public void Should_be_able_to_convert_string_to_boolean()
        {
            Assert.That("1".ToBoolean(), Is.EqualTo(true));
            Assert.That("0".ToBoolean(), Is.EqualTo(false));
        }

        [Test]
        public void Should_be_able_to_convert_string_to_datetime()
        {
            DateTime date = new DateTime(2009, 1, 1);

            Assert.That("1/1/2009".ToDateTime(), Is.EqualTo(date));
        }

        [Test]
        public void Escape_SingleQuote_ToSlashSingleQuote()
        {
            string s = "'";
            Assert.That(s.Escape(), Is.EqualTo(@"\'"));
        }

        [Test]
        public void Escape_Slash_ToSlashSlash()
        {
            Assert.That(@"\".Escape(), Is.EqualTo(@"\\"));
        }

        [Test]
        public void Escape_LT_ToAmpLTSemicolon()
        {
            Assert.That("<".Escape(), Is.EqualTo("&lt;"));
        }

        [Test]
        public void Escape_GT_ToAmpGTSemicolon()
        {
            Assert.That(">".Escape(), Is.EqualTo("&gt;"));
        }

        [Test]
        public void Escape_StringIsNull_ReturnsEmptyString()
        {
            string s = null;
            Assert.That(s.Escape(), Is.EqualTo(""));
        }
    }
}
