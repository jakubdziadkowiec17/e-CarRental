using AutoMapper;
using e_CarRentalAPI.Constants;
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
    public class PaymentController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("count")]
        [Authorize(Roles = $"{Role.Admin},{Role.Employee}")]
        public async Task<ActionResult<int>> GetPaymentCount()
        {
            try
            {
                var paymentCount = await _paymentRepository.GetPaymentCountAsync();
                return Ok(paymentCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{Role.Admin},{Role.Employee}")]
        public async Task<ActionResult<List<PaymentDTO>>> GetPayments()
        {
            try
            {
                var payments = await _paymentRepository.GetPayments();
                var paymentDTOs = new List<PaymentDTO>();

                foreach (var payment in payments)
                {
                    var user = await _userManager.FindByIdAsync(payment.CreatedByUser);

                    var paymentDTO = new PaymentDTO
                    {
                        Id = payment.Id,
                        RentalId = payment.RentalId,
                        Description = payment.Description,
                        Sum = payment.Sum,
                        PaymentDate = payment.PaymentDate,
                        UserId = user.Id,
                        UserFullName = user.FirstName + user.SecondName + user.LastName
                    };

                    paymentDTOs.Add(paymentDTO);
                }

                return Ok(paymentDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getPaymentsByRentalId/{rentalId}")]
        [Authorize(Roles = $"{Role.Admin},{Role.Employee}")]
        public async Task<ActionResult<List<PaymentDTO>>> GetPaymentsByRentalId(int rentalId)
        {
            try
            {
                var payments = await _paymentRepository.GetPaymentsByRentalId(rentalId);
                var paymentDTOs = new List<PaymentDTO>();

                foreach (var payment in payments)
                {
                    var user = await _userManager.FindByIdAsync(payment.CreatedByUser);

                    var paymentDTO = new PaymentDTO
                    {
                        Id = payment.Id,
                        RentalId = payment.RentalId,
                        Description = payment.Description,
                        Sum = payment.Sum,
                        PaymentDate = payment.PaymentDate,
                        UserId = user.Id,
                        UserFullName = user.FirstName + user.SecondName + user.LastName
                    };

                    paymentDTOs.Add(paymentDTO);
                }

                return Ok(paymentDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{Role.Admin},{Role.Employee}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByIdAsync(id);
                var user = await _userManager.FindByIdAsync(payment.CreatedByUser);

                if (payment != null)
                {
                    var paymentDTO = new PaymentDTO
                    {
                        Id = payment.Id,
                        RentalId = payment.RentalId,
                        Description = payment.Description,
                        Sum = payment.Sum,
                        PaymentDate = payment.PaymentDate,
                        UserId = user.Id,
                        UserFullName = user.FirstName + user.SecondName + user.LastName
                    };

                    return Ok(paymentDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{Role.Admin},{Role.Employee}")]
        public async Task<IActionResult> AddPayment(PaymentDTO paymentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DateTime date = DateTime.Now;

                var payment = new Payment
                {
                    Id = paymentDTO.Id,
                    RentalId = paymentDTO.RentalId,
                    Description = paymentDTO.Description,
                    Sum = paymentDTO.Sum,
                    PaymentDate = paymentDTO.PaymentDate,
                    CreatedByUser = paymentDTO.UserId
                };

                await _paymentRepository.AddPaymentAsync(payment);

                return CreatedAtAction(nameof(GetPayments), new { id = payment.Id }, payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var Payment = await _paymentRepository.GetPaymentByIdAsync(id);
                if (Payment == null)
                {
                    return NotFound();
                }
                await _paymentRepository.DeletePaymentAsync(Payment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
