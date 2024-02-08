using Financify.Data;
using Financify.DTO;
using Financify.Models;
using Microsoft.AspNetCore.Mvc;

namespace Financify.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoryController : ControllerBase
{
    private readonly Database _banco;

    public CategoryController(Database banco)
    {
        _banco = banco;
    }

    [HttpPatch("buscar")]
    public IActionResult BuscarCategoriaNome([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var category = _banco.Categories.FirstOrDefault(c => c.Name.Equals(categoryDTO.Name));

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter a categoria: {ex.Message}");
            return BadRequest("Erro ao obter a categoria.");
        }
    }

    [HttpPost("adicionar")]
    public IActionResult AdicionarCategoria([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            Category category = new()
            {
                Name = categoryDTO.Name,
                ColorHEX = categoryDTO.ColorHEX
            };

            _banco.Categories.Add(category);
            _banco.SaveChanges();

            return Created("", category);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar a categoria: {ex.Message}");
            return BadRequest("Erro ao adicionar a categoria.");
        }
    }

    [HttpPut("atualizar")]
    public IActionResult AtualizarCategoria([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var category = _banco.Categories.FirstOrDefault(c => c.Name.Equals(categoryDTO.Name));

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDTO.Name;
            category.ColorHEX = categoryDTO.ColorHEX;

            _banco.Categories.Update(category);
            _banco.SaveChanges();

            return Ok(category);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter a categoria: {ex.Message}");
            return BadRequest("Erro ao obter a categoria.");
        }
    }

    [HttpDelete("deletar")]
    public IActionResult DeletarCategoria([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var category = _banco.Categories.FirstOrDefault(c => c.Name.Equals(categoryDTO.Name));

            if (category == null)
            {
                return NotFound();
            }

            _banco.Categories.Remove(category);
            _banco.SaveChanges();

            return Ok(category);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter a categoria: {ex.Message}");
            return BadRequest("Erro ao obter a categoria.");
        }
    }

}