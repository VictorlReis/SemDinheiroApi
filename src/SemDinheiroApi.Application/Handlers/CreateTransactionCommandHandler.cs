using AutoMapper;
using MediatR;
using SemDinheiroApi.Requests;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Repositories;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Handlers;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionRequest, CreateTransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<CreateTransactionResponse> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.CreateAsync(_mapper.Map<Transaction>(request));
    }
}
