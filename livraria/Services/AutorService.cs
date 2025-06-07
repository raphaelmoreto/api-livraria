using Models;
using Service.InterfaceAutor;
using Repository.InterfaceAutor;

namespace Services
{
    public class AutorService : IAutorService
    {
        private IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<bool> AtualizarAutor(string nomeAutor, int idAutor)
        {
            if (string.IsNullOrEmpty(nomeAutor))
                return false;

            bool verificarNomeAutor = await _autorRepository.SelecionarAutorPorNome(nomeAutor);

            if (verificarNomeAutor)
                return false;

            return await _autorRepository.AtualizarAutor(nomeAutor, idAutor);
        }

        public async Task<bool> CadastrarAutor(string nomeAutor)
        {
            if (string.IsNullOrEmpty(nomeAutor))
                return false;

            bool verificarNomeAutor = await _autorRepository.SelecionarAutorPorNome(nomeAutor);

            if (verificarNomeAutor)
                return false;

            return await _autorRepository.InserirAutor(nomeAutor); ;
        }

        public async Task<bool> ExcluirAutor(int idAutor)
        {
            return await _autorRepository.DeletarAutor(idAutor);
        }

        public async Task<Autor?> ObterAutorPorId(int idAutor)
        {
            var autor = await _autorRepository.SelecionarAutorPorId(idAutor);
            return autor;
        }

        public async Task<IEnumerable<Autor>> ObterTodosAutores()
        {
            List<Autor> list = (await _autorRepository.SelecionarAutores()).ToList();
            return list;
        }
    }
}
