using MediatR;
using SemDinheiroApi.Commands;
using SemDinheiroApi.Databases.Models;
using SemDinheiroApi.Repositories;

namespace SemDinheiroApi.Handlers;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly ITransactionRepository _transactionRepository;

    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.DeleteAsync(request.Id);
    }
}