namespace Dtos.Livro
{
    public class CadastrarLivroDto
    {
        public string Titulo { get; set; } = string.Empty;

        public DateTime? AnoPublicacao { get; set; }

        public int? IdAutor { get; set; }
    }
}
