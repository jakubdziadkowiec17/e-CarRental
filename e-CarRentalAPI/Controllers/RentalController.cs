using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Implementations;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace e_CarRentalAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICarRepository _carRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalController(IRentalRepository rentalRepository, IMapper mapper, IClientRepository clientRepository, ICarRepository carRepository, IPaymentRepository paymentRepository, UserManager<ApplicationUser> userManager)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _carRepository = carRepository;
            _paymentRepository = paymentRepository;
            _userManager = userManager;
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetRentalCount()
        {
            try
            {
                var rentalCount = await _rentalRepository.GetRentalCountAsync();
                return Ok(rentalCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("monthCount")]
        public async Task<ActionResult<List<ChartDTO>>> GetRentalMonthCount()
        {
            try
            {
                var rentalMonthCount = await _rentalRepository.GetRentalMonthCountAsync();
                return Ok(rentalMonthCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<RentalDTO>>> GetRentals()
        {
            try
            {
                var rentals = await _rentalRepository.GetRentals();
                var rentalDTOs = new List<RentalDTO>();

                foreach (var rental in rentals)
                {
                    var sum = 0;
                    string paid;
                    var client = await _clientRepository.GetClientByIdAsync(rental.ClientId);
                    var user = await _userManager.FindByIdAsync(rental.CreatedByUser);
                    var car = await _carRepository.GetCarByIdAsync(rental.CarId);
                    var payments = await _paymentRepository.GetPaymentsByRentalId(rental.Id);

                    foreach (var payment in payments)
                    {
                        sum = sum + payment.Sum;
                    }

                    if (sum >= (rental.Hours * car.PricePerHour))
                    {
                        paid = "Yes";
                    }
                    else
                    {
                        paid = "No";
                    }

                    var rentalDTO = new RentalDTO
                    {
                        Id = rental.Id,
                        ClientId = client.Id,
                        ClientFullName = client.FirstName + client.SecondName + client.LastName + " - " + client.PESEL,
                        UserId = user.Id,
                        UserFullName = user.FirstName + user.SecondName + user.LastName,
                        CarId = car.Id,
                        FullNameWithYear = car.Brand + car.Model + " - " + car.YearOfProduction,
                        Hours = rental.Hours,
                        CreatedDate = rental.CreatedDate,
                        StartDate = rental.StartDate,
                        EndDate = rental.EndDate,
                        IsPaid = paid,
                        PaymentStatus = sum + " / " + (rental.Hours * car.PricePerHour)
                    };

                    rentalDTOs.Add(rentalDTO);
                }

                return Ok(rentalDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getRentalsByClientId/{clientId}")]
        public async Task<ActionResult<List<RentalDTO>>> GetRentalsByClientId(int clientId)
        {
            try
            {
                var rentals = await _rentalRepository.GetRentalsByClientId(clientId);
                var rentalDTOs = new List<RentalDTO>();

                foreach (var rental in rentals)
                {
                    var sum = 0;
                    string paid;
                    var client = await _clientRepository.GetClientByIdAsync(rental.ClientId);
                    var user = await _userManager.FindByIdAsync(rental.CreatedByUser);
                    var car = await _carRepository.GetCarByIdAsync(rental.CarId);
                    var payments = await _paymentRepository.GetPaymentsByRentalId(rental.Id);

                    foreach (var payment in payments)
                    {
                        sum = sum + payment.Sum;
                    }

                    if (sum >= (rental.Hours * car.PricePerHour))
                    {
                        paid = "Yes";
                    }
                    else
                    {
                        paid = "No";
                    }

                    var rentalDTO = new RentalDTO
                    {
                        Id = rental.Id,
                        ClientId = client.Id,
                        ClientFullName = client.FirstName + client.SecondName + client.LastName + " - " + client.PESEL,
                        UserId = user.Id,
                        UserFullName = user.FirstName + user.SecondName + user.LastName,
                        CarId = car.Id,
                        FullNameWithYear = car.Brand + car.Model + " - " + car.YearOfProduction,
                        Hours = rental.Hours,
                        CreatedDate = rental.CreatedDate,
                        StartDate = rental.StartDate,
                        EndDate = rental.EndDate,
                        IsPaid = paid,
                        PaymentStatus = sum + " / " + (rental.Hours * car.PricePerHour)
                    };

                    rentalDTOs.Add(rentalDTO);
                }

                return Ok(rentalDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getRentalsByCarId/{carId}")]
        public async Task<ActionResult<List<RentalDTO>>> GetRentalsByCarId(int carId)
        {
            try
            {
                var rentals = await _rentalRepository.GetRentalsByCarId(carId);
                var rentalDTOs = new List<RentalDTO>();

                foreach (var rental in rentals)
                {
                    var sum = 0;
                    string paid;
                    var client = await _clientRepository.GetClientByIdAsync(rental.ClientId);
                    var user = await _userManager.FindByIdAsync(rental.CreatedByUser);
                    var car = await _carRepository.GetCarByIdAsync(rental.CarId);
                    var payments = await _paymentRepository.GetPaymentsByRentalId(rental.Id);

                    foreach (var payment in payments)
                    {
                        sum = sum + payment.Sum;
                    }

                    if (sum >= (rental.Hours * car.PricePerHour))
                    {
                        paid = "Yes";
                    }
                    else
                    {
                        paid = "No";
                    }

                    var rentalDTO = new RentalDTO
                    {
                        Id = rental.Id,
                        ClientId = client.Id,
                        ClientFullName = client.FirstName + client.SecondName + client.LastName + " - " + client.PESEL,
                        UserId = user.Id,
                        UserFullName = user.FirstName + user.SecondName + user.LastName,
                        CarId = car.Id,
                        FullNameWithYear = car.Brand + car.Model + " - " + car.YearOfProduction,
                        Hours = rental.Hours,
                        CreatedDate = rental.CreatedDate,
                        StartDate = rental.StartDate,
                        EndDate = rental.EndDate,
                        IsPaid = paid,
                        PaymentStatus = sum + " / " + (rental.Hours * car.PricePerHour)
                    };

                    rentalDTOs.Add(rentalDTO);
                }

                return Ok(rentalDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDTO>> GetRental(int id)
        {
            try
            {
                var sum = 0;
                string paid;
                var rental = await _rentalRepository.GetRentalByIdAsync(id);
                var client = await _clientRepository.GetClientByIdAsync(rental.ClientId);
                var user = await _userManager.FindByIdAsync(rental.CreatedByUser);
                var car = await _carRepository.GetCarByIdAsync(rental.CarId);
                var payments = await _paymentRepository.GetPaymentsByRentalId(rental.Id);
                if(rental != null && client != null && user != null && car != null && payments != null)
                {
                    foreach (var payment in payments)
                    {
                        sum = sum + payment.Sum;
                    }

                    if (sum>=(rental.Hours * car.PricePerHour))
                    {
                        paid = "Yes";
                    }
                    else
                    {
                        paid = "No";
                    }

                    var rentalDTO = new RentalDTO
                    {
                        Id = rental.Id,
                        ClientId = client.Id,
                        ClientFullName = client.FirstName + client.SecondName + client.LastName + " - " + client.PESEL,
                        UserId = user.Id,
                        UserFullName = user.FirstName + user.SecondName + user.LastName,
                        CarId = car.Id,
                        FullNameWithYear = car.Brand + car.Model + " - " + car.YearOfProduction,
                        Hours = rental.Hours,
                        CreatedDate = rental.CreatedDate,
                        StartDate = rental.StartDate,
                        EndDate = rental.EndDate,
                        IsPaid = paid,
                        PaymentStatus = sum + " / " + (rental.Hours * car.PricePerHour)
                    };
                    
                    return Ok(rentalDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRental(RentalDTO rentalDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DateTime date = DateTime.Now;

                var rental = new Rental
                {
                    ClientId = rentalDTO.ClientId,
                    CreatedByUser = rentalDTO.UserId,
                    CarId = rentalDTO.CarId,
                    Hours = rentalDTO.Hours,
                    CreatedDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0),
                    StartDate = rentalDTO.StartDate,
                    EndDate = rentalDTO.EndDate
                };

                await _rentalRepository.AddRentalAsync(rental);

                return CreatedAtAction(nameof(GetRentals), new { id = rental.Id }, rental);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, RentalDTO rentalDTO)
        {
            try
            {
                var rental = await _rentalRepository.GetRentalByIdAsync(id);
                if (rental == null)
                {
                    return NotFound();
                }

                rental.ClientId = rentalDTO.ClientId;
                rental.CreatedByUser = rentalDTO.UserId;
                rental.CarId = rentalDTO.CarId;
                rental.Hours = rentalDTO.Hours;
                rental.StartDate = rentalDTO.StartDate;
                rental.EndDate = rentalDTO.EndDate;

                await _rentalRepository.UpdateRentalAsync(rental);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            try
            {
                var rental = await _rentalRepository.GetRentalByIdAsync(id);
                if (rental == null)
                {
                    return NotFound();
                }
                await _rentalRepository.DeleteRentalAsync(rental);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
