using System.Collections.Generic;

namespace GestaoLivraria.Dados.Entidades
{
    public class Pedido
    {
        public PedidoCabecalho PedidoCabecalho { get; set; }
        public IEnumerable<PedidoDetalhes> PedidoDetalhes { get; set; }
    }
}
