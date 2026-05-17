using Application.DTOs.Payments;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsService _paymentsService;

        public PaymentsController(PaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PaymentResponseDTO>> CreatePayment([FromForm] PaymentRequestDTO paymentRequest)
        {
            try
            {
                var paymentResponseDTO = await _paymentsService.CreatePayment(paymentRequest);
                return Ok(paymentResponseDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{paymentId}/proof")]
        [Authorize(Roles = "Owner, User")]
        public async Task<IActionResult> GetProof(Guid paymentId)
        {
            try
            {
                var result = await _paymentsService.GetPaymentProofUrl(paymentId);
                return PhysicalFile(result.ProofPath, result.ContentType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("underReview")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<PendingPaymentListDTO>>> GetPaymentsUnderReview()
        {
            return Ok(await _paymentsService.GetPaymentsUnderReview());
        }

        [HttpPatch("{paymentId}/status")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> ChangeStatusPayment(Guid paymentId, [FromQuery] ChangePaymentStatusDTO status)
        {
            try
            {
                await _paymentsService.ChangeStatusPayment(paymentId, status);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{paymentId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeletePayment(Guid paymentId)
        {
            try
            {
                await _paymentsService.DeletePayment(paymentId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
        
}
