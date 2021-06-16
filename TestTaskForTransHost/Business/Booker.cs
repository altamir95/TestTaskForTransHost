using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskForTransHost.Enums;
using TestTaskForTransHost.Models;
using TestTaskForTransHost.ViewModels;

namespace TestTaskForTransHost.Business
{
    // 1. По приведенным ниже методам воссоздать необходимые модели в коде и БД. Проще говоря, сделать так, чтобы приведенный ниже
    //	  код компилировался и корректно отрабатывал в рантайме.
    // 2. Отрефакторить методы. Для простоты будем считать, что номера бронируются только на один день

    // Решение прислать в виде .NET Solution
    // Плюсом будет наличие запускаемого проекта, в котором можно выполнять нижепредстваленные методы, например,
    // консольное приложение с параметрами командной строки, Desktop или веб-сервис.
    // Любые уместные дополнения приветствуются (например, unit-тесты)

    public class Booker
    {
        private int BookingDaysCount = 1;
        BookingContext db;

        public Booker()
        {
            db = new BookingContext();
        }
        //Поиск отеля по имени
        public Hotel FindHotel(string hotelName)
        {
            if (string.IsNullOrWhiteSpace(hotelName)) return null;
            hotelName = hotelName.Trim();

            var foundHotel = db.Hotels.FirstOrDefault(i => i.Name == hotelName);

            return foundHotel;
        }


        //получить список свободных номеров гостиницы нужного класса за указанную дату
        public List<Room> GetFreeRooms(DateTime date, int hotelId, RoomClasses roomClass)
        {
            var roomClassId = Convert.ToInt32(roomClass);

            if (hotelId < 1) return null;

            if (date == new DateTime()) return null;

            if (roomClassId == 0) return null;

            var unreservedRooms = db.Rooms
                .Include(r => r.RoomReservations)
                .Where(r =>
                !r.RoomReservations.Any(s => s.ReservationDate == date) &&
                r.HotelId == hotelId &&
                r.RoomClassId == roomClassId
                )
                .ToList();

            return unreservedRooms;
        }

        public RoomReservation ReserveRoom(int roomId,string clientPassportNumber, DateTime clientDateBirth, string clientFullName)
        {
            if (roomId < 1) return null;

            if (string.IsNullOrWhiteSpace(clientPassportNumber)) return null;
            clientPassportNumber = clientPassportNumber.Trim();

            var reservationRoomDate = DateTime.Now.AddDays(BookingDaysCount).Date; 

            var isRoomReservedOrUnexist = db.Rooms
                .Include(r => r.RoomReservations)
                .Any(r => r.Id == roomId && !r.RoomReservations
                .Any(s => s.ReservationDate.Date == reservationRoomDate && s.RoomId == roomId));
            if (!isRoomReservedOrUnexist) return null;


            var ClientFromDB = new BookingContext()
               .Clients
               .FirstOrDefault(r => r.PassportNumber == clientPassportNumber);

            if (ClientFromDB == null)
            {
                if (string.IsNullOrWhiteSpace(clientFullName)) return null;
                clientFullName = clientFullName.Trim(); 

                if (clientDateBirth == new DateTime()) return null;

                ClientFromDB = new Client
                {
                    DateBirth = clientDateBirth,
                    PassportNumber = clientPassportNumber,
                    FullName = clientFullName
                };

                db.Add(ClientFromDB);

                db.SaveChanges();
            }

            var reservation = new RoomReservation()
            {
                ClientId = ClientFromDB.Id,
                ReservationDate = reservationRoomDate,
                RoomId = roomId,
            };

            db.Add(reservation);
            db.SaveChanges();

            return reservation;
        }
    }



}
