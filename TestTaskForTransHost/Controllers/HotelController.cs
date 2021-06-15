using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (hotelName == null) ModelState.AddModelError("errors", HttpRequestValues.parametr_is_null.ToString());
            else hotelName = hotelName.Trim();

            if (hotelName == "") ModelState.AddModelError("errors", HttpRequestValues.parametr_is_empty.ToString());

            if (!ModelState.IsValid) return BadRequest(ModelState);


            Booker booker = new Booker();

            Hotel hotel = booker.FindHotel(hotelName);
            if (hotel == null) return NotFound(HttpRequestValues.resource_not_found.ToString());

            return new ObjectResult(hotel);
        }

        [HttpGet]
        public IActionResult GetFreeRooms(DateTime reservationRoomDateTime, int hotelId, RoomClasses roomClass)
        {
            if (hotelId < 1) ModelState.AddModelError("errors", HttpRequestValues.id_is_less_one.ToString());

            if (reservationRoomDateTime == new DateTime()) ModelState.AddModelError("errors", HttpRequestValues.date_not_specified.ToString());

            if (Convert.ToInt32(roomClass) == 0) ModelState.AddModelError("errors", HttpRequestValues.room_classes_not_specified.ToString());

            if (!ModelState.IsValid) return BadRequest(ModelState);


            DateTime reservationRoomDate = reservationRoomDateTime.Date;

            Booker booker = new Booker();

            List<Room> rooms = booker.GetFreeRooms(reservationRoomDate, hotelId, roomClass);

            return new ObjectResult(rooms);
        }

        [HttpPost]
        public IActionResult ReserveRoom(ReserveRoomViewModel viewModel)
        {
            if (viewModel == null) ModelState.AddModelError("errors", HttpRequestValues.parametr_is_null.ToString());
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (viewModel.Room == null) ModelState.AddModelError("errors", HttpRequestValues.parametr_is_null.ToString());
            if (!ModelState.IsValid) return BadRequest(ModelState); 

            if (viewModel.ClientPassportNumber == null) ModelState.AddModelError("errors", HttpRequestValues.client_passport_number_is_null.ToString());
            else viewModel.ClientPassportNumber = viewModel.ClientName.Trim();
            if (viewModel.ClientPassportNumber == "") ModelState.AddModelError("errors", HttpRequestValues.client_passport_number_is_empty.ToString());


            if (viewModel.Room.Id < 1) ModelState.AddModelError("errors", HttpRequestValues.room_id_is_less_one.ToString());
            if (viewModel.Room.HotelId < 1) ModelState.AddModelError("errors", HttpRequestValues.hotel_id_is_less_one.ToString());
            if (!Enum.IsDefined(typeof(RoomClasses), viewModel.Room.RoomClassId)) ModelState.AddModelError("errors", HttpRequestValues.value_in_enum_room_classes_unexist.ToString());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isClientExist = new BookingContext()
               .Clients
               .Any(r => r.PassportNumber == viewModel.ClientPassportNumber);
            if (!isClientExist)
            {
                if (viewModel.ClientName == null) ModelState.AddModelError("errors", HttpRequestValues.client_name_is_null.ToString());
                else viewModel.ClientName = viewModel.ClientName.Trim();

                if (viewModel.ClientName == "") ModelState.AddModelError("errors", HttpRequestValues.client_name_is_empty.ToString());

                if (viewModel.ClientDateBirth == new DateTime()) ModelState.AddModelError("errors", HttpRequestValues.client_date_birth_not_specified.ToString());
            }

            viewModel.Room.Hotel = new Hotel();
            viewModel.Room.RoomClass = new RoomClass();

            var isRoomExist = new BookingContext()
                .Rooms
                .Any(r => r.Id == viewModel.Room.Id && r.RoomClassId == viewModel.Room.RoomClassId && r.HotelId == viewModel.Room.HotelId);
            if (!isRoomExist)
            {
                ModelState.AddModelError("errors", HttpRequestValues.room_unexist.ToString());
            }


            if (!ModelState.IsValid) return BadRequest(ModelState);


            DateTime reservationRoomDate = DateTime.Now.AddDays(1).Date;

            Booker booker = new Booker();

            try
            {
                booker.ReserveRoom(reservationRoomDate, viewModel.Room, viewModel.ClientPassportNumber, viewModel.ClientName, viewModel.ClientDateBirth);
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("errors", HttpRequestValues.selected_room_already_reserved.ToString());
            }
            catch
            {
                ModelState.AddModelError("errors", HttpRequestValues.unexpected_error_during_reserve.ToString());
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);


            return Ok();
        }
    }
}
