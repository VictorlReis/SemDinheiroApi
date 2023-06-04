using System.Globalization;
using AutoMapper;
using MediatR;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Requests;
using SemDinheiroApi.Repositories;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Handlers;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionRequest, UpdateTransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateTransactionResponse> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.UpdateAsync(_mapper.Map<Transaction>(request));
    }
}