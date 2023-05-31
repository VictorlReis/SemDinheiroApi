using Microsoft.EntityFrameworkCore;
using SemDinheiroApi.Databases;
using SemDinheiroApi.Databases.Models.Domain;

namespace SemDinheiroApi.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId)
    {
        return await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) 
        {
            return false;
        }
        
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}
