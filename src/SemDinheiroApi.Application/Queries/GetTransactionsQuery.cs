using SemDinheiroApi.Databases.Models;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Queries;

using MediatR;

public record GetTransactionsQuery(string UserId) : IRequest<IEnumerable<GetTransactionsResponse>>;