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
        private readonly AutorRepository _autoRepository;

        public AutorController(AutorRepository autorRepository)
        {
            _autoRepository = autorRepository;
        }

        //O "IActionResult" É UM TIPO GENÉRICO DE RETORNO PARA MÉTODOS DE CONTROLLERS QUE REPRESENTAM QUALQUER TIPO DE RESPOSTA HTTP

        [HttpPost]
        public async Task<IActionResult> CriarAutor([FromBody] Autor autor)
        {
            if (string.IsNullOrEmpty(autor.Nome))
            {
                return BadRequest("NOME DO AUTOR VÁZIO");
            }

            bool insercao = await _autoRepository.InsertAutorAsync(autor);
            if (insercao)
            {
                return Ok();
            }
            else
            {
                return Conflict(); //INDICA QUE A REQUISIÇÃO NÃO PODE SER COMPLETADA DEVIDO A UM CONFLITO
            }
        }
    }
}
