using Financify.Data;
using Financify.DTO;
using Financify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Financify.Controllers;

[ApiController]
[Route("api/metas")]
public class GoalController : ControllerBase
{
    private readonly Database _banco;

    public GoalController(Database banco)
    {
        _banco = banco;
    }

    [HttpGet("listar/todas")]
    public IActionResult ListarTodasMetas()
    {
        try
        {
            var goals = _banco.Goals.Include(g => g.Transactions).ToList();
            return Ok(goals);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter metas: {ex.Message}");
            return BadRequest("Erro ao obter metas.");
        }
    }

    [HttpPatch("buscar/id")]
    public IActionResult BuscarMetaId([FromBody] GoalDTO goalDTO)
    {
        try
        {
            var goal = _banco.Goals.Include(g => g.Transactions).FirstOrDefault(g => g.GoalId == goalDTO.GoalId);
            if (goal == null)
            {
                return NotFound("Meta não encontrada.");
            }
            return Ok(goal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter meta: {ex.Message}");
            return BadRequest("Erro ao obter meta.");
        }
    }

    [HttpPatch("buscar/transactions")]
    public IActionResult BuscarMetaTransactions([FromBody] GoalDTO goalDTO)
    {
        try
        {
            var goal = _banco.Goals.Include(g => g.Transactions).FirstOrDefault(g => g.GoalId == goalDTO.GoalId);
            if (goal == null)
            {
                return NotFound("Meta não encontrada.");
            }

            goalDTO.CurrentValue = goal.InitialValue;

            foreach (var transaction in goal.Transactions)
            {
                goalDTO.CurrentValue += transaction.Amount;
            }

            return Ok(goalDTO);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter meta: {ex.Message}");
            return BadRequest("Erro ao obter meta.");
        }
    }

    [HttpPatch("buscar/ano")]
    public IActionResult BuscarMetaAno([FromBody] GoalDTO goalDTO)
    {
        try
        {
            var goal = _banco.Goals.Include(g => g.Transactions).FirstOrDefault(g => g.StartDate.Year == goalDTO.StartDate.Value.Year);
            if (goal == null)
            {
                return NotFound("Meta não encontrada.");
            }
            return Ok(goal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter meta: {ex.Message}");
            return BadRequest("Erro ao obter meta.");
        }
    }

    [HttpPost("adicionar")]
    public IActionResult AdicionarMeta([FromBody] GoalDTO goalDTO)
    {
        try
        {
            Random randomGen = new();

            var goal = new Goal
            {
                GoalId = randomGen.Next(100000, 1000000),
                Name = goalDTO.Name,
                Description = goalDTO.Description,
                TargetValue = goalDTO.TargetValue ?? 0,
                InitialValue = goalDTO.InitialValue ?? 0,
                StartDate = goalDTO.StartDate ?? DateTime.UtcNow,
                CompletionDate = goalDTO.CompletionDate
            };

            _banco.Goals.Add(goal);
            _banco.SaveChanges();

            return Created("", goal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar meta: {ex.Message}");
            return BadRequest("Erro ao adicionar meta.");
        }
    }

    [HttpPut("atualizar")]
    public IActionResult AtualizarMeta([FromBody] GoalDTO goalDTO)
    {
        try
        {
            var goal = _banco.Goals.Include(g => g.Transactions).FirstOrDefault(g => g.GoalId == goalDTO.GoalId);
            if (goal == null)
            {
                return NotFound("Meta não encontrada.");
            }

            goal.Name = goalDTO.Name;
            goal.Description = goalDTO.Description;
            goal.TargetValue = goalDTO.TargetValue ?? 0;
            goal.InitialValue = goalDTO.InitialValue ?? 0;
            goal.StartDate = goalDTO.StartDate ?? DateTime.UtcNow;
            goal.CompletionDate = goalDTO.CompletionDate;

            _banco.SaveChanges();

            return Ok(goal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar meta: {ex.Message}");
            return BadRequest("Erro ao atualizar meta.");
        }
    }

    [HttpDelete("deletar")]
    public IActionResult DeletarMeta([FromBody] GoalDTO goalDTO)
    {
        try
        {
            var goal = _banco.Goals.Include(g => g.Transactions).FirstOrDefault(g => g.GoalId == goalDTO.GoalId);
            if (goal == null)
            {
                return NotFound("Meta não encontrada.");
            }

            _banco.Goals.Remove(goal);
            _banco.SaveChanges();

            return Ok(goal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao deletar meta: {ex.Message}");
            return BadRequest("Erro ao deletar meta.");
        }
    }
}
