using Microsoft.AspNetCore.Mvc;
using Models;
using Interfaces;

namespace livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;

        public LivroController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CriarLivro([FromBody] Livro livro)
        {
            if (string.IsNullOrEmpty(livro.Titulo))
            {
                return BadRequest("LIVRO SEM TÍTULO");
            }

            if (livro.AnoPublicacao == null)
            {
                return BadRequest("LIVRO SEM ANO DE PUBLICAÇÃO");
            }

            if (livro.IdAutor == null)
            {
                return BadRequest("LIVRO SEM AUTOR VINCULADO");
            }

            try
            {
                bool insercao = await _livroRepository.InsertLivroAsync(livro);

                if (insercao)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> BuscarTodosLivros()
        {
            try
            {
                var livros = (await _livroRepository.GetLivrosAsync()).ToList();

                if (livros is null || livros.Count == 0)
                    return NoContent();

                return Ok(livros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
