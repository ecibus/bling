using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain.LOS;

namespace Bling.Tests.Domain.LOS
{
    [TestFixture]
    public class HMDAChangesTest
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
        public void ConvertListToTable_should_return_empty_when_parameter_pass_is_empty_or_null()
        {
            Assert.That(HMDAChanges.ConvertListToTable(null), Is.Empty);
            Assert.That(HMDAChanges.ConvertListToTable(new List<HMDAChanges>()), Is.Empty);
        }

        [Test]
        public void Should_be_able_to_convert_list_to_html_table()
        {
            HMDAChanges hmda = new HMDAChanges()
            {
                Id = 1,
                LoanNumber = "LoanNumber",
                ReportYear = "2009",
                FieldName = "FieldName",
                NewData = "NewData",
                GEMUser = new Bling.Domain.GEMUser { Id = 0, UserInfo = new Bling.Domain.UserInfo { FirstName = "Me" } },
                CreatedOn = new DateTime(2008, 1, 1)
            };

            List<HMDAChanges> changes = new List<HMDAChanges>();
            changes.Add(hmda);

            string table =
                "<table>" +
                    "<tr>" +
                        "<td>Date</td><td>Loan Number</td><td>Field Name</td><td>New Value</td><td>Added By</td><td>&nbsp;</td>" +
                    "</tr>" +
                    "<tr id='hmda1'>" +
                        "<td>1/1/2008 12:00:00 AM</td><td>LoanNumber</td><td>FieldName</td><td>NewData</td><td>Me</td><td><img id='1' alt='Delete' src='/Images/Trash.gif' /></td>" +
                    "</tr>" +
                "</table>"
                ;
                
            Assert.That(HMDAChanges.ConvertListToTable(changes), Is.EqualTo(table));
        }

        [Test]
        public void Should_be_able_to_convert_object_to_html_row()
        {
            HMDAChanges hmda = new HMDAChanges()
            {
                Id = 1,
                LoanNumber = "LoanNumber",
                ReportYear = "2009",
                FieldName = "FieldName",
                NewData = "NewData",
                GEMUser = new Bling.Domain.GEMUser { Id = 0, UserInfo = new Bling.Domain.UserInfo { FirstName = "Me" } },
                //CreatedBy = "Me",
                CreatedOn = new DateTime(2008, 1, 1)
            };

            string row =
                "<tr id='hmda1'>" +
                    "<td>1/1/2008 12:00:00 AM</td><td>LoanNumber</td><td>FieldName</td><td>NewData</td><td>Me</td><td><img id='1' alt='Delete' src='/Images/Trash.gif' /></td>" +
                "</tr>";
                        
            Assert.That(hmda.ToRow(), Is.EqualTo(row));
        }
    }
}
