using System;
using Bling.Domain.CustomerService;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain.CustomerService
{
    [TestFixture]
    public class SecurityConnectionShipDateTests
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
        public void Should_be_able_to_convert_object_to_html_table_row()
        {
            //SecurityConnectionShipDateInfo scsd = new SecurityConnectionShipDateInfo("123\tINVESTORNO\tBORROWER\tdoc type\t1/1/2000\tHousing Finance	California Housing	7.90127E+11");
            SecurityConnectionShipDateInfo scsd = new SecurityConnectionShipDateInfo("GM8060105PC	1051000204	GEM Mortgage	12/08/2010			UNDERHILL	\"BANK OF AMERICA, N.A.\"	229159922		12/11/2010		TitlePolicy	05/20/2011	05/25/2011	794796835369	BofA052511	Bank of America	\"BAC HOME LOANS SERVICING, LP\"	4951 Savarese Circle - Mail Code:  FL1-907-01-11  ATTN: Florida Document Processing");

            Assert.That(scsd.ToString(), Is.EqualTo("<tr><td>1051000204</td><td>TITLE POLICY</td><td>05/25/2011</td></tr>"));
            
        }
    }
}
