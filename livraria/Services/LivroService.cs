using Models;
using Dtos.Livro;
using Services;
using Repository.InterfaceLivro;
using Service.InterfaceLivro;

namespace Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<Response<AtualizarLivroDto>> AtualizarLivro(AtualizarLivroDto livroDTO, int idLivro)
        {
            Response<AtualizarLivroDto> response = new Response<AtualizarLivroDto>();

            try
            {
                if (string.IsNullOrWhiteSpace(livroDTO.Titulo))
                    response.Erro("TÍTULO DO LIVRO NÃO PODE SER NULO");

                if (livroDTO.AnoPublicacao.HasValue) //VERIFICAR UMA FORMA DE PODER VIR NULO
                {
                    if (livroDTO.AnoPublicacao > DateTime.Now)
                        response.Erro("ANO DE PUBLICAÇÃO NÃO PODE SER MAIOR QUE A DATA ATUAL");
                }

                if (idLivro is 0)
                    response.Erro("ID NÃO INFORMADO");

                if (response.TemNotificacao())
                    return response;

                var validarLivro = await _livroRepository.VerificarSeExisteLivroPorNome(livroDTO.Titulo);
                if (validarLivro)
                    return response.Erro("LIVRO JÁ CADASTRADO");

                Livro livro = new Livro(livroDTO.Titulo, livroDTO.AnoPublicacao, livroDTO.IdAutor, idLivro);

                var livroAtualizado = await _livroRepository.AtualizarLivro(livro);
                if (!livroAtualizado)
                    return response.Erro("ERRO AO ATUALIZAR LIVRO!");

                return response.Sucesso(livroDTO, "LIVRO ATUALIZADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<IEnumerable<ListarLivrosPorAutor>>> BuscarLivrosPorAutor(string nomeAutor)
        {
            Response<IEnumerable<ListarLivrosPorAutor>> response = new Response<IEnumerable<ListarLivrosPorAutor>>();

            try
            {
                var livrosPorAutor = await _livroRepository.SelecionarLivrosPorAutor(nomeAutor);

                if (livrosPorAutor == null)
                    return response.Sucesso(null, "LIVROS NÃO ENCONTRADO");

                return response.Sucesso(livrosPorAutor, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<ListarLivroPorNome>> BuscarLivroPorNome(string livroNome)
        {
            Response<ListarLivroPorNome> response = new Response<ListarLivroPorNome>();

            try
            {
                var livro = await _livroRepository.SelecionarLivroPorNome(livroNome);

                if (livro == null)
                    return response.Sucesso(null, "LIVRO NÃO ENCONTRADO");

                return response.Sucesso(livro, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<IEnumerable<ListarLivrosDto>>> BuscarTodosLivros()
        {
            Response<IEnumerable<ListarLivrosDto>> response = new Response<IEnumerable<ListarLivrosDto>>();

            try
            {
                var listaLivros = await _livroRepository.SelecionarTodosLivros();

                if (listaLivros == null)
                    return response.Sucesso(null, "LIVROS NÃO ENCONTRADO");

                return response.Sucesso(listaLivros, "BUSCA REALIZADA COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }

        public async Task<Response<CadastrarLivroDto>> CadastrarLivro(CadastrarLivroDto livroDTO)
        {
            Response<CadastrarLivroDto> response = new Response<CadastrarLivroDto>();

            try
            {
                if (string.IsNullOrWhiteSpace(livroDTO.Titulo))
                    response.Erro("TÍTULO DO LIVRO NÃO PODE SER NULO");

                if (livroDTO.AnoPublicacao.HasValue) //VERIFICAR UMA FORMA DE PODER VIR NULO
                {
                    if (livroDTO.AnoPublicacao > DateTime.Now)
                        response.Erro("ANO DE PUBLICAÇÃO NÃO PODE SER MAIOR QUE A DATA ATUAL");
                }

                if (response.TemNotificacao())
                {
                    return response;
                }

                var validarLivro = await _livroRepository.VerificarSeExisteLivroPorNome(livroDTO.Titulo);
                if (validarLivro)
                    return response.Erro("LIVRO JÁ CADASTRADO");

                Livro livro = new Livro(livroDTO.Titulo, livroDTO.AnoPublicacao, livroDTO.IdAutor);

                var livroCadastrado = await _livroRepository.InserirLivro(livro);
                if (!livroCadastrado)
                    return response.Erro("ERRO AO INSERIR LIVRO!");

                return response.Sucesso(livroDTO, "LIVRO CADASTRADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                return response.Erro("ERRO INTERNO: " + ex.Message);
            }
        }
    }
}
