namespace Dtos.Livro
{
    public class ListarLivrosPorAutor
    {
        public string Titulo { get; set; } = string.Empty;

        public DateTime? AnoPublicacao { get; set; }

        public string? Autor { get; set; } = string.Empty;
    }
}
