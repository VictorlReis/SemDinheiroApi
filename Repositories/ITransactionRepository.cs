using SemDinheiroApi.Databases.Models;
namespace SemDinheiroApi.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId);
    Task<Transaction> CreateAsync(Transaction transaction);

    Task<Transaction> UpdateAsync(Transaction transaction);

    Task<bool> DeleteAsync(int id);
}