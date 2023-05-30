using SemDinheiroApi.Databases.Models;
namespace SemDinheiroApi.Queries;

using MediatR;

public record GetTransactionsQuery(string UserId) : IRequest<IEnumerable<Transaction>>;
