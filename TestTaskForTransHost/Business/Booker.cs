using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskForTransHost.Enums;
using TestTaskForTransHost.Models;

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
        //Поиск отеля по имени
        public Hotel FindHotel(string hotelName)
        {
            Hotel hotel = null;

            var context = new BookingContext();

            var foundHotel = context.Hotels.Where(i => i.Name == hotelName).FirstOrDefault();

            if (foundHotel != null)
            {
                hotel = foundHotel;
            }

            return hotel;
        }


        //получить список свободных номеров гостиницы нужного класса за указанную дату
        public List<Room> GetFreeRooms(DateTime date, int hotelId, RoomClasses roomClass)
        {
            List<Room> freeRooms = new List<Room>();

            var allHotelRooms = new BookingContext()
                .Hotels
                .Where(i => i.Id == hotelId)
                .Single()
                .Rooms;

            var roomClassId = Convert.ToInt32(roomClass); 

            var hotelRooms = allHotelRooms.Where(i => i.RoomClassId == roomClassId).ToList();

            var dateReservations = new BookingContext().Reservations.Where(i => i.ReservationDate == date).ToList();

            freeRooms = hotelRooms.Where(i => dateReservations.Where(j => j.RoomId == i.Id).Count() == 0).ToList();

            return freeRooms;
        }

        //зарезервировать номер, с проверкой на бронь и созданием записи клиента по необходимости
        public void ReserveRoom(DateTime date, Room room, string clientPassportNumber, string clientName, DateTime clientDateBirth)
        {
            var allHotelRooms = new BookingContext()
                .Hotels
                .Where(i => i.Id == room.HotelId)
                .Single()
                .Rooms;

            var hotelRooms = allHotelRooms
                .Where(i => i.RoomClassId == room.RoomClassId)
                .ToList();

            var dateReservations = new BookingContext().Reservations
                .Where(i => i.ReservationDate == date)
                .ToList();

            var freeRooms = hotelRooms
                .Where(i => dateReservations.Where(j => j.RoomId == i.Id).Count() == 0)
                .ToList();

            bool error = false;

            if (freeRooms.Where(i => i.Id == room.Id).ToList().Count == 0)
                error = true;

            var client = new BookingContext()
                .Clients
                .Where(i => i.PassportNumber == clientPassportNumber)
                .FirstOrDefault();

            if (error)
                throw new ArgumentException("Selected room already reserved");

            if (client == null)
            {
                client = new Client
                {
                    DateBirth = clientDateBirth,
                    PassportNumber = clientPassportNumber,
                    FullName = clientName
                };

                var context = new BookingContext();

                context.Add(client);

                context.SaveChanges();
            }


            var reservationContext = new BookingContext();

            var reservation = new RoomReservation();

            reservation.ClientId = client.Id;
            reservation.ReservationDate = date;
            reservation.RoomId = room.Id;

            reservationContext.Add(reservation);
            reservationContext.SaveChanges();
        }
    }
     

    
}
