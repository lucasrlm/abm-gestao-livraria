using GestaoLivraria.Mock;
using System.Collections.Generic;

namespace GestaoLivraria.Dados.Modelos.ListarLivros
{
    public class ListarLivrosRetorno
    {
        public LivroMock Livro { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
    }
}
