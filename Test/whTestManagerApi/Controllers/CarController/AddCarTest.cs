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
    public class AddCarTest
    {
        [Test]
        public void When_RunWithoutService_Then_GetNullReferenceException()
        {
            var controller = new WC.CarController(null);
            Assert.That(async () => await controller.GetCars(), Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public async Task When_ModelStateIsNotValid_Then_ReturnBadRequest()
        {
            var carService = new Mock<ICarService>(MockBehavior.Strict);
            var carExample = new Car() { PlateNumber = "123" };
            WC.CarController carController = new WC.CarController(carService.Object);
            carController.ModelState.AddModelError("car", "Car not valid");

            var result = await carController.AddCar(carExample);

            Assert.That(result, Is.TypeOf<BadRequestResult>());
            carService.VerifyAll();
        }

        [Test]
        public async Task When_ModelStateIsValid_Then_ReturnCreatedObject()
        {
            var carService = new Mock<ICarService>(MockBehavior.Strict);
            var carExample = new Car() { PlateNumber = "123" };
            carService.Setup(p => p.AddCar(carExample)).Returns(Task.FromResult<Car>(carExample));

            WC.CarController carController = new WC.CarController(carService.Object);

            var result = await carController.AddCar(carExample);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual((((result as OkObjectResult)?.Value as Car)?.PlateNumber ?? "123"), carExample.PlateNumber);
            carService.VerifyAll();
        }
    }
}
