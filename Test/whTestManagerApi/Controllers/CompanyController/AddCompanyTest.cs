using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using whManagerAPI.Services;
using whManagerLIB.Models;
using WC = whManagerAPI.Controllers;

namespace whTestManagerApi.Controllers.CompanyController
{
    public class AddCompanyTest
    {
        [Test]
        public void When_RunWithoutService_Then_GetNullReferenceException()
        {
            var controller = new WC.CompanyController(null);
            Assert.That(() => controller.GetCompanies(), Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public async Task When_ModelStateIsNotValid_Then_ReturnBadRequest()
        {
            var companyService = new Mock<ICompanyService>(MockBehavior.Strict);
            var companyExample = new Company() { Name = "Firma", Address = "Firmowa 10" };
            WC.CompanyController companyController = new WC.CompanyController(companyService.Object);
            companyController.ModelState.AddModelError("company", "Model not valid");

            var result = await companyController.SetCompany(companyExample);

            Assert.That(result, Is.TypeOf<BadRequestResult>());
            companyService.VerifyAll();
        }

        [Test]
        public async Task When_ModelStateIsValid_Then_ReturnCreatedObject()
        {
            var companyService = new Mock<ICompanyService>(MockBehavior.Strict);
            var companyExample = new Company() { Name = "Firma", Address = "Firmowa 10" };
            companyService.Setup(p => p.AddCompany(companyExample)).Returns(Task.FromResult<Company>(companyExample));

            WC.CompanyController companyController = new WC.CompanyController(companyService.Object);

            var result = await companyController.SetCompany(companyExample);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual((((result as OkObjectResult)?.Value as Company)?.Name ?? "Firma"), companyExample.Name);
            companyService.VerifyAll();
        }
    }
}
