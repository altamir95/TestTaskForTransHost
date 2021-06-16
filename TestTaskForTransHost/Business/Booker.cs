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
            if (string.IsNullOrWhiteSpace(hotelName) ) return null;
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

        //зарезервировать номер, с проверкой на бронь и созданием записи клиента по необходимости
        public RoomReservation ReserveRoom(int roomId, ClientModel clientModel)
        { 
            if (roomId   <1) return null;
            if (clientModel == null) return null;
            if (string.IsNullOrWhiteSpace(clientModel.PassportNumber)) return null;
            clientModel.PassportNumber.Trim();

            if (string.IsNullOrWhiteSpace(clientModel.FullName)) return null;
            clientModel.FullName.Trim();

            if (roomId < 1) return null;  

            var reservationRoomDate = DateTime.Now.AddDays(BookingDaysCount).Date;

            var isRoomExist = db.Rooms.Any(r =>   r.Id == roomId);
            if (!isRoomExist) return null;

            var isRoomReservedOrUnexist = db.Reservations.Any(r => r.ReservationDate.Date == reservationRoomDate && r.RoomId == roomId  );
            if (isRoomReservedOrUnexist) return null;

            var ClientFromDB = new BookingContext()
               .Clients
               .FirstOrDefault(r => r.PassportNumber == clientModel.PassportNumber);

            if (ClientFromDB == null)
            {
                if (string.IsNullOrWhiteSpace(clientModel.FullName)) return null;
                clientModel.FullName = clientModel.FullName.Trim();

                if (clientModel.DateBirth == new DateTime()) return null; 

                ClientFromDB = new Client
                {
                    DateBirth = clientModel.DateBirth,
                    PassportNumber = clientModel.PassportNumber,
                    FullName = clientModel.FullName
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
