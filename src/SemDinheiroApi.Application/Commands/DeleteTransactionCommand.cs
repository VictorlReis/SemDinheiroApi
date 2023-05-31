using MediatR;

namespace SemDinheiroApi.Commands;

public record DeleteTransactionCommand(int Id) : IRequest<bool>;