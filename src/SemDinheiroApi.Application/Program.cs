using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapPost("/transactions/xp", async (int month, int year, IFormFile file, IMediator mediator) =>
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
                new DateTime(year, month, 01), "xXP", "zXp", valor, userId));
    }

    return Results.Ok(transactions);
});


app.Run();
