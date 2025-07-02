using System.ComponentModel.DataAnnotations;

namespace Dtos.Autor
{
    public class CadastrarAutorDto
    {
        public string Nome { get; set; } = string.Empty;

        public CadastrarAutorDto(string nome)
        {
            Nome = nome;
        }
    }
}
