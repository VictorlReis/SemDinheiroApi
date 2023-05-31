using MediatR;
using SemDinheiroApi.Databases.Models;

namespace SemDinheiroApi.Commands;

public record CreateTransactionCommand(Transaction Transaction) : IRequest<Transaction>;
