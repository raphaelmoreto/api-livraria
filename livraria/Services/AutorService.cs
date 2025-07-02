using AutoMapper;
using Models;
using Dtos.Autor;
using Repository.InterfaceAutor;
using Service.InterfaceAutor;

namespace Services
{
    public class AutorService : IAutorService
    {
        private IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public AutorService(IAutorRepository autorRepository, IMapper mapper)
        {
            _autorRepository = autorRepository;
            _mapper = mapper;
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

                var autorAtualizado = await _autorRepository.AtualizarAutor(autorNomeDTO, idAutor);
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

                var autor = await _autorRepository.InserirAutor(autorNomeDTO);
                if (!autor)
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

                var autorMapeado = _mapper.Map<ListarAutorPorNomeDto>(autor);
                return response.Sucesso(autorMapeado, "BUSCA REALIZADA COM SUCESSO");
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

                var autoresMapeado = _mapper.Map<IEnumerable<ListarAutoresDto>>(autores);
                return response .Sucesso(autoresMapeado, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }
    }
}
