using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Transaction : BaseEntity<int>
{
    [Column("name")]
    public string Name { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("category")]
    public string Category { get; set; }

    [Column("type")]
    public TransactionType Type { get; set; }

    [Column("effective_date")]
    public DateTime EffectiveDate { get; set; }

    [Column("entry_date")]
    public DateTime EntryDate { get; set; }

    [Column("username")]
    public string Username { get; set; }
    
    public enum TransactionType
    {
        Expense,
        Income
    }   
}