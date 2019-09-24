using GestaoLivraria.Dados.Entidades;
using System.Collections.Generic;

namespace GestaoLivraria.Dados.Modelos.ListarLivros
{
    public class ListarLivrosRetorno
    {
        public Livro Livro { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
    }
}
