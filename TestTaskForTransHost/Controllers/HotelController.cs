using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTaskForTransHost.Business;
using TestTaskForTransHost.Enums;
using TestTaskForTransHost.Models;
using TestTaskForTransHost.ViewModels;

namespace TestTaskForTransHost.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HotelController : ControllerBase
    {

        [HttpGet("{hotelName}")]
        public IActionResult GetByName(string hotelName)
        {
            Booker booker = new Booker();

            Hotel hotel = booker.FindHotel(hotelName);
            if (hotel == null) return NotFound(HttpRequestValues.resource_not_found.ToString());

            return new ObjectResult(hotel);
        }

        [HttpGet]
        public IActionResult GetFreeRooms(DateTime reservationRoomDateTime, int hotelId, RoomClasses roomClass)
        {
            DateTime reservationRoomDate = reservationRoomDateTime.Date;

            Booker booker = new Booker();

            IEnumerable<Room> rooms = booker.GetFreeRooms(reservationRoomDate, hotelId, roomClass);
            if(rooms==null) return BadRequest(HttpRequestValues.incorrect_parametrs.ToString());
            if(rooms.Count()<1) return new EmptyResult();

            return new ObjectResult(rooms);
        }

        [HttpPost]
        public IActionResult ReserveRoom(ReserveRoomViewModel viewModel)
        {
            Booker booker = new Booker();

            if (viewModel == null) return BadRequest(HttpRequestValues.parametr_is_null.ToString());

            var result = booker.ReserveRoom(viewModel.roomId, viewModel.Client);

            if (result == null) return BadRequest(HttpRequestValues.selected_room_unexist_or_already_reserved.ToString());

            return Ok();
        }
    }
}
