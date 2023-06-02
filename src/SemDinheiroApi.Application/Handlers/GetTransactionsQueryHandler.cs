using AutoMapper;
using SemDinheiroApi.Queries;
using SemDinheiroApi.Repositories;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, IEnumerable<GetTransactionsResponse>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public GetTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetTransactionsResponse>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<GetTransactionsResponse>>(await _transactionRepository.GetByUserIdAsync(request.UserId, request.Year, request.Month));
    }
}