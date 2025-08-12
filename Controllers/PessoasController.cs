using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PessoasController : ControllerBase
  {
    private readonly PessoasService _pessoasService;
    public PessoasController(PessoasService pessoasService)
    {
      _pessoasService = pessoasService;
    }
    [HttpGet]
    public async Task<List<Pessoa>> Get() =>
   await _pessoasService.GetAsync();

    [HttpPost]
    public async Task<IActionResult> Post(PessoasController newPessoa)
    {
    await _pessoasService.CreateAsync(newPessoa);
    return CreatedAtAction(nameof(Get), new { id = newPessoa.Id }, newPessoa);
   }
  }
}