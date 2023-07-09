using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SemDinheiroApi.Requests;
using SemDinheiroApi.Databases;
using SemDinheiroApi.Databases.Models.Domain;
using SemDinheiroApi.Queries;
using SemDinheiroApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapGet("/transactions/{userId}", async (string userId, int year, int month, IMediator mediator) 
    => await mediator.Send(new GetTransactionsQuery(userId, year, month)));

app.MapPost("/transaction", async (CreateTransactionRequest request, IMediator mediator) 
    => await mediator.Send(request));

app.MapPut("/transaction", async (UpdateTransactionRequest request, IMediator mediator) 
    => await mediator.Send(request));

app.MapDelete("/transaction/{id:int}", async (int id, IMediator mediator) 
    => await mediator.Send(new DeleteTransactionRequest(id)));

app.MapPost("/transactions/csv", async (IFormFile file, IMediator mediator) =>
{
    using var reader = new StreamReader(file.OpenReadStream());

    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ";"
    };

    using var csv = new CsvReader(reader, config);

    var transactions = new List<Transaction>();
    const string userId = "string";

    var csvTransactions = csv.GetRecords<CsvTransaction>();

    foreach (var csvTransaction in csvTransactions)
    {
        var textValue = csvTransaction.Valor.Replace("R$ ", "").Replace(".", "").Replace(',', '.').Trim();
        if (textValue.Contains('-')) continue;

        var valor = decimal.Parse(textValue, CultureInfo.InvariantCulture);
        
        if(valor < 0) continue;

        await mediator.Send(new CreateTransactionRequest(csvTransaction.Estabelecimento, TransactionType.Expense,
                new DateTime(2023, 06, 01), "xp csv", "xp csv", valor, userId));
    }

    return Results.Ok(transactions);
});

app.MapPost("transaction/seed", async (IFormFile file, IMediator mediator) =>
{
    
    if (file.Length <= 0)
    {
        return Results.BadRequest("No file was uploaded.");
    }

    using (var reader = new StreamReader(file.OpenReadStream()))
    {
        while (await reader.ReadLineAsync() is { } line)
        {
            var values = line.Split(',');
            
            var description = values[1];
            var type = int.Parse(values[2]);
            var startDate = DateTime.Parse(values[3]);
            var paymentMethod = values[4];
            var tag = values[5];
            var value = decimal.Parse(values[6]);
            var userId = values[7];

            var transaction = new CreateTransactionRequest(description, (TransactionType)type, startDate, paymentMethod, tag, value, userId);

            await mediator.Send(transaction);
        }
    }
    
    return Results.Ok();
});


app.Run();
