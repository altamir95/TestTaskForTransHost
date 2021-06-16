using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace TestTaskForTransHost.Tests.HotelController
{
    public class GetFreeRoomsActionTests
    {
        [Fact]
        public void IfReservationRoomDateTimeIsNotSpecified()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();

            // act
            var result = controller.GetFreeRooms(new DateTime(), 1, Enums.RoomClasses.One);
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
        [Fact]
        public void IfHotelIdIsZero()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();

            // act
            var result = controller.GetFreeRooms(new DateTime(2021, 1, 1), 0, Enums.RoomClasses.One);
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
        [Fact]
        public void IfParametrsCorret()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();

            // act
            var result = controller.GetFreeRooms(new DateTime(2021, 11, 12), 1, Enums.RoomClasses.One);
            var objecResult = result as ObjectResult;

            // assert
            Assert.NotNull(objecResult);
        }
        [Fact]
        public void IfDateReserveDay()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();

            // act
            var result = controller.GetFreeRooms(new DateTime(2021, 12, 12), 1, Enums.RoomClasses.One);
            var emptyResult = result as EmptyResult;

            // assert
            Assert.NotNull(emptyResult);
        }
    }
}
