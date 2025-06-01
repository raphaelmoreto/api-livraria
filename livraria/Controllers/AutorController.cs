using Microsoft.AspNetCore.Mvc;
using Models;
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
        public async Task<IActionResult> CriarAutor([FromBody] Autor autor)
        {
            try
            {
                bool cadastro = await _autorService.CadastrarAutor(autor.Nome);
                return cadastro ? Ok("autor cadastrado com sucesso") : Conflict("autor já existe no banco");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosAutores()
        {
            var autores = await _autorService.ObterTodosAutores();
            return Ok(autores);
        }

        [HttpGet("{idAutor}")]
        public async Task<IActionResult> BuscarAutorPorId([FromRoute] int idAutor)
        {
            var autor = await _autorService.ObterAutorPorId(idAutor);
            return autor  != null ? Ok(autor) : NoContent();
        }

        [HttpPut("{idAutor}")]
        public async Task<IActionResult> AtualizarAutor([FromBody] Autor autor, [FromRoute] int idAutor)
        {
            var atualizarAutor = await _autorService.AtualizarAutor(autor.Nome, idAutor);
            return Ok();
        }

        [HttpDelete("{idAutor}")]
        public async Task<IActionResult> DeletarAutor([FromRoute] int idAutor)
        {
            var autorDeletado = await _autorService.ExcluirAutor(idAutor);
            return  NoContent();
        }
    }
}
