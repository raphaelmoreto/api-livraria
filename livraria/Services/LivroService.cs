using AutoMapper;
using Dtos.Livro;
using Models;
using Repository.InterfaceLivro;
using Service.InterfaceLivro;

namespace Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;

        public LivroService(ILivroRepository livroRepository, IMapper mapper)
        {
            _livroRepository = livroRepository;
            _mapper = mapper;
        }

        public async Task<Response<CadastrarLivroDto>> CadastrarLivro(CadastrarLivroDto livro)
        {
            Response<CadastrarLivroDto> response = new Response<CadastrarLivroDto>();

            try
            {
                if (string.IsNullOrEmpty(livro.Titulo))
                {
                    response.Mensagem = "TÍTULO DO LIVRO NÃO PODE SER NULO/VÁZIO";
                    response.Status = true;
                    return response;
                }

                if (livro.IdAutor == null || livro.IdAutor == 0)
                {
                    livro.IdAutor = 1;
                }

                if (livro.AnoPublicacao.HasValue) //VERIFICAR UMA FORMA PARA A DATA VIR NULA
                {
                    if (livro.AnoPublicacao > DateTime.Now)
                    {
                        response.Mensagem = "ANO DE PUBLICAÇÃO NÃO PODE SER MAIOR QUE A DATA ATUAL";
                        response.Status = true;
                        return response;
                    }
                }

                var livroCadastrado = await _livroRepository.InserirLivro(livro);

                response.Mensagem = "LIVRO CADASTRADO";
                response.Status = livroCadastrado;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<Response<IEnumerable<ListarLivrosDto>>> BuscarTodosLivros()
        {
            Response<IEnumerable<ListarLivrosDto>> response = new Response<IEnumerable<ListarLivrosDto>>();

            try
            {
                var listaLivros = await _livroRepository.SelecionarTodosLivros();

                if (listaLivros == null)
                {
                    response.Mensagem = "NENHUM LIVRO ENCONTRADO!";
                    response.Status = true;
                    return response;
                }

                var livrosMapeado = _mapper.Map<IEnumerable<ListarLivrosDto>>(listaLivros);
                response.Dados = livrosMapeado;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public Task<Response<ListarLivroPorNome>> BuscarLivroPorNome(string livroNome)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<ListarLivrosPorAutor>>> BuscarLivrosPorAutor(string nomeAutor)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<AtualizarLivroDto>> AtualizarLivro(AtualizarLivroDto livro, int idLivro)
        {
            Response<AtualizarLivroDto> response = new Response<AtualizarLivroDto>();

            try
            {
                if (string.IsNullOrEmpty(livro.Titulo))
                {
                    response.Mensagem = "TÍTULO NÃO PODE SER NULO/VÁZIO";
                    response.Status = false;
                    return response;
                }

                if (livro.AnoPublicacao.HasValue)
                {
                    if (livro.AnoPublicacao > DateTime.Now)
                    {
                        response.Mensagem = "DATA DE PUBLICAÇÃO DO LIVRO NÃO PODE SER MAIOR QUE A DATA ATUAL";
                        response.Status = false;
                        return response;
                    }
                }

                var verificarLivro = await _livroRepository.VerificarSeExisteLivroPorNome(livro.Titulo);

                if (verificarLivro)
                {
                    response.Mensagem = "LIVRO JÁ CADASTRADO";
                    response.Status = false;
                    return response;
                }

                var livroAtualizado = await _livroRepository.AtualizarLivro(livro, idLivro);

                if (!livroAtualizado)
                {
                    response.Mensagem = "ERRO AO ATUALIZAR LIVRO";
                    response.Status = false;
                    return response;
                }

                response.Mensagem = "LIVRO ATUALIZADO";
                response.Status = livroAtualizado;
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
