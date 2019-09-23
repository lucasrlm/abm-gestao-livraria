using GestaoLivraria.Dados.Entidades;
using System.Collections.Generic;

namespace GestaoLivraria.Mock
{
    public class PedidoDetalhesMock
    {
        public IEnumerable<PedidoDetalhes> ListarPedidoDetalhes()
        {
            return new List<PedidoDetalhes>()
            {
                PedidoDetalhes1(),
                PedidoDetalhes2(),
                PedidoDetalhes3()
            };
        }

        public PedidoDetalhes PedidoDetalhes1()
        {
            return new PedidoDetalhes()
            {
                Id = 1,
                LivroId = 1,
                PedidoCabecalhoId = 1
            };
        }

        public PedidoDetalhes PedidoDetalhes2()
        {
            return new PedidoDetalhes()
            {
                Id = 2,
                LivroId = 2,
                PedidoCabecalhoId = 2
            };
        }

        public PedidoDetalhes PedidoDetalhes3()
        {
            return new PedidoDetalhes()
            {
                Id = 3,
                LivroId = 3,
                PedidoCabecalhoId = 3
            };
        }
    }
}
