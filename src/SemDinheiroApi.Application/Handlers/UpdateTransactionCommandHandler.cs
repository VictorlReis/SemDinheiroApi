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

    // public async Task<UpdateTransactionResponse> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
    // {
    //     var existingTransaction = await _transactionRepository.GetById(request.Id);
    //
    //     if (existingTransaction is null)
    //     {
    //         return new UpdateTransactionResponse(0);
    //     }
    //     
    //     var field = char.ToUpper(request.Field[0]) + request.Field[1..];
    //     var property = typeof(Transaction).GetProperty(field);
    //
    //     if (property is null)
    //     {
    //         return new UpdateTransactionResponse(0);
    //     }
    //     
    //     property.SetValue(existingTransaction, request.Value.ToString());
    //
    //     return _mapper.Map<UpdateTransactionResponse>(await _transactionRepository.UpdateAsync(existingTransaction));
    // }
}