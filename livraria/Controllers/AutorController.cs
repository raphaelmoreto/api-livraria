using Microsoft.AspNetCore.Mvc;
using Dtos.Autor;
using Service.InterfaceAutor;

namespace Controllers
{
    [ApiController] //INDICA QUE A CLASSE É UM CONTROLLER DE API
    [Route("api/[controller]")] //DEFINIÇÃO DA ROTA. "api/" É UM PREFIXO DA ROTA E O "[controller]" É UM PLACEHOLDER QUE SERÁ SUBSTITUÍDO PELO NOME DO CONTROLLER ("api/autor")
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        //O "IActionResult" É UM TIPO GENÉRICO DE RETORNO PARA MÉTODOS DE CONTROLLERS QUE REPRESENTAM QUALQUER TIPO DE RESPOSTA HTTP

        [HttpDelete("{idAutor}")]
        public async Task<IActionResult> DeleteAutor([FromRoute] int idAutor)
        {
            var resultado = await _autorService.ExcluirAutor(idAutor);

            if (!resultado.StatusResponse)
            {
                return Conflict(new { erros = resultado.Notificacao});
            }

            return Ok(new
            {
                mensagem = resultado.Notificacao,
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostAutor([FromBody] CadastrarAutorDto autorNomeDTO)
        {
            var resultado = await _autorService.CadastrarAutor(autorNomeDTO);

            if (!resultado.StatusResponse)
                return Conflict(new { erros = resultado.Notificacao });

            return Ok(new
            {
                mensagem = resultado.Notificacao,
                dados = resultado.Dados
            });
        }

        [HttpGet("por-nome")]
        public async Task<IActionResult> GetAutorPorNome([FromQuery] string autorNome)
        {
            var resultado = await _autorService.ObterAutorPorNome(autorNome);

            if (!resultado.StatusResponse)
                return Conflict(new { erros = resultado.Notificacao});

            return Ok(new
            {
                mensagem = resultado.Notificacao,
                dados = resultado.Dados
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosAutores()
        {
            var resultado = await _autorService.ObterTodosAutores();

            if (!resultado.StatusResponse)
                return Conflict(new { erros = resultado.Notificacao});

            return Ok(new
            {
                mensagem = resultado.Notificacao,
                dados = resultado.Dados
            });
        }

        [HttpPut("{idAutor}")]
        public async Task<IActionResult> PutAutor([FromBody] AtualizarAutorDto autorNomeDTO, [FromRoute] int idAutor)
        {
            var resultado = await _autorService.AtualizarAutor(autorNomeDTO, idAutor);

            if (!resultado.StatusResponse)
                return Conflict(new { erros = resultado.Notificacao });

            return Ok(new
            {
                mensagem = resultado.Notificacao,
                dados = resultado.Dados
            });
        }
    }
}
