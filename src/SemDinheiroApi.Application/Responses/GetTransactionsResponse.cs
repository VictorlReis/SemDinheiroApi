using SemDinheiroApi.Databases.Models.Domain;

namespace SemDinheiroApi.Responses;

public record GetTransactionsResponse(int Id, string Description, TransactionType Type, 
    DateTime StartDate, string PaymentMethod, string Tag, 
    decimal Value, string UserId);