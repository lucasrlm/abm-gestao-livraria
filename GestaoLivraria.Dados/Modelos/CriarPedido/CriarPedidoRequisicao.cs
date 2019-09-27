using System.Collections.Generic;

namespace GestaoLivraria.Dados.Modelos.CriarPedido
{
    public class CriarPedidoRequisicao
    {
        public string UsuarioNome { get; set; }
        public IEnumerable<int> LivrosId { get; set; }
    }
}
