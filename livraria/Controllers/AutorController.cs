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
            var cadastroAutor = await _autorService.CadastrarAutor(autorNome);

            if (cadastroAutor.Status == false)
            {
                return Conflict(cadastroAutor);
            }

            return Ok(cadastroAutor);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosAutores()
        {
            var autores = await _autorService.ObterTodosAutores();

            if (autores.Status == false)
            {
                return NotFound(autores);
            }

            return Ok(autores);
        }

        [HttpGet("{idAutor}")]
        public async Task<IActionResult> GetAutorPorId([FromRoute] int idAutor)
        {
            var autor = await _autorService.ObterAutorPorId(idAutor);
            
            if (autor.Status == false)
            {
                return NotFound(autor);
            }

            return Ok(autor);
        }

        [HttpPut("{idAutor}")]
        public async Task<IActionResult> AtualizarAutor([FromBody] AtualizarAutorDto autorNome, [FromRoute] int idAutor)
        {
            var autorAtualizado = await _autorService.AtualizarAutor(autorNome, idAutor);

            if (autorAtualizado.Status == false)
            {
                return Conflict(autorAtualizado);
            }

            return Ok(autorAtualizado);
        }

        [HttpDelete("{idAutor}")]
        public async Task<IActionResult> DeletAutor([FromRoute] int idAutor)
        {
            var autorDeletado = await _autorService.ExcluirAutor(idAutor);

            if (autorDeletado.Status == false)
            {
                return Conflict(autorDeletado);
            }

            return Ok(autorDeletado);
        }
    }
}
