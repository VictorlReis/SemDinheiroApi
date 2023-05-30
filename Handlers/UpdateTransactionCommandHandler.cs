using MediatR;
using SemDinheiroApi.Commands;
using SemDinheiroApi.Databases.Models;
using SemDinheiroApi.Repositories;

namespace SemDinheiroApi.Handlers;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Transaction>
{
    private readonly ITransactionRepository _transactionRepository;

    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.UpdateAsync(request.Transaction);
    }
}