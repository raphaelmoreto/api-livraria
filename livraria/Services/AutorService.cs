using AutoMapper;
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

        //public async Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNome)
        //{
        //    Response<CadastrarAutorDto> response = new Response<CadastrarAutorDto>();

        //    try
        //    {
        //        if (string.IsNullOrEmpty(autorNome.Nome))
        //        {
        //            response.Mensagem = "PREENCHIMENTO DO NOME OBRIGATÓRIO";
        //            response.Status = false;
        //            return response;
        //        }

        //        var validarAutor = await _autorRepository.VerificarAutorPorNome(autorNome.Nome);

        //        if (validarAutor)
        //        {
        //            response.Mensagem = "AUTOR JÁ CADASTRADO";
        //            response.Status = false;
        //            return response;
        //        }

        //        var autor = await _autorRepository.InserirAutor(autorNome.Nome);

        //        if (!autor)
        //        {
        //            response.Mensagem = "ERRO AO INSERIR AUTOR";
        //            response.Status = false;
        //            return response;
        //        }

        //        response.Mensagem = "AUTOR CADASTRADO COM SUCESSO";
        //        response.Status = autor;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Mensagem = ex.Message;
        //        response.Status = false;
        //        return response;
        //    }
        //}

        //public async Task<Response<bool>> ExcluirAutor(int idAutor)
        //{
        //    Response<bool> response = new Response<bool>();

        //    try
        //    {
        //        var autorExcluido = await _autorRepository.DeletarAutor(idAutor);

        //        if (!autorExcluido)
        //        {
        //            response.Mensagem = "NÃO FOI POSSÍVEL DELETAR AUTOR";
        //            response.Status = false;
        //            return response;
        //        }

        //        response.Mensagem = "AUTOR EXCLUÍDO";
        //        response.Status = true;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Mensagem = ex.Message;
        //        response.Status = false;
        //        return response;
        //    }
        //}

        //public async Task<Response<ListarAutorPorIdDto>> ObterAutorPorId(int idAutor)
        //{
        //    Response<ListarAutorPorIdDto> response = new Response<ListarAutorPorIdDto>();

        //    try
        //    {
        //        var autor = await _autorRepository.SelecionarAutorPorId(idAutor);

        //        if (autor == null)
        //        {
        //            response.Mensagem = "NENHUM AUTOR LOCALIZADO!";
        //            response.Status = false;
        //            return response;
        //        }

        //        var autorMapeado = _mapper.Map<ListarAutorPorIdDto>(autor);
        //        response.Dados = autorMapeado;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Mensagem = ex.Message;
        //        response.Status = false;
        //        return response;
        //    }
        //}

        //public async Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores()
        //{
        //    Response<IEnumerable<ListarAutoresDto>> response = new Response<IEnumerable<ListarAutoresDto>>();

        //    try
        //    {
        //        var autores = await _autorRepository.SelecionarAutores();

        //        if (!autores.Any())
        //        {
        //            response.Mensagem = "NENHUM AUTOR LOCALIZADO";
        //            response.Status = false;
        //            return response;
        //        }

        //        var autoresMapeado = _mapper.Map<IEnumerable<ListarAutoresDto>>(autores);
        //        response.Dados = autoresMapeado;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Mensagem = ex.Message;
        //        response.Status = false;
        //        return response;
        //    }
        //}
    }
}
