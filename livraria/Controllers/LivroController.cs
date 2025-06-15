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
                return Ok(cadastrarLivro);
            }

            return Ok(cadastrarLivro);
        }
    }
}
