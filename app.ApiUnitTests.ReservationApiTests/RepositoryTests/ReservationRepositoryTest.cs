using app.Services.ReservationAPI.Data;
using app.Services.ReservationAPI.Models;
using app.Services.ReservationAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.ApiUnitTests.ReservationApiTests.RepositoryTests
{
    public class ReservationRepositoryTest
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly DataContext _context;

        public ReservationRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ReservationDatabase").Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();

            _reservationRepository = new ReservationRepository(_context);

            _context.AddRange(new List<Reservation>
            {
                new Reservation
                {
                    Id = 1,
                    ReservationDate = new DateTime(2025,02,20),
                    StartDate = new DateTime(2025, 02, 21),
                    EndDate = new DateTime(2025,02,22),
                    CarId = 1,
                    UserId = "userId",
                    TotalPrice = 2000
                },

                new Reservation
                {
                    Id = 2,
                    ReservationDate = new DateTime(2025,02,23),
                    StartDate = new DateTime(2025, 02, 24),
                    EndDate = new DateTime(2025,02,25),
                    CarId = 2,
                    UserId = "userId2",
                    TotalPrice = 1000
                }

            });

            _context.SaveChanges();

        }

        [Fact]
        public async Task GetReservations_ShouldReturnAllReservations()
        {
            //act
            var reservations = await _reservationRepository.GetAllReservations();

            //assert
            Assert.Equal(2, reservations.Count);
        }

        [Fact]
        public async Task GetUserReservations_ShouldReturnUserReservations() 
        {
            //act

            var reservations = await _reservationRepository.GetUserReservations("userId");

            //assert
            Assert.NotNull(reservations);

            Assert.Equal(1, reservations.Count);

            Assert.Equal("userId", reservations[0].UserId);
        }

        [Fact]
        public async Task AddReservation_ShouldAddNewReservationToDatabase()
        {
            //arrange
            var newReservation = new Reservation
            {
                Id = 3,
                ReservationDate = new DateTime(2025, 02, 26),
                StartDate = new DateTime(2025, 02, 27),
                EndDate = new DateTime(2025, 02, 28),
                CarId = 3,
                UserId = "userId3",
                TotalPrice = 3000
            };

            //act

            await _reservationRepository.AddReservation(newReservation);

            var reservations = await _reservationRepository.GetAllReservations();

            //assert
            Assert.Equal(3, reservations.Count);


        }
    }
}
