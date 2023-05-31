using SemDinheiroApi.Databases.Models;
using SemDinheiroApi.Databases.Models.Domain;

namespace SemDinheiroApi.Responses;

public record GetTransactionsResponse(int Id, string Description, TransactionType Type, 
    DateTime StartDate, DateTime? EndDate, string PaymentMethod, int Month, string Tag, 
    decimal Value, string UserId);