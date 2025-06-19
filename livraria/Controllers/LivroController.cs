using Microsoft.AspNetCore.Mvc;
using Dtos.Livro;
using Service.InterfaceLivro;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public async Task<IActionResult> PostLivro(CadastrarLivroDto livro)
        {
            var cadastrarLivro = await _livroService.CadastrarLivro(livro);

            if (cadastrarLivro.Status == false)
            {
                return BadRequest(cadastrarLivro);
            }

            return Ok(cadastrarLivro);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosLivros()
        {
            var livros = await _livroService.BuscarTodosLivros();

            if (livros.Status == false)
            {
                return BadRequest(livros);
            }

            return Ok(livros);
        }
    }
}
