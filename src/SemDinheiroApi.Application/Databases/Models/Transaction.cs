﻿namespace SemDinheiroApi.Databases.Models;

public enum TransactionType
{
    Expense,
    Recipe
}

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Tag { get; set; } 
    public decimal Value { get; set; }
    public string UserId { get; set; } // Este é o ID do usuário do provedor OAuth
}