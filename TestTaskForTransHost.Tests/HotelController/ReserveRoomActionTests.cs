using Microsoft.AspNetCore.Mvc;
using System;
using TestTaskForTransHost.Models;
using TestTaskForTransHost.ViewModels;
using Xunit;

namespace TestTaskForTransHost.Tests.HotelController
{
    public class ReserveRoomActionTests
    {
        [Fact]
        public void IfAllParametrsNull()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();
            // act
            var result = controller.ReserveRoom(null);
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
        [Fact]
        public void IfAllParametrsNewObject()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();
            // act
            var result = controller.ReserveRoom(new ReserveRoomViewModel());
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
        [Fact]
        public void IfInReserveRoomViewModelAllVariableIsNull()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();
            ReserveRoomViewModel viewModel = new ReserveRoomViewModel()
            {
                Client = new ClientModel()
                {
                    DateBirth = new DateTime(),
                    FullName = null,
                    PassportNumber = null,
                },
                roomId = 0
            };

            // act 
            var result = controller.ReserveRoom(viewModel);
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
        [Fact]
        public void IfInReserveRoomViewModelAllVariableValueIsincorrect()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();
            ReserveRoomViewModel viewModel = new ReserveRoomViewModel()
            {
                 Client = new ClientModel()
                 {
                     DateBirth =DateTime.Now,
                     FullName = "ClientName",
                     PassportNumber="ClientPassportNumber"
                 }, 
                roomId = 222
            };

            // act
            var result = controller.ReserveRoom(viewModel);
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
    }
}
