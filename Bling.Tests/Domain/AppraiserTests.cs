using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bling.Domain;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Domain
{
    [TestFixture]
    public class AppraiserTests
    {
        [Test]
        public void ApprovedLoanTypesContains_LoanTypeIsInTheList_ReturnsTrue()
        {
            //Arrange
            Appraiser appraiser = new Appraiser { ApprovedLoanTypes = "FHA/CONV" };
	
            //Act
            var result = appraiser.ApprovedLoanTypesContains("fha");

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ApprovedLoanTypesContains_LoanTypeIsNotInTheList_ReturnsFalse()
        {
            //Arrange
            Appraiser appraiser = new Appraiser { ApprovedLoanTypes = "CONV" };

            //Act
            var result = appraiser.ApprovedLoanTypesContains("fha");

            //Assert
            Assert.That(result, Is.False);
        }


        [Test]
        public void ToHTMLDropDown_AppraiserIsEmpty_ReturnsNoAvailableAppraiser()
        {
            //Arrange
            List<Appraiser> list = new List<Appraiser>();
            
            //Act
            string result = Appraiser.ToHTMLDropDown(list, "", "");

            //Assert
            Assert.That(result, Is.EqualTo("No Available Appraiser"));
        }

        [Test]
        public void ToHTMLDropDown_NoApprovedLoanTypeOnList_ReturnsNoAvailableAppraiser()
        {
            //Arrange
            List<Appraiser> list = new List<Appraiser>();
            list.Add(new Appraiser { Id = "1", FirstName = "F1", LastName = "L1", ApprovedLoanTypes = "lt" });
            list.Add(new Appraiser { Id = "2", FirstName = "F2", LastName = "L2", ApprovedLoanTypes = "lt" });
            list.Add(new Appraiser { Id = "3", FirstName = "F3", LastName = "L3", ApprovedLoanTypes = "lt" });


            //Act
            string result = Appraiser.ToHTMLDropDown(list, "", "other");

            //Assert
            Assert.That(result, Is.EqualTo("No Available Appraiser"));
        }

        [Test]
        public void ToHTMLDropDown_LastAppraiserIsGiven_ReturnsDropdownWithNextItemSelected()
        {
            //Arrange
            List<Appraiser> list = new List<Appraiser>();
            list.Add(new Appraiser { Id = "1", FirstName = "F1", LastName = "L1", ApprovedLoanTypes = "lt" });
            list.Add(new Appraiser { Id = "2", FirstName = "F2", LastName = "L2", ApprovedLoanTypes = "lt" });
            list.Add(new Appraiser { Id = "3", FirstName = "F3", LastName = "L3", ApprovedLoanTypes = "lt" });
            	
            //Act
            string result = Appraiser.ToHTMLDropDown(list, "2", "lt");

            //Assert
            Assert.That(result, Is.EqualTo("<select id='ddAppraiser'><option value=\"1\" >F1 L1 ()</option><option value=\"2\" >F2 L2 ()</option><option value=\"3\" selected=\"selected\">F3 L3 ()</option></select>"));
        }

        [Test]
        public void ToHTMLDropDown_AppraiserPreviouslySelectedAsCONV_ShouldSelectNextFHAOnTheList()
        {
            //Arrange
            List<Appraiser> list = new List<Appraiser>();
            list.Add(new Appraiser { Id = "1", FirstName = "F1", LastName = "L1", ApprovedLoanTypes = "conv" });
            list.Add(new Appraiser { Id = "2", FirstName = "F2", LastName = "L2", ApprovedLoanTypes = "conv" });
            list.Add(new Appraiser { Id = "3", FirstName = "F3", LastName = "L3", ApprovedLoanTypes = "conv" });
            list.Add(new Appraiser { Id = "4", FirstName = "F4", LastName = "L4", ApprovedLoanTypes = "fha" });
            list.Add(new Appraiser { Id = "5", FirstName = "F5", LastName = "L5", ApprovedLoanTypes = "fha" });

            //Act
            string result = Appraiser.ToHTMLDropDown(list, "2", "FHA");

            //Assert
            Assert.That(result, Is.EqualTo("<select id='ddAppraiser'><option value=\"4\" selected=\"selected\">F4 L4 ()</option><option value=\"5\" >F5 L5 ()</option></select>"));
        }

        [Test]
        public void ToHTMLDropDown_AppraiserPreviouslySelectedIsLastInTheItem_ShouldSelectTheFirstOneOnTheList()
        {
            //Arrange
            List<Appraiser> list = new List<Appraiser>();
            list.Add(new Appraiser { Id = "1", FirstName = "F1", LastName = "L1", ApprovedLoanTypes = "conv" });
            list.Add(new Appraiser { Id = "2", FirstName = "F2", LastName = "L2", ApprovedLoanTypes = "conv" });
            list.Add(new Appraiser { Id = "3", FirstName = "F3", LastName = "L3", ApprovedLoanTypes = "conv" });
            list.Add(new Appraiser { Id = "4", FirstName = "F4", LastName = "L4", ApprovedLoanTypes = "fha" });
            list.Add(new Appraiser { Id = "5", FirstName = "F5", LastName = "L5", ApprovedLoanTypes = "fha" });

            //Act
            string result = Appraiser.ToHTMLDropDown(list, "3", "CONV");

            //Assert
            Assert.That(result, Is.EqualTo("<select id='ddAppraiser'><option value=\"1\" >F1 L1 ()</option><option value=\"2\" >F2 L2 ()</option><option value=\"3\" >F3 L3 ()</option></select>"));
        }

    }
}
