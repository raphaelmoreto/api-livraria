﻿namespace Dtos.Livro
{
    public class AtualizarLivroDto
    {
        public string Titulo { get; set; } = string.Empty;

        public DateTime? AnoPublicacao { get; set; }

        public int? IdAutor { get; set; }
    }
}
