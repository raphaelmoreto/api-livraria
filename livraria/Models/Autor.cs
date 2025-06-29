namespace Models
{
    public class Autor
    {
        public int Id { get; private set; }

        public string Nome { get; set; } = string.Empty;

        public bool StatusAutor { get; set; }
    }
}