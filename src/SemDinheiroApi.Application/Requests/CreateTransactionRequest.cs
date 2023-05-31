using MediatR;
using SemDinheiroApi.Databases.Models.Domain;

namespace SemDinheiroApi.Requests;

public record CreateTransactionRequest(string Description, TransactionType Type, 
    DateTime StartDate, DateTime? EndDate, string PaymentMethod, int Month, string Tag, 
    decimal Value, string UserId) : IRequest<Transaction>;

