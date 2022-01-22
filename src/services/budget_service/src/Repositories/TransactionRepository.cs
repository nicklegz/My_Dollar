using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;

namespace Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    public TransactionRepository(AppDbContext context)
    {
        _context= context;
    }

    public async Task<List<Transaction>> GetListTransactionsAsync(string username)
    {
        return await _context.Transactions.Where(
            transaction => transaction.Username == username).ToListAsync();
    }

    public async Task<Transaction> GetTransactionAsync(string username, int transactionId)
    {
        return await _context.Transactions.FirstOrDefaultAsync(
            transaction => transaction.Username == username && transaction.Id == transactionId
            );
    }

    public async Task<int> CreateTransactionAsync(Transaction transaction)
    {
        var newTransaction = await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return newTransaction.Entity.Id;
    }

    public async Task UpdateTransactionAsync(Transaction existingTransaction, Transaction updateTransaction)
    {
        existingTransaction = updateTransaction;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(string username, Transaction transaction)
    {
        _context.Remove(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<Transaction> GetTransactionByUserAndIdAsync(string username, int transactionId)
    {
        return await _context.Transactions.FirstOrDefaultAsync(
            transaction => transaction.Username == username && transaction.Id == transactionId);
    }
}