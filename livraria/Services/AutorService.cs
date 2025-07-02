using Models;
using Dtos.Autor;
using Repository.InterfaceAutor;
using Service.InterfaceAutor;

namespace Services
{
    public class AutorService : IAutorService
    {
        private IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<Response<AtualizarAutorDto>> AtualizarAutor(AtualizarAutorDto autorNomeDTO, int idAutor)
        {
            Response<AtualizarAutorDto> response = new Response<AtualizarAutorDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(autorNomeDTO.Nome))
                    return response.Erro("NOME DO AUTOR NÃO PODE SER NULO");

                var validarAutor = await _autorRepository.VerificarAutorPorNome(autorNomeDTO.Nome);
                if (validarAutor)
                    return response.Erro("AUTOR JÁ CADASTRADO");

                Autor autor = new Autor(autorNomeDTO.Nome);

                var autorAtualizado = await _autorRepository.AtualizarAutor(autor, idAutor);
                if (!autorAtualizado)
                    return response.Erro("ERRO AO ATUALIZAR AUTOR");

                return response.Sucesso(autorNomeDTO, "AUTOR ATUALIZADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro($"ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNomeDTO)
        {
            Response<CadastrarAutorDto> response = new Response<CadastrarAutorDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(autorNomeDTO.Nome))
                    return response.Erro("NOME DO AUTOR NÃO PODE SER NULO");

                var validarAutor = await _autorRepository.VerificarAutorPorNome(autorNomeDTO.Nome);
                if (validarAutor)
                    return response.Erro("AUTOR JÁ CADASTRADO");

                Autor autor = new Autor(autorNomeDTO.Nome);

                var autorCadastrado = await _autorRepository.InserirAutor(autor);
                if (!autorCadastrado)
                    return response.Erro("ERRO AO INSERIR AUTOR");

                return response.Sucesso(autorNomeDTO, "AUTOR CADASTRADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro($"ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<bool>> ExcluirAutor(int idAutor)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                var autorExcluido = await _autorRepository.DeletarAutor(idAutor);
                if (!autorExcluido)
                    return response.Erro("ERRO AO EXCLUIR AUTOR");

                return response.Sucesso(true, "AUTOR EXCLUIDO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<ListarAutorPorNomeDto>> ObterAutorPorNome(string autorNome)
        {
            Response<ListarAutorPorNomeDto> response = new Response<ListarAutorPorNomeDto>();
            try
            {
                var autor = await _autorRepository.SelecionarAutorPorNome(autorNome);

                if (autor == null)
                    return response.Sucesso(null, "AUTOR NÃO ENCONTRADO!");

                return response.Sucesso(autor, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores()
        {
            Response<IEnumerable<ListarAutoresDto>> response = new Response<IEnumerable<ListarAutoresDto>>();
            try
            {
                var autores = await _autorRepository.SelecionarAutores();

                if (!autores.Any())
                    return response.Sucesso(null, "AUTORES NÃO ENCONTRADOS");

                return response .Sucesso(autores, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }
    }
}
