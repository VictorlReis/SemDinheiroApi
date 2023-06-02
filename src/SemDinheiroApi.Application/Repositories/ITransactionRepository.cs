using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Repositories;

public interface ITransactionRepository
{
    public Task<Transaction?> GetById(int id);
    Task<IEnumerable<Transaction?>> GetByUserIdAsync(string userId, int year, int month);
    Task<CreateTransactionResponse> CreateAsync(Transaction? transaction);

    Task<UpdateTransactionResponse> UpdateAsync(Transaction? transaction);

    Task<bool> DeleteAsync(int id);
}