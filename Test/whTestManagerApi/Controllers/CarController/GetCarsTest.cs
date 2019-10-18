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

namespace whTestManagerApi.Controllers.CarController
{
    [TestFixture]
    public class GetCarsTest
    {
        [Test]
        public void When_RunWithoutService_Then_GetNullReferenceException()
        {
            var controller = new WC.CarController(null);
            Assert.That(async () => await controller.GetCars(), Throws.TypeOf<NullReferenceException>());
        }
        [Test]
        public async Task When_FindAllCard_Then_ReturnAllExistingCars()
        {
            var carService = new Mock<ICarService>(MockBehavior.Strict);
            carService.Setup(p => p.GetCars()).Returns(Task.FromResult<IList<Car>>(new List<Car>()));

            WC.CarController carController = new WC.CarController(carService.Object);
            var result = await carController.GetCars();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual((((result as OkObjectResult)?.Value as IList<Car>).Count), 0);
            carService.VerifyAll();
        }
    }
}
