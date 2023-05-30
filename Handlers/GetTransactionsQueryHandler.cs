using SemDinheiroApi.Databases.Models;
using SemDinheiroApi.Queries;
using SemDinheiroApi.Repositories;
using SemDinheiroApi.Repositories;

namespace SemDinheiroApi.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, IEnumerable<Transaction>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.GetByUserIdAsync(request.UserId);
    }
}
