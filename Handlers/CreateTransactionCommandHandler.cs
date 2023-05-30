using MediatR;
using SemDinheiroApi.Commands;
using SemDinheiroApi.Databases.Models;
using SemDinheiroApi.Repositories;

namespace SemDinheiroApi.Handlers;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction>
{
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.CreateAsync(request.Transaction);
    }
}