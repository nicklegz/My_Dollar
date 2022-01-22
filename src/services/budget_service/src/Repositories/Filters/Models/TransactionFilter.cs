namespace Repositories.Filters.Models;

public class TransactionFilter
{
    public bool FilterByAmount { get; set; }
    public decimal AmountFrom { get; set; }
    public decimal AmountTo { get; set; }

    public bool FilterByEffectiveDate { get; set; }
    public DateTime EffectiveDateFrom { get; set; }
    public DateTime EffectiveDateTo { get; set; }
}