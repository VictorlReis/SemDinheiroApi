using MediatR;

namespace SemDinheiroApi.Requests;

public record DeleteTransactionRequest(int Id) : IRequest<bool>;