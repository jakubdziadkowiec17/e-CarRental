using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Implementations;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace e_CarRentalAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCarCount()
        {
            try
            {
                var carCount = await _carRepository.GetCarCountAsync();
                return Ok(carCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<CarListDTO>> GetCars()
        {
            try
            {
                var cars = await _carRepository.GetCars();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCar(int id)
        {
            try
            {
                var car = await _carRepository.GetCarByIdAsync(id);
                if (car == null)
                {
                    return NotFound();
                }

                var carDTO = _mapper.Map<CarDTO>(car);
                return Ok(carDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarDTO carDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var car = _mapper.Map<Car>(carDTO);
                await _carRepository.AddCarAsync(car);

                return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarDTO carDTO)
        {
            try
            {
                var car = await _carRepository.GetCarByIdAsync(id);
                if (car == null)
                {
                    return NotFound();
                }

                _mapper.Map(carDTO, car);
                await _carRepository.UpdateCarAsync(car);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var car = await _carRepository.GetCarByIdAsync(id);
                if (car == null)
                {
                    return NotFound();
                }
                car.IsArchive = true;
                await _carRepository.DeleteCarAsync(car);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
