using System.ComponentModel.DataAnnotations.Schema;

namespace Otus.Teaching.Concurrency.Import.Core.Entities;

[Table("customer", Schema = "public")]
public class Customer
{
    [Column("id")]
    public int Id { get; set; }

    [Column("fullname")]
    public string FullName { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("phone")]
    public string Phone { get; set; }
}