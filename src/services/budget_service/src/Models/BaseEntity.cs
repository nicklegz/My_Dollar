using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class BaseEntity<TId>
{
    [Column("id")]
    public int Id { get; set; }
}
