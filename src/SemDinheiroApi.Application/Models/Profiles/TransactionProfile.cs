
using AutoMapper;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Requests;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Models.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, CreateTransactionRequest>()
            .ReverseMap();
        
        CreateMap<Transaction, GetTransactionsResponse>()
            .ReverseMap();
        
        CreateMap<Transaction, UpdateTransactionResponse>()
            .ReverseMap();
    }
}