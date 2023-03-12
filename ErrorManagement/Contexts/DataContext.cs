using ErrorManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace ErrorManagement.Contexts;

internal class DataContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\1\Desktop\Inlamning_databaser\ErrorManagement\ErrorManagement\Contexts\sql_db.mdf;Integrated Security=True;Connect Timeout=30";

    //communication between models and database

    #region Constructors

    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    #endregion


    #region Overrides

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) 
        { 
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    #endregion


    #region Entities

    // public DbSet<CommentEntitiy> Comments { get; set; } = null!;

    public DbSet<CustomerEntity> Customers { get; set; } = null!;

    public DbSet<ErrandEntity> Errands { get; set; } = null!;





    #endregion

}
