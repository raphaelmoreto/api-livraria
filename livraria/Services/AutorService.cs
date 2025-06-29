using System.Collections.Generic;
using AutoMapper;
using Azure;
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
            try
            {
                if (string.IsNullOrWhiteSpace(autorNomeDTO.Nome))
                    return Response<AtualizarAutorDto>.Erro("NOME DO AUTOR NÃO PODE SER NULO");

                var validarAutor = await _autorRepository.VerificarAutorPorNome(autorNomeDTO.Nome);
                if (validarAutor)
                    return Response<AtualizarAutorDto>.Erro("AUTOR JÁ CADASTRADO");

                var autorAtualizado = await _autorRepository.AtualizarAutor(autorNomeDTO.Nome, idAutor);
                if (!autorAtualizado)
                    return Response<AtualizarAutorDto>.Erro("ERRO AO ATUALIZAR AUTOR");

                return Response<AtualizarAutorDto>.Sucesso(autorNomeDTO, "AUTOR ATUALIZADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return Response<AtualizarAutorDto>.Erro($"ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNomeDTO)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(autorNomeDTO.Nome))
                    return Response<CadastrarAutorDto>.Erro("NOME DO AUTOR NÃO PODE SER NULO");

                var validarAutor = await _autorRepository.VerificarAutorPorNome(autorNomeDTO.Nome);
                if (validarAutor)
                    return Response<CadastrarAutorDto>.Erro("AUTOR JÁ CADASTRADO");

                var autor = await _autorRepository.InserirAutor(autorNomeDTO.Nome);
                if (!autor)
                    return Response<CadastrarAutorDto>.Erro("ERRO AO INSERIR AUTOR");

                return Response<CadastrarAutorDto>.Sucesso(autorNomeDTO, "AUTOR CADASTRADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return Response<CadastrarAutorDto>.Erro($"ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<bool>> ExcluirAutor(int idAutor)
        {
            try
            {
                var autorExcluido = await _autorRepository.DeletarAutor(idAutor);
                if (!autorExcluido)
                    return Response<bool>.Erro("ERRO AO EXCLUIR AUTOR");

                return Response<bool>.Sucesso(true, "AUTOR EXCLUIDO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return Response<bool>.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<ListarAutorPorNomeDto>> ObterAutorPorNome(string autorNome)
        {
            try
            {
                var autor = await _autorRepository.SelecionarAutorPorNome(autorNome);

                if (autor == null)
                    return Response<ListarAutorPorNomeDto>.Sucesso(null, "AUTOR NÃO ENCONTRADO!");

                var autorMapeado = _mapper.Map<ListarAutorPorNomeDto>(autor);
                return Response<ListarAutorPorNomeDto>.Sucesso(autorMapeado, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return Response<ListarAutorPorNomeDto>.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores()
        {
            try
            {
                var autores = await _autorRepository.SelecionarAutores();

                if (!autores.Any())
                    return Response<IEnumerable<ListarAutoresDto>>.Sucesso(null, "AUTORES NÃO ENCONTRADOS");

                var autoresMapeado = _mapper.Map<IEnumerable<ListarAutoresDto>>(autores);
                return Response<IEnumerable<ListarAutoresDto>> .Sucesso(autoresMapeado, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<ListarAutoresDto>>.Erro("ERRO INTERNO: " + ex.Message);
            }
        }
    }
}
