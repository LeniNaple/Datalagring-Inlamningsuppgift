using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
namespace ErrorManagement.Models.Entities;

public class ErrandEntity
{

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string ErrorMessage { get; set; } = null!;

    [Required]
    public int Status { get; set; }

    [Required]
    [ConcurrencyCheck]
    public DateTime LogTime { get; set; } 


    //Connecting customer...
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;


    //Connecting comment...?
   // public int? CommentId { get; set; }
   // public CommentEntitiy Comment { get; set; } = null!;


}
