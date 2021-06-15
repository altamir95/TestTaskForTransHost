﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskForTransHost.Enums;
using Xunit;

namespace TestTaskForTransHost.Tests.HotelController
{
    public class GetByNameActionTests
    {
        [Fact]
        public void IfNameInParametrIsNull()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();

            // act
            var result = controller.GetByName(null);
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
        [Fact]
        public void IfNameInParametrIsEmptyString()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();

            // act
            var result = controller.GetByName("  ");
            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
        }
         
        [Fact]
        public void IfInParameterUnexistentObjectName()
        {
            // Arrange
            Controllers.HotelController controller = new Controllers.HotelController();

            // act
            var result = controller.GetByName("The Overlook Hotel");
            var notFoundResult = result as  NotFoundObjectResult;

            // assert
            Assert.NotNull(notFoundResult);
        }
    }
}