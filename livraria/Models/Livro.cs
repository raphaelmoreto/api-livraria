namespace Models
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public DateTime? AnoPublicacao { get; set; }
        
        public int? IdAutor { get; private set; }

        public Livro(string titulo, DateTime? anoPublicacao = null, int? idAutor = null)
        {
            Titulo = titulo;

            if (anoPublicacao.HasValue)
            {
                AnoPublicacao = anoPublicacao.Value;
            }

            if (idAutor.HasValue)
            {
                IdAutor = idAutor.Value;
            }
        }
    }
}
