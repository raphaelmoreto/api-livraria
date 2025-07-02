namespace Models
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public DateTime? AnoPublicacao { get; set; }
        
        public int? IdAutor { get; set; }
    }
}
