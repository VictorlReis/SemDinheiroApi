using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Queries;

using MediatR;

public record GetTransactionsQuery(string UserId, int Year, int Month) : IRequest<IEnumerable<GetTransactionsResponse>>;