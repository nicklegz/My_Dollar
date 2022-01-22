using Models;

namespace Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetListTransactionsAsync(string username);
    Task<Transaction> GetTransactionAsync(string username, int transactionId);
    Task<int> CreateTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction existingTransaction, Transaction updateTransaction);
    Task DeleteTransactionAsync(string username, Transaction transaction);
    Task<Transaction> GetTransactionByUserAndIdAsync(string username, int transactionId);
}