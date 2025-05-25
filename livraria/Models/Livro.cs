namespace Models.Livro
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public DateTime AnoPublicacao { get; set; }
        
        public Autor Autor { get; set; }

        public Livro(string titulo, DateTime anoPublicacao, Autor autor)
        {
            Titulo = titulo;
            AnoPublicacao = anoPublicacao;
            Autor = autor;
        }
    }
}
