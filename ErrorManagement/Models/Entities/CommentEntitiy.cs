using System.ComponentModel.DataAnnotations;
namespace ErrorManagement.Models.Entities;

public class CommentEntitiy
{

    [Key]
    public int Id { get; set; } 

    public string Response { get; set; } = null!;

    public string ResponseTime { get; set; } = null!;



    // should point to a specific errand???
    public ErrandEntity Errand { get; set; } = null!;

    //points to a list of different errands...
    public ICollection<ErrandEntity> Errands = new HashSet<ErrandEntity>();

}
