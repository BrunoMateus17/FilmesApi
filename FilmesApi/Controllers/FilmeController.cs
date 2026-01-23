using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FilmeController : ControllerBase
  {
    private readonly FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context,IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
      Filme filme = _mapper.Map<Filme>(filmeDto);
      _context.Filmes.Add(filme);
      _context.SaveChanges();
      return CreatedAtAction(nameof(RecuperaFilmePorId),new {id = filme.Id},filme);
    }

    [HttpGet]
    public IEnumerable<ReadFilmeDto> recuperarFilmes([FromQuery]int skip = 0, int take = 50)
    {
      return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take)) ;
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
      var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filme == null) return NotFound();
      var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
      return Ok(filmeDto);
    }
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    { 
      var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmePacial(int id, [FromBody] JsonPatchDocument<UpdateFilmeDto> patch)
    {
      var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<UpdateFilmeDto>(filme);
        patch.ApplyTo(filmeDto, ModelState);
        if (!TryValidateModel(filmeDto))
        {
          return ValidationProblem(ModelState);
        }

      _mapper.Map(filmeDto, filme);
      _context.SaveChanges();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult RemoverFilme(int id)
    {
      var filme = _context.Filmes.FirstOrDefault(u => u.Id == id);

      if (filme != null)
      {
        _context.Filmes.Remove(filme);
        _context.SaveChanges(); // Aqui o Pomelo executa o SQL no MySQL
        return NoContent();
      }
      return NotFound();
    }

  }
}
