using MediatR;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Responses;

namespace SemDinheiroApi.Requests;

public record UpdateTransactionRequest(Transaction Transaction) : IRequest<UpdateTransactionResponse>;