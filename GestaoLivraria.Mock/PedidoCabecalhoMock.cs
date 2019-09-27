using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Util.Enum;
using System.Collections.Generic;

namespace GestaoLivraria.Mock
{
    public class PedidoCabecalhoMock
    {
        public IEnumerable<PedidoCabecalho> ListarPedidosCabecalho()
        {
            return new List<PedidoCabecalho>()
            {
                PedidoCabecalho1(),
                PedidoCabecalho2(),
                PedidoCabecalho3(),
            };
        }

        private PedidoCabecalho PedidoCabecalho1()
        {
            return new PedidoCabecalho()
            {
                Id = 1,
                StatusPedido = StatusPedido.Aberto,
                UsuarioNome = "Lucas"
            };
        }

        private PedidoCabecalho PedidoCabecalho2()
        {
            return new PedidoCabecalho()
            {
                Id = 2,
                StatusPedido = StatusPedido.Realizado,
                UsuarioNome = "Lucas"
            };
        }

        private PedidoCabecalho PedidoCabecalho3()
        {
            return new PedidoCabecalho()
            {
                Id = 3,
                StatusPedido = StatusPedido.Entregue,
                UsuarioNome = "Lucas"
            };
        }
    }
}
