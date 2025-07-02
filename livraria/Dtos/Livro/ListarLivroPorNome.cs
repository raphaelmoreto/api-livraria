namespace Dtos.Livro
{
    public class ListarLivroPorNome
    {
        public string Titulo { get; set; } = string.Empty;

        public DateTime? AnoPublicacao { get; set; }

        public string? Autor { get; set; } = string.Empty;
    }
}
