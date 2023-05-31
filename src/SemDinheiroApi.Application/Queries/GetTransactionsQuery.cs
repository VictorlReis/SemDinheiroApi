using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Queries;

using MediatR;

public record GetTransactionsQuery(string UserId) : IRequest<IEnumerable<GetTransactionsResponse>>;