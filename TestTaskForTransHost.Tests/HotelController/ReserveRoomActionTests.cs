using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ClientDateBirth = new DateTime(),
                ClientName = null,
                ClientPassportNumber = null,
                Room = new Models.Room() { HotelId = 0, RoomClassId = 0, Id = 0 }
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
                ClientDateBirth = DateTime.Now,
                ClientName = "ClientName",
                ClientPassportNumber = "ClientPassportNumber",
                Room = new Room() { HotelId = int.MaxValue, RoomClassId = int.MaxValue, Id = int.MaxValue }
            };

            // act
            var result = controller.ReserveRoom(new ReserveRoomViewModel());
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
    }
}
