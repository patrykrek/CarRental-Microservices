using app.Services.ReservationAPI.Models;
using app.Services.ReservationAPI.Models.DTO;
using app.Services.ReservationAPI.Repository.Interface;
using app.Services.ReservationAPI.Service;
using app.Services.ReservationAPI.Service.Interface;
using AutoMapper;
using Moq;

namespace app.ApiUnitTests.ReservationApiTests
{
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICarService> _carServiceMock;
        private readonly ReservationService _reservationService;

        public ReservationServiceTests()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _mapperMock = new Mock<IMapper>();
            _carServiceMock = new Mock<ICarService>();
            _reservationService = new ReservationService(_reservationRepositoryMock.Object, _mapperMock.Object, _carServiceMock.Object);
        }

        [Fact]
        public async Task GetAllReservations_ShouldReturnListOfAllReservations_WhenEverythingIsCorrect()
        {
            //arrange
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    Id = 1,
                    StartDate = new DateTime(2025, 03, 20),
                    EndDate = new DateTime(2025, 03, 25),
                    CarId = 1,
                    TotalPrice = 200,
                    ReservationDate = DateTime.Now,
                    UserId = "userId"
                },

                new Reservation
                {
                    Id = 2,
                    StartDate = new DateTime(2025, 03, 21),
                    EndDate = new DateTime(2025, 03, 25),
                    CarId = 2,
                    TotalPrice = 400,
                    ReservationDate = DateTime.Now,
                    UserId = "userId"
                }

            };

            var reservationsDTO = new List<GetReservationDTO>
            {
                new GetReservationDTO
                {
                    Id = 1,
                    StartDate = new DateTime(2025, 03, 20),
                    EndDate = new DateTime(2025, 03, 25),
                    CarId = 1,
                    TotalPrice = 200,
                    ReservationDate = DateTime.Now,
                    UserId = "userId"
                },

                new GetReservationDTO
                {
                    Id = 2,
                    StartDate = new DateTime(2025, 03, 21),
                    EndDate = new DateTime(2025, 03, 25),
                    CarId = 2,
                    TotalPrice = 400,
                    ReservationDate = DateTime.Now,
                    UserId = "userId"
                }
            };

            _reservationRepositoryMock.Setup(repo => repo.GetAllReservations()).ReturnsAsync(reservations);

            _mapperMock.Setup(m => m.Map<List<GetReservationDTO>>(It.IsAny<Reservation>())).Returns(reservationsDTO);


            //act

            var result = await _reservationService.GetAllReservations();

            //assert
            Assert.NotNull(result);

            Assert.True(result.IsSuccess);

            Assert.Equal(reservationsDTO.Count, ((List<GetReservationDTO>)result.Result).Count);

        }

        [Fact]
        public async Task GetAllReservations_ShouldReturnFalseSuccessAndMessage_WhenExceptionIsThrown()
        {
            //arrange

            _reservationRepositoryMock.Setup(repo => repo.GetAllReservations()).ThrowsAsync(new SystemException("Database problem"));

            //act

            var result = await _reservationService.GetAllReservations();

            //assert

            Assert.NotNull(result);

            Assert.False(result.IsSuccess);

            Assert.Equal("Database problem", result.Message);


        }

        [Fact]
        public async Task GetUserReservations_ShouldReturnUserReservations_WhenEverythingIsCorrect()
        {
            //arrange

            var userId = "userId";

            var reservation = new List<Reservation>
            {
                new Reservation
                {
                    Id = 1,
                    StartDate = new DateTime(2025, 03, 20),
                    EndDate = new DateTime(2025, 03, 25),
                    CarId = 1,
                    TotalPrice = 200,
                    ReservationDate = DateTime.Now,
                    UserId = userId,
                }
                
            };

            var reservationDTO = new List<GetUserReservationDTO>
            {
                new GetUserReservationDTO
                {
                    CarId = 1,
                    StartDate = new DateTime(2025, 03, 20),
                    EndDate = new DateTime(2025, 03, 25),
                    TotalPrice = 200,
                    ReservationDate = DateTime.Now,

                }
               
            };

            _reservationRepositoryMock.Setup(repo => repo.GetUserReservations(userId)).ReturnsAsync(reservation);

            _mapperMock.Setup(m => m.Map<List<GetUserReservationDTO>>(It.IsAny<Reservation>())).Returns(reservationDTO);


            //act

            var result = await _reservationService.GetUserReservations(userId);

            //assert
            Assert.NotNull(result);

            Assert.True(result.IsSuccess);

            Assert.Equal(reservationDTO.Count, ((List<GetUserReservationDTO>)result.Result).Count);
        }

        [Fact]
        public async Task GetUserReservations_ShouldReturnFalseSuccessAndMessage_WhenReservationsCountIs0()
        {
            //arrange
            var userId = "userId";

            var reservations = new List<Reservation>();

            _reservationRepositoryMock.Setup(repo => repo.GetUserReservations(userId)).ReturnsAsync(reservations);


            //act

            var result = await _reservationService.GetUserReservations(userId);


            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal("You don't have any reservations", result.Message);

        }

        [Fact]
        public async Task GetUserReservations_ShouldReturnFalseSuccessAndMessage_WhenExceptionIsThrown()
        {
            //arrange
            var userId = "userId";

            _reservationRepositoryMock.Setup(repo => repo.GetUserReservations(userId)).ThrowsAsync(new SystemException("Database problem"));

            //act

            var result = await _reservationService.GetUserReservations(userId);


            //assert

            Assert.False(result.IsSuccess);

            Assert.Equal("Database problem", result.Message);
        }

        [Fact]
        public async Task CreateReservation_ShouldCreateReservation_WhenEverythingIsCorrect()
        {
            //arrange
            var reservationDTO = new AddReservationDTO
            {
                CarId = 1,
                StartDate = new DateTime(2025, 03, 11),
                EndDate = new DateTime(2025,03,20),
                UserId = "userId"
            };

            _carServiceMock.Setup(serv => serv.GetCarById(It.IsAny<int>()))
                .ReturnsAsync(new ResponseDTO { IsSuccess = true, Result = new object() });

            _carServiceMock.Setup(serv => serv.GetCarPrice(It.IsAny<int>()))
                .ReturnsAsync(new ResponseDTO { IsSuccess = true, Result = 100 });

            _reservationRepositoryMock.Setup(repo => repo.AddReservation(It.IsAny<Reservation>()))
                .Returns(Task.CompletedTask);

            //act

            var result = await _reservationService.CreateReservation(reservationDTO);

            //assert
            Assert.True(result.IsSuccess);


        }

        [Fact]
        public async Task CreateReservation_ShouldReturnFalseSuccessAndMessage_WhenCarDoesNotExist()
        {
            //arrange
            var reservationDTO = new AddReservationDTO
            {
                CarId = 1,
                StartDate = new DateTime(2025, 03, 11),
                EndDate = new DateTime(2025, 03, 20),
                UserId = "userId"
            };

            _carServiceMock.Setup(serv => serv.GetCarById(It.IsAny<int>()))
                .ReturnsAsync(new ResponseDTO { IsSuccess = false, Message = "Car does not exist"});

            //act

            var result = await _reservationService.CreateReservation(reservationDTO);


            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal("Car does not exist", result.Message);

        }

        [Fact]
        public async Task CreateReservation_ShouldReturnFalseSuccessAndMessage_WhenStartDateIsAfterEndDate()
        {
            //arrange
            var reservationDTO = new AddReservationDTO
            {
                CarId = 1,
                StartDate = new DateTime(2025, 03, 11),
                EndDate = new DateTime(2025, 03, 9),
                UserId = "userId"
            };

            _carServiceMock.Setup(serv => serv.GetCarById(It.IsAny<int>()))
                .ReturnsAsync(new ResponseDTO { IsSuccess = true, Result = new object() });

            _carServiceMock.Setup(serv => serv.GetCarPrice(It.IsAny<int>()))
                .ReturnsAsync(new ResponseDTO { IsSuccess = true, Result = 100 });

            //act

            var result = await _reservationService.CreateReservation(reservationDTO);


            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal("Start Date can't be before End Date", result.Message);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnFalseSuccessAndMessage_WhenExceptionIsThrown()
        {
            //arrange

            var reservationDTO = new AddReservationDTO
            {
                CarId = 1,
                StartDate = new DateTime(2025, 03, 11),
                EndDate = new DateTime(2025, 03, 20),
                UserId = "userId"
            };

            _carServiceMock.Setup(serv => serv.GetCarById(It.IsAny<int>())).ThrowsAsync(new Exception("Database problem"));

            //act

            var result = await _reservationService.CreateReservation(reservationDTO);

            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal("Database problem", result.Message);
        }
    }
}