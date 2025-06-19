using AutoMapper;
using Dtos.Autor;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
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

        public async Task<Response<AtualizarAutorDto>> AtualizarAutor(AtualizarAutorDto autorNome, int idAutor)
        {
            Response<AtualizarAutorDto> response = new Response<AtualizarAutorDto>();

            try
            {
                if (string.IsNullOrEmpty(autorNome.Nome))
                {
                    response.Mensagem = "nome do autor nulo/vázio!";
                    response.Status = false;
                    return response;
                }

                var validarAutor = await _autorRepository.SelecionarAutorPorNome(autorNome.Nome);

                if (validarAutor)
                {
                    response.Mensagem = "autor já cadastrado";
                    response.Status = false;
                    return response;
                }

                var autorAtualizado = await _autorRepository.AtualizarAutor(autorNome.Nome, idAutor);

                if (!autorAtualizado)
                {
                    response.Mensagem = "erro ao inserir autor!";
                    response.Status = false;
                    return response;
                }

                response.Mensagem = "autor atualizado com sucesso";
                response.Status = autorAtualizado;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNome)
        {
            Response<CadastrarAutorDto> response = new Response<CadastrarAutorDto>();

            try
            {
                if (string.IsNullOrEmpty(autorNome.Nome))
                {
                    response.Mensagem = "PREENCHIMENTO DO NOME OBRIGATÓRIO";
                    response.Status = false;
                    return response;
                }

                var validarAutor = await _autorRepository.SelecionarAutorPorNome(autorNome.Nome);

                if (validarAutor)
                {
                    response.Mensagem = "AUTOR JÁ CADASTRADO";
                    response.Status = false;
                    return response;
                }

                var autor = await _autorRepository.InserirAutor(autorNome.Nome);

                if (!autor)
                {
                    response.Mensagem = "ERRO AO INSERIR AUTOR";
                    response.Status = false;
                    return response;
                }

                response.Mensagem = "AUTOR CADASTRADO COM SUCESSO";
                response.Status = autor;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<Response<bool>> ExcluirAutor(int idAutor)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var autorExcluido = await _autorRepository.DeletarAutor(idAutor);

                if (!autorExcluido)
                {
                    response.Mensagem = "não foi possível excluir o autor!";
                    response.Status = false;
                    return response;
                }

                response.Mensagem = "autor excluído";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<Response<ListarAutorPorIdDto>> ObterAutorPorId(int idAutor)
        {
            Response<ListarAutorPorIdDto> response = new Response<ListarAutorPorIdDto>();

            try
            {
                var autor = await _autorRepository.SelecionarAutorPorId(idAutor);

                if (autor == null)
                {
                    response.Mensagem = "nenhum autor localizado";
                    response.Status = false;
                    return response;
                }

                var autorMapeado = _mapper.Map<ListarAutorPorIdDto>(autor);
                response.Dados = autorMapeado;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores()
        {
            Response<IEnumerable<ListarAutoresDto>> response = new Response<IEnumerable<ListarAutoresDto>>();

            try
            {
                var autores = await _autorRepository.SelecionarAutores();

                if (!autores.Any())
                {
                    response.Mensagem = "NENHUM AUTOR LOCALIZADO";
                    response.Status = false;
                    return response;
                }

                var autoresMapeado = _mapper.Map<IEnumerable<ListarAutoresDto>>(autores);
                response.Dados = autoresMapeado;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
