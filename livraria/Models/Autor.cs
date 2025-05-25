namespace Models
{
    public class Autor
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public Autor(string nome)
        {
            Nome = nome;
        }
    }
}