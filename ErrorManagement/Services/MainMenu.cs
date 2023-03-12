using ErrorManagement.Contexts;
using ErrorManagement.Models;

namespace ErrorManagement.Services;

internal class MainMenu
{
    private static DataContext _context = new DataContext();
    public List<Errand> Errands = new List<Errand>();


    public async Task CustomerChoice()
    {
        Console.Clear();
        Console.WriteLine("Welcome customer!");
        Console.WriteLine("1: Register new error.");
        Console.WriteLine("2: View details on error.");
        Console.WriteLine("3: View all errors, in database.");
        Console.WriteLine("4: Change an error.");
        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                Console.Clear();
                await LogErrorAsync();
                Console.ReadKey();
                break;

            case "2":
                Console.Clear();
                await ViewErrorDetailsAsync();
                Console.ReadKey();
                break;

            case "3":
                Console.Clear();
                await ViewAllErrorsAsync();
                Console.ReadKey();
                break;

            case "4":
                Console.Clear();
                await ChangeErrorAsync();
                Console.ReadKey();
                break;

            default:
                Console.Clear();
                Console.WriteLine("Not a valid choice.");
                Console.ReadKey();
                break;
        }

    }

    public async Task EmployeeChoice()
    {
        Console.Clear();
        Console.WriteLine("Welcome employee!");
        Console.WriteLine("1: View all errands.");
        Console.WriteLine("2: View detailed errand.");
        Console.WriteLine("3: Change status on errand.");
        Console.WriteLine("4: Delete an errand from database.");
        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                Console.Clear();
                await ViewAllErrorsAsync();
                break;

            case "2":
                Console.Clear();
                await ViewErrorDetailsAsync();
                break;

            case "3":
                Console.Clear();
                await ChangeErrorStatusAsync();
                break;

            case "4":
                Console.Clear();
                await DeleteErrorAsync();
                break;

            default:
                Console.Clear();
                Console.WriteLine("Not a valid choice.");
                Console.ReadKey();
                break;
        }

    }


    private async Task LogErrorAsync() //Saves new errand to database... 
    {
        Errand errand = new Errand();

        Console.WriteLine("You have chosen to let us know of an error:");
        Console.WriteLine("Enter your name: ");
        errand.Name = Console.ReadLine() ?? "";

        Console.WriteLine("Enter your email: ");
        errand.Email = Console.ReadLine() ?? "";

        Console.WriteLine("Enter your phonenumber (optional): ");
        errand.PhoneNumber = Console.ReadLine();

        Console.WriteLine("Describe the error that has occured: ");
        errand.ErrorMessage = Console.ReadLine() ?? "";

        errand.Status = 1;
        errand.LogTime = DateTime.Now;

        await CustomerService.SaveAsync(errand);
        Console.WriteLine("Your errand have been logged, we will be in touch.");
        Console.ReadKey();
    }

    private async Task ViewAllErrorsAsync() //Loads all errands from database, and displays them...
    {
        var errands = await CustomerService.GetAllAsync();

        if (errands.Any())
        {
            Console.WriteLine("All errands in database: ");
            Console.WriteLine("");
            int nr = 0;
            foreach (Errand errand in errands)
            {
                nr++;
                Console.WriteLine($"Comment number: {nr}. ");
                Console.WriteLine($"Handling number: {errand.Id}");
                Console.WriteLine($"Customer: {errand.Name}");

                if (errand.Status == 0)
                {
                    Console.WriteLine("ERRAND STATUS: - Changed by customer, needs reassignment.");
                }
                else if (errand.Status == 1)
                {
                    Console.WriteLine("ERRAND STATUS: - Not assigned.");
                }
                else if (errand.Status == 2)
                {
                    Console.WriteLine("ERRAND STATUS: - Ongoing!");
                }
                else
                {
                    Console.WriteLine("ERRAND STATUS: - Completed!");
                }

                Console.WriteLine($"Error: {errand.ErrorMessage}");
                Console.WriteLine("");

            }

            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("There are no errors in database!");
            Console.WriteLine("");
            Console.ReadKey();

        }

    }

    private async Task ViewErrorDetailsAsync() //Loads one errand... 
    {
        Console.WriteLine("Search errand by handling number: ");
        var errandNumber = Console.ReadLine();

        if (!string.IsNullOrEmpty(errandNumber))
        {

            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    Console.WriteLine($"Handling number: {_errand.Id}");
                    Console.WriteLine($"Customer information: {_errand.Name} {_errand.Email} {_errand.PhoneNumber} ");
                    Console.WriteLine($"Logged by customer: {_errand.LogTime} Errand status: {_errand.Status}");
                    Console.WriteLine($"Error {_errand.ErrorMessage}");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("There is no errand with that handling number.");
                    Console.ReadKey();
                }
            } 
            catch
            {
                Console.WriteLine("Not a valid entry.");
                Console.ReadKey();
            }


        } else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }
 

        
    }

    private async Task ChangeErrorStatusAsync()
    {
        Console.WriteLine("Change status. ");
        Console.WriteLine("Search errand by handling number: ");
        var errandNumber = Console.ReadLine();
        if (!string.IsNullOrEmpty(errandNumber))
        {
            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    Console.WriteLine($"Errand: {_errand.Id} has been found.");
                    Console.WriteLine("1: Change status to: Ongoing");
                    Console.WriteLine("2: Change status to: Completed.");
                    Console.WriteLine("3: Change status back to: Unassigned");
                    string choice = Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        case "1":
                            _errand.Status = 2;
                            Console.WriteLine("Set to ONGOING");
                            Console.ReadKey();
                            break;
                        case "2":
                            _errand.Status = 3;
                            Console.WriteLine("Set to COMPLETED");
                            Console.ReadKey();
                            break;
                        case "3":
                            _errand.Status = 1;
                            Console.WriteLine("Set to UNASSIGNED");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Not a valid choice");
                            Console.ReadKey();
                            break;

                    }
                    await CustomerService.UpdateAsync(_errand);
                }
                else
                {
                    Console.WriteLine("There is no errand with that handling number.");
                    Console.ReadKey();
                }
            }
            catch
            {
                Console.WriteLine("Not a valid entry.");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }
    }

    private async Task ChangeErrorAsync()
    {
        Console.WriteLine("Change errand. ");
        Console.WriteLine("Search errand by handling number: ");
        var errandNumber = Console.ReadLine();

        if (!string.IsNullOrEmpty(errandNumber))
        {
            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    Console.WriteLine($"Errand: {_errand.Id} has been found.");
                    Console.WriteLine("Fill in the new information.");
                    Console.WriteLine("Name: ");
                    _errand.Name = Console.ReadLine() ?? "";
                    Console.WriteLine("Email: ");
                    _errand.Email = Console.ReadLine() ?? "";
                    Console.WriteLine("PhoneNumber: ");
                    _errand.PhoneNumber = Console.ReadLine();
                    Console.WriteLine("ErrorMessage: ");
                    _errand.ErrorMessage = Console.ReadLine() ?? "";

                    _errand.Status = 0;

                    await CustomerService.UpdateAsync(_errand);
                }
                else
                {
                    Console.WriteLine("There is no errand with that handling number.");
                    Console.ReadKey();
                }
            }
            catch
            {
                Console.WriteLine("Not a valid entry.");
                Console.ReadKey();
            }


        }
        else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }


    }

    private async Task DeleteErrorAsync()
    {
        Console.WriteLine("To delete an errand from database. ");
        Console.WriteLine("Search errand by handling number: ");
        var errandNumber = Console.ReadLine();

        if (!string.IsNullOrEmpty(errandNumber))
        {

            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    
                    Console.WriteLine($"Do you want to delete the following comment?");
                    Console.WriteLine($"Customer number {_errand.Id}");
                    Console.WriteLine($"Error {_errand.ErrorMessage}");
                    Console.WriteLine($"");
                    Console.WriteLine($"Y for yes N for no");
                    string choice = Console.ReadLine() ?? "";

                    if (choice == "Y")
                    {
                        await CustomerService.DeleteAsync(errandNr);
                        Console.WriteLine("Deleted from database!");
                        Console.ReadKey();

                    }
                    else if (choice == "N")
                    {
                        Console.WriteLine("Nothing has been deleted.");
                        Console.ReadKey();

                    } else
                    {
                        Console.WriteLine("Not a valid choice!");
                        Console.ReadKey();

                    }
                }
                else
                {
                    Console.WriteLine("There is no errand with that handling number.");
                    Console.ReadKey();

                }
            }
            catch
            {
                Console.WriteLine("Not a valid entry.");
                Console.ReadKey();

            }
        }
        else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();

        }
        Console.ReadKey();

    }


}
