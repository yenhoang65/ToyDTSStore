using Business.DTO.CategoryDTO;
using Business.DTO.Payment;
using Business.Helper;
using Business.Response;
using Data.Models;
using DTS_ToyStore.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository.PaymentRepository;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IConfiguration _configuration;
        public PaymentController(IConfiguration configuration, IPaymentRepository paymentRepository)
        {
            _configuration = configuration;
            _paymentRepository = paymentRepository;
        }

        [HttpGet("GetAllPaymentByUserId")]
        public async Task<IActionResult> GetAllPaymentByUserId(int pageIndex = 1, int pageSize = 10, PaymentStatus? status = null)
        {
            var result = await _paymentRepository.GetPaymentsByUserIdAsync(pageIndex, pageSize, status);
            return Ok(result);
        }


        [HttpGet("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments(int pageIndex = 1, int pageSize = 10, PaymentStatus? status = null)
        {
            var result = await _paymentRepository.GetPaymentsAsync(pageIndex, pageSize, status);
            return Ok(result);
        }



        [HttpGet("CreatePayment")]
        public async Task<IActionResult> CreatePayment(Guid paymentId)
        {
            try
            {
                var response = await _paymentRepository.CreatePaymentUrl(paymentId);
                if (response.Code == ResponseCode.Success)
                {
                    string paymentUrl = response.Data?.ToString();
                    return Ok(paymentUrl);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("{id}")]

        public async Task<ActionResult<Response>> PaymentCallback(Guid id, [FromForm] CreatePaymentDTO paymentDTO)
        {
            try
            {
                var response = await _paymentRepository.ProcessVnPayCallback(id, paymentDTO);
                if (response.Code == ResponseCode.NotFound)
                {
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception ex){
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi cập nhật danh mục: " + ex.Message
                });
            }
        }
    }
}
