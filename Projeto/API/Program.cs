using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>(); 
var app = builder.Build();

app.MapGet("/", () => "API de Folha de Pagamentos");

app.MapPost("/api/funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDataContext ctx) =>
{
    var funcionarioExistente = ctx.Funcionarios.FirstOrDefault(f => f.CPF == funcionario.CPF);

    if (funcionarioExistente != null)
    {
        return Results.BadRequest($"Funcionário com CPF {funcionario.CPF} já cadastrado.");
    }

    ctx.Funcionarios.Add(funcionario);
    ctx.SaveChanges();
    return Results.Created("", funcionario);
});


app.MapGet("/api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Funcionarios.Any())
    {
        return Results.Ok(ctx.Funcionarios.ToList());
    }

    return Results.NotFound();
});

app.MapPost("/api/folha/cadastrar", async ([FromBody] Folha folha, [FromServices] AppDataContext ctx) =>
{
    var funcionario = await ctx.Funcionarios.FindAsync(folha.FuncionarioId);
    if (funcionario == null)
    {
        return Results.NotFound($"Funcionário com ID {folha.FuncionarioId} não encontrado.");
    }

    decimal salarioBruto =  1500;
    decimal desconto = salarioBruto * 0.1m; // exemplo: 10% de desconto
    decimal salarioLiquido = salarioBruto - desconto;

    folha.SalarioBruto = salarioBruto;
    folha.Descontos = desconto;
    folha.SalarioLiquido = salarioLiquido;

    ctx.Folhas.Add(folha);
    await ctx.SaveChangesAsync();

    return Results.Created($"/api/folha/{folha.Id}", folha);
});


app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Folhas.Any())
    {
        return Results.Ok(ctx.Folhas.Include(f => f.Funcionario).ToList());
    }

    return Results.NotFound();
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", ([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano, [FromServices] AppDataContext ctx) =>
{
    var folha = ctx.Folhas
        .Include(f => f.Funcionario)
        .FirstOrDefault(f => f.Funcionario.CPF == cpf && f.Data.Month == mes && f.Data.Year == ano);
        
    if (folha == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(folha);
});

app.Run();
