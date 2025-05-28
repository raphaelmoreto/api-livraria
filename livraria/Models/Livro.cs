namespace Models
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public DateTime? AnoPublicacao { get; set; }
        
        public int? IdAutor { get; set; }

        public Livro()
        {
        }

        public Livro(string titulo, DateTime anoPublicacao, int idAutor)
        {
            Titulo = titulo;
            AnoPublicacao = anoPublicacao;
            IdAutor = idAutor;
        }
    }
}
