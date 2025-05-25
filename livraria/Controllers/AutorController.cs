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
            var autores = (await _autorRepository.GetAutoresAsync()).ToList();

            if (autores is null || autores.Count == 0)
                return NoContent();

            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> BuscarAutoresPorId(int id)
        {
            var autor = await _autorRepository.GetPorIdAsync(id);

            if (autor is null)
                return NoContent();

            return Ok(autor);
        }

        [HttpPut("{idAutor}")]
        public async Task<ActionResult<bool>> AtualizarAutor([FromBody] Autor autor, int idAutor)
        {
            var autorAtualizado = await _autorRepository.PutAutorAsync(autor, idAutor);

            if (!autorAtualizado)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{idAutor}")]
        public async Task<ActionResult<bool>> DeletarAutor(int idAutor)
        {
            var autorDeletado = await _autorRepository.DeleteAutorAsync(idAutor);

            if (!autorDeletado)
                return BadRequest();

            return Ok();
        }
    }
}
