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

        //[HttpGet("buscar-por-nome")]
        //public async Task<IActionResult> GetLivroPorNome([FromQuery] string livroNome)
        //{
        //    var result = await _livroService.BuscarLivroPorNome(livroNome);

        //    if (!result.Status)
        //    {
        //        return Ok(result);
        //    }

        //    return Ok(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetTodosLivros()
        //{
        //    var resultado = await _livroService.BuscarTodosLivros();

        //    if (!resultado.Status)
        //    {
        //        return Ok(resultado);
        //    }

        //    return Ok(resultado);
        //}

        [HttpPost]
        public async Task<IActionResult> PostLivro([FromBody] CadastrarLivroDto livroDTO)
        {
            var resultado = await _livroService.CadastrarLivro(livroDTO);

            if (!resultado.StatusResponse)
            {
                return Conflict(new { erros = resultado.Notificacao });
            }

            return Ok(new
            {
                mensagem = resultado.Notificacao,
                dados = resultado.Dados
            });
        }

        //[HttpPut("{idLivro}")]
        //public async Task<IActionResult> PutLivro([FromBody] AtualizarLivroDto livro, [FromRoute] int idLivro)
        //{
        //    var resultado = await _livroService.AtualizarLivro(livro, idLivro);

        //    if (!resultado.Status)
        //    {
        //        return Ok(resultado);
        //    }

        //    return Ok(resultado);
        //}
    }
}
