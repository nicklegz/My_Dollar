using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class User
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("username")]
    [Required]
    public string Username { get; set; }
    
    [Column("password")]
    [Required]
    public string Password { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
}