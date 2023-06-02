using Microsoft.EntityFrameworkCore;
using SemDinheiroApi.Databases;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetById(int id)
    {
        return await _context.Transactions.FindAsync(id);
    }

    public async Task<IEnumerable<Transaction?>> GetByUserIdAsync(string userId, int year, int month)
    {
        return await _context.Transactions
            .Where(t => t != null && t.UserId == userId && t.StartDate.Year == year && t.StartDate.Month == month)
            .ToListAsync();
    }

    public async Task<CreateTransactionResponse> CreateAsync(Transaction? transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return new CreateTransactionResponse(transaction.Id);
    }

    public async Task<UpdateTransactionResponse?> UpdateAsync(Transaction? transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return new UpdateTransactionResponse(transaction.Id);
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
