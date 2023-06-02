using MediatR;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Requests;

public record CreateTransactionRequest(string Description, TransactionType Type, 
    DateTime StartDate, string PaymentMethod, string Tag, 
    decimal Value, string UserId) : IRequest<CreateTransactionResponse>;

