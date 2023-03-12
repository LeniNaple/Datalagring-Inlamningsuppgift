
namespace ErrorManagement.Models;

public class Errand
{

    public Guid Id { get; set; }

    public string ErrorMessage { get; set; } = null!;

    public int Status { get; set; } 

    public DateTime LogTime { get; set; }


    // Connected to... customer...

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }



    // Jag hade inte tid att fixa VG-delen med kommentarer ;(

    // Connected to... comment... 

    // public string Response { get; set; } = null!;

    // public string ResponseTime { get; set; } = null!;



}
