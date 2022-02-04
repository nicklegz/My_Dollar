using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TransactionController(ITransactionRepository transactionRepo, IHttpContextAccessor httpContextAccessor)
    {
        _transactionRepo = transactionRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetListTransactions()
    {
        try
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (username == null || string.IsNullOrWhiteSpace(username))
            {
                return BadRequest();
            }

            var listTransactions = await _transactionRepo.GetListTransactionsAsync(username);

            return Ok(listTransactions);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}