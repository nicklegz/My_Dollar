using Infrastructure;
using Models;
using Repositories.Filters.Models;

namespace Repositories.Filters.Queries;

public class TransactionQuery
{
    public IQueryable<Transaction> GetTransactionQuery(AppDbContext context, TransactionFilter transactionFilter)
    {
        IQueryable<Transaction> transactionQuery = context.Set<Transaction>();

        if(transactionFilter.FilterByAmount)
        {
            transactionQuery = transactionQuery.Where(
                transaction => transaction.Amount >= transactionFilter.AmountFrom && 
                transaction.Amount <= transactionFilter.AmountTo);
        }

        if(transactionFilter.FilterByEffectiveDate)
        {
            transactionQuery = transactionQuery.Where(
                transaction => transaction.EffectiveDate >= transactionFilter.EffectiveDateFrom && 
                transaction.EffectiveDate <= transactionFilter.EffectiveDateTo);
        }

        return transactionQuery;
    }
}