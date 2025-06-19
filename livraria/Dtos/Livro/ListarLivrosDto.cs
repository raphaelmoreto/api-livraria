namespace Dtos.Livro
{
    public class ListarLivrosDto
    {
        public int Id { get; }

        public string Titulo { get; } = string.Empty;

        public DateTime? AnoPublicacao { get; }

        public string Autor { get; } = string.Empty;
    }
}
