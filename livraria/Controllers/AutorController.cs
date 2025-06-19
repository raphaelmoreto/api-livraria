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

        [HttpPost]
        public async Task<IActionResult> PostAutor([FromBody] CadastrarAutorDto autorNome)
        {
            var resultado = await _autorService.CadastrarAutor(autorNome);

            if (!resultado.Status)
            {
                return Ok(resultado.Mensagem);
            }

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosAutores()
        {
            var resultado = await _autorService.ObterTodosAutores();

            if (resultado.Status == false)
            {
                return Ok(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("{idAutor}")]
        public async Task<IActionResult> GetAutorPorId([FromRoute] int idAutor)
        {
            var resultado = await _autorService.ObterAutorPorId(idAutor);
            
            if (resultado.Status == false)
            {
                return Ok(resultado);
            }

            return Ok(resultado);
        }

        [HttpPut("{idAutor}")]
        public async Task<IActionResult> AtualizarAutor([FromBody] AtualizarAutorDto autorNome, [FromRoute] int idAutor)
        {
            var resultado = await _autorService.AtualizarAutor(autorNome, idAutor);

            if (resultado.Status == false)
            {
                return Ok(resultado);
            }

            return Ok(resultado);
        }

        [HttpDelete("{idAutor}")]
        public async Task<IActionResult> DeletAutor([FromRoute] int idAutor)
        {
            var resultado = await _autorService.ExcluirAutor(idAutor);

            if (resultado.Status == false)
            {
                return Ok(resultado);
            }

            return Ok(resultado);
        }
    }
}
