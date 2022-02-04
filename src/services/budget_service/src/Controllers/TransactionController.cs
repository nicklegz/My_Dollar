using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepo;

    public TransactionController(ITransactionRepository transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetListTransactions()
    {
        return Ok();
    }
}