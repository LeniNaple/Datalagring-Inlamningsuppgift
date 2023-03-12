using ErrorManagement.Contexts;
using ErrorManagement.Models;
using ErrorManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorManagement.Services;

internal class CustomerService
{

    private static DataContext _context = new DataContext();

    public static async Task SaveAsync(Errand errand)
    {
        var _errandEntity = new ErrandEntity
        {
            Id = errand.Id,
            ErrorMessage = errand.ErrorMessage,
            Status = errand.Status,
            LogTime = errand.LogTime,
        };

        var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Name == errand.Name && x.Email == errand.Email);
        if (_customerEntity != null)
        {
            _errandEntity.CustomerId = _customerEntity.Id;
        }
        else
            _errandEntity.Customer = new CustomerEntity
            {
                Name = errand.Name,
                Email = errand.Email,
                PhoneNumber = errand.PhoneNumber,
            };
        _context.Add(_errandEntity);
        await _context.SaveChangesAsync();

    }

    public static async Task<IEnumerable<Errand>> GetAllAsync()
    {
        var _errands = new List<Errand>();

        foreach (var _errand in await _context.Errands.Include(x => x.Customer).ToListAsync())
            _errands.Add(new Errand() {
                Id = _errand.Id,
                ErrorMessage = _errand.ErrorMessage,
                Status = _errand.Status,
                LogTime = _errand.LogTime,
                Name = _errand.Customer.Name,
                Email = _errand.Customer.Email,
                PhoneNumber = _errand.Customer.PhoneNumber,
            });

        return _errands;
    }

    public static async Task<Errand> GetAsync(Guid errandNumber)
    {
        var _errand = await _context.Errands.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == errandNumber);
        if (_errand != null)
        {
            return new Errand()
            {
                Id = _errand.Id,
                ErrorMessage = _errand.ErrorMessage,
                Status = _errand.Status,
                LogTime = _errand.LogTime,
                Name = _errand.Customer.Name,
                Email = _errand.Customer.Email,
                PhoneNumber = _errand.Customer.PhoneNumber,
            };
        }
        else {
            return null!;
        }

    }


    public static async Task UpdateAsync(Errand errand)
    {
        var _errand = await _context.Errands.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == errand.Id);
        if (_errand != null)
        {
            if (!string.IsNullOrEmpty(errand.ErrorMessage))
                _errand.ErrorMessage = errand.ErrorMessage;
            _errand.Status = errand.Status;

            if (!string.IsNullOrEmpty(errand.Name) || !string.IsNullOrEmpty(errand.Email))
            {
                var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Name == errand.Name && x.Email == errand.Email && x.PhoneNumber == errand.PhoneNumber);
                if (_customerEntity != null)
                {
                    _errand.CustomerId = _customerEntity.Id;
                }
                else
                {
                    _errand.Customer = new CustomerEntity
                    {
                        Name = errand.Name,
                        Email = errand.Email,
                        PhoneNumber = errand.PhoneNumber,
                    };
                }
            }
            _context.Update(_errand);
            await _context.SaveChangesAsync();
        }
    }


    public static async Task DeleteAsync(Guid errandNumber)
    {
        var _errand = await _context.Errands.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == errandNumber);
        if (_errand != null)
        {
            _context.Remove(_errand);
            await _context.SaveChangesAsync();
        }
        await _context.SaveChangesAsync();
    }

    
}
