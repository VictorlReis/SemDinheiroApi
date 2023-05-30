using MediatR;
using SemDinheiroApi.Databases.Models;

namespace SemDinheiroApi.Commands;

public record UpdateTransactionCommand(Transaction Transaction) : IRequest<Transaction>;