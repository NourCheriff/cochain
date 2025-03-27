using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("transactions/{walletId}")]
        [Authorize(Policy = "ReadTransactions")]
        public async Task<IActionResult> GetTransactionsByWalletId(string walletId)
        {
            var response = await _transactionService.GetTransactionsByWalletId(walletId);
            if (response == null)
            {
                return BadRequest(new { message = "Wallet ID not found" });
            }
            return Ok(response);
        }

        [HttpPost("AddTransaction")]
        [Authorize(Policy = "WriteTransaction")]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transactionObj)
        {
            var response = await _transactionService.AddTransaction(transactionObj);
            if (response == null)
            {
                return BadRequest(new { message = "Transaction not found" });
            }
            return Ok(response);
        }
    }
}