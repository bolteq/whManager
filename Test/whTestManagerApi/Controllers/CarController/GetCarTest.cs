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
    public class GetCarTest
    {
        [Test]
        public void When_RunWithoutService_Then_GetNullReferenceException()
        {
            var controller = new WC.CarController(null);
            Assert.That(async () => await controller.GetCar(0), Throws.TypeOf<NullReferenceException>());
        }
        [Test]
        public async Task When_NotFindCar_Then_ReturnBadRequest()
        {
            var carService = new Mock<ICarService>(MockBehavior.Strict);
            carService.Setup(p => p.GetCar(0)).Returns(Task.FromResult<Car>(null));

            WC.CarController carController = new WC.CarController(carService.Object);
            var result = await carController.GetCar(0);

            Assert.That(result, Is.TypeOf<BadRequestResult>());

            carService.VerifyAll();
        }

        [Test]
        public async Task When_FindCar_Then_ReturnOkWithObiect()
        {
            var retCar = new Car()
            {
                Id = 1,
            };

            var carService = new Mock<ICarService>(MockBehavior.Strict);
            carService.Setup(p => p.GetCar(1)).Returns(Task.FromResult<Car>(retCar));

            WC.CarController carController = new WC.CarController(carService.Object);
            var result = await carController.GetCar(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual((((result as OkObjectResult)?.Value as Car)?.Id ?? 0), 1);
            carService.VerifyAll();
        }
    }
}
