using Financify.Data;
using Financify.DTO;
using Financify.Models;
using Microsoft.AspNetCore.Mvc;

namespace Financify.Controllers;

[ApiController]
[Route("api/transacoes")]
public class TransactionController : ControllerBase
{
    private readonly Database _banco;

    public TransactionController(Database banco)
    {
        _banco = banco;
    }

    [HttpGet("listar/todas")]
    public IActionResult ListarTodasTransacoes()
    {
        try
        {
            var transactions = _banco.Transactions.ToList();

            if(transactions == null)
                return BadRequest();

            return Ok(transactions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter as transações: {ex.Message}");
            return BadRequest("Erro ao obter as transações.");
        }
    }

    [HttpPatch("listar/mes")]
    public IActionResult ListarTransacoesMes([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            if(transactionDTO == null)
                return BadRequest();
                
            var transactions = _banco.Transactions.Where(t => t.RegisterDate.Month == transactionDTO.MonthNumber).ToList();
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter as transações: {ex.Message}");
            return BadRequest("Erro ao obter as transações.");
        }
    }

    [HttpPatch("listar/ano")]
    public IActionResult ListarTransacoesAno([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            if(transactionDTO == null)
                return BadRequest();

            var transactions = _banco.Transactions.Where(t => t.RegisterDate.Year == transactionDTO.YearNumber).ToList();
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter as transações: {ex.Message}");
            return BadRequest("Erro ao obter as transações.");
        }
    }

    [HttpPatch("listar/metas")]
    public IActionResult ListarTransacoesMetas([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            if(transactionDTO == null)
                return BadRequest();
                
            var goal = _banco.Goals.FirstOrDefault(g => g.GoalId == transactionDTO.GoalId);
            var transactions = _banco.Transactions.Where(t => t.GoalId == goal.Id).ToList();
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter as transações: {ex.Message}");
            return BadRequest("Erro ao obter as transações.");
        }
    }

    [HttpPatch("listar/categorias")]
    public IActionResult ListarTransacoesCategorias([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            if(transactionDTO == null)
                return BadRequest();

            var category = _banco.Categories.FirstOrDefault(c => c.Name == transactionDTO.CategoryName);
            var transactions = _banco.Transactions.Where(t => t.CategoryName == category.Name).ToList();
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter as transações: {ex.Message}");
            return BadRequest("Erro ao obter as transações.");
        }
    }

    [HttpPatch("buscar/id")]
    public IActionResult BuscarTransacaoId([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            var transaction = _banco.Transactions.FirstOrDefault(t => t.TransactionId == transactionDTO.TransactionId);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter a transação: {ex.Message}");
            return BadRequest("Erro ao obter a transação.");
        }
    }

    [HttpPost("adicionar")]
    public IActionResult AdicionarTransacao([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            Guid guid = Guid.NewGuid();

            var goal = _banco.Goals.FirstOrDefault(g => g.GoalId == transactionDTO.GoalId);
            var category = _banco.Categories.FirstOrDefault(c => c.Name.Equals(transactionDTO.CategoryName));

            var transaction = new Transaction
                {
                    TransactionId = guid,
                    IsExpense = (bool)transactionDTO.IsExpense,
                    Amount = (decimal)transactionDTO.Amount,
                    Category = category,
                    MinimalDescription = transactionDTO.MinimalDescription,
                    RegisterDate = DateTime.UtcNow.ToLocalTime(),
                    Goal = goal
                };

                _banco.Transactions.Add(transaction);
                _banco.SaveChanges();

                return Created("", transaction);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar a transação: {ex.Message}");
            return BadRequest("Erro ao adicionar a transação.");
        }
    }

    [HttpPut("atualizar")]
    public IActionResult AtualizarTransacao([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            var transaction = _banco.Transactions.FirstOrDefault(t => t.TransactionId == transactionDTO.TransactionId);
            if (transaction == null)
                return NotFound();

            var goal = _banco.Goals.FirstOrDefault(g => g.GoalId == transactionDTO.GoalId);
            var category = _banco.Categories.FirstOrDefault(c => c.Name.Equals(transactionDTO.CategoryName));

            transaction.IsExpense = (bool)transactionDTO.IsExpense;
            transaction.Amount = (decimal)transactionDTO.Amount;
            transaction.Category = category;
            transaction.MinimalDescription = transactionDTO.MinimalDescription;
            transaction.RegisterDate = (DateTime)transactionDTO.RegisterDate;
            transaction.Goal = goal;

            _banco.SaveChanges();

            return Ok(transaction);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar a transação: {ex.Message}");
            return BadRequest("Erro ao atualizar a transação.");
        }
    }

    [HttpDelete("deletar")]
    public IActionResult DeleteTransaction([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            var transaction = _banco.Transactions.FirstOrDefault(t => t.TransactionId == transactionDTO.TransactionId);
            if (transaction == null)
                return NotFound();

            _banco.Transactions.Remove(transaction);
            _banco.SaveChanges();

            return Ok(transaction);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao excluir a transação: {ex.Message}");
            return BadRequest("Erro ao excluir a transação.");
        }
    }
}
