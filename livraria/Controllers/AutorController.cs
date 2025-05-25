using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorys;

namespace Controllers
{
    [ApiController] //INDICA QUE A CLASSE É UM CONTROLLER DE API
    [Route("api/[controller]")] //DEFINIÇÃO DA ROTA. "api/" É UM PREFIXO DA ROTA E O "[controller]" É UM PLACEHOLDER QUE SERÁ SUBSTITUÍDO PELO NOME DO CONTROLLER ("api/autor")
    public class AutorController : ControllerBase
    {
        private readonly AutorRepository _autorRepository;

        public AutorController(AutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        //O "IActionResult" É UM TIPO GENÉRICO DE RETORNO PARA MÉTODOS DE CONTROLLERS QUE REPRESENTAM QUALQUER TIPO DE RESPOSTA HTTP

        [HttpPost]
        public async Task<ActionResult> CriarAutor([FromBody] Autor autor)
        {
            if (string.IsNullOrEmpty(autor.Nome))
            {
                return BadRequest("NOME DO AUTOR VÁZIO");
            }

            bool insercao = await _autorRepository.InsertAutorAsync(autor);

            if (insercao)
            {
                return Ok();
            }
            else
            {
                return Conflict(); //INDICA QUE A REQUISIÇÃO NÃO PODE SER COMPLETADA DEVIDO A UM CONFLITO
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> BuscarTodosAutores()
        {
            var autores = (await _autorRepository.BuscarAutores()).ToList();

            if (autores is null || autores.Count == 0)
                return NoContent();

            return Ok(autores);
        }
    }
}
