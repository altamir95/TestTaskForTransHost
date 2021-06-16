using Microsoft.EntityFrameworkCore;
using System;

namespace TestTaskForTransHost.Models
{
    public class BookingContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomReservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomClass> RoomClasses { get; set; }

        public BookingContext()
        {
            Database.EnsureCreated(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("workstation id=transhostdb.mssql.somee.com;packet size=4096;user id=zakariev_SQLLogin_1;pwd=9iwap7r8x2;data source=transhostdb.mssql.somee.com;persist security info=False;initial catalog=transhostdb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasAlternateKey(a => a.PassportNumber); 
            modelBuilder.Entity<Hotel>().HasAlternateKey(a => a.Name);

            modelBuilder.Entity<Client>().HasData(
                new Client[]
                {
                new Client { Id=1,DateBirth=new DateTime(1995,10,28),FullName="Bill Gates",PassportNumber="31195855" }
                });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel[]
                {
                new Hotel { Id=1, Name="Capella Ubud" },
                new Hotel { Id=2, Name="The Ritz-Carlton" }
                });

            modelBuilder.Entity<RoomClass>().HasData(
               new RoomClass[]
               {
                new RoomClass { Id=(int)Enums.RoomClasses.One },
                new RoomClass { Id=(int)Enums.RoomClasses.Two  },
                new RoomClass { Id=(int)Enums.RoomClasses.Three  }
               });


            modelBuilder.Entity<Room>().HasData(
               new Room[]
               {
                new Room { Id=1, HotelId=1,RoomClassId=1 },
                new Room {Id=2, HotelId=1,RoomClassId=2   },
                new Room { Id=3, HotelId=1,RoomClassId=3  },
                new Room { Id=4, HotelId=1,RoomClassId=3  },
                new Room { Id=5, HotelId=2,RoomClassId=1  },
                new Room { Id=6, HotelId=2,RoomClassId=2  },
                new Room { Id=7, HotelId=2,RoomClassId=3  },
                new Room { Id=8, HotelId=2,RoomClassId=3  },
               });

            modelBuilder.Entity<RoomReservation>().HasData(
             new RoomReservation[]
             {
                new RoomReservation { Id=1,ClientId=1,ReservationDate= new DateTime(2021,12,12),RoomId=1 }
             });
        }
    }
}
