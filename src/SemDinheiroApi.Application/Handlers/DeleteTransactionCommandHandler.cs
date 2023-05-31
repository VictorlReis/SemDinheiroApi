using MediatR;
using SemDinheiroApi.Requests;
using SemDinheiroApi.Repositories;

namespace SemDinheiroApi.Handlers;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionRequest, bool>
{
    private readonly ITransactionRepository _transactionRepository;

    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.DeleteAsync(request.Id);
    }
}