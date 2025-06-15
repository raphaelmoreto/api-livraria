using Dtos.Livro;
using Models;
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

        public async Task<Response<CadastrarLivroDto>> CadastrarLivro(CadastrarLivroDto livro)
        {
            Response<CadastrarLivroDto> response = new Response<CadastrarLivroDto>();

            try
            {
                if (string.IsNullOrEmpty(livro.Titulo))
                {
                    response.Mensagem = "título do livro não pode ser nulo ou vázio";
                    response.Status = false;
                    return response;
                }

                if (livro.IdAutor == null || livro.IdAutor == 0)
                {
                    livro.IdAutor = 1;
                }

                if (livro.AnoPublicacao.HasValue)
                {
                    if (livro.AnoPublicacao > DateTime.Now)
                    {
                        response.Mensagem = "ano de publicação não pode ser maior que a data atual";
                        response.Status = false;
                        return response;
                    }
                }

                var livroCadastrado = await _livroRepository.InserirLivro(livro);

                response.Mensagem = "livro cadastrado com sucesso";
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
    }
}
