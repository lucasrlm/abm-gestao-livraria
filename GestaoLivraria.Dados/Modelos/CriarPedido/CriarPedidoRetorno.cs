using GestaoLivraria.Dados.Entidades;
using System.Collections.Generic;

namespace GestaoLivraria.Dados.Modelos.CriarPedido
{
    public class CriarPedidoRetorno
    {
        public PedidoCabecalho PedidoCabecalho { get; set; }

        public IEnumerable<PedidoDetalhes> PedidoDetalhes { get; set; }
    }
}
