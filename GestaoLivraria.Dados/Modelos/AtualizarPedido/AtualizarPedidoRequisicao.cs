using GestaoLivraria.Util.Enum;
using System.Collections.Generic;

namespace GestaoLivraria.Dados.Modelos.AtualizarPedido
{
    public class AtualizarPedidoRequisicao
    {
        public StatusPedido StatusPedido { get; set; }
        public string UsuarioNome { get; set; }
        public IEnumerable<int> LivrosId { get; set; }
    }
}
