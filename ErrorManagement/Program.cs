using ErrorManagement.Services;

MainMenu menu = new MainMenu();


while (true)
{
    Console.Clear();
    Console.WriteLine("Welcome to Error handling system, console version!");
    Console.WriteLine("1: For customers.");
    Console.WriteLine("2: For employees.");
    var choice = Console.ReadLine();

    switch (choice)
    {

        case "1":
            await menu.CustomerChoice();
            break;

        case "2":
            await menu.EmployeeChoice();
            break;

        default:
            Console.Clear();
            Console.WriteLine("Not a valid choice.");
            Console.ReadKey();
            break;

    }
}
