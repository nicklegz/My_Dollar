using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Repositories.Interfaces;

namespace Controllers;

public class TransactionController : BaseController
{
    private readonly ITransactionRepository _transactionRepo;

    public TransactionController(ITransactionRepository transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetListTransactions()
    {
        // return _transactionRepo.GetListTransactionsAsync();
    }
}