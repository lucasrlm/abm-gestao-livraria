using GestaoLivraria.Util.Enum;
using System.Collections.Generic;

namespace GestaoLivraria.Mock
{
    public class PedidoCabecalho
    {
        public int Id { get; set; }
        public StatusPedido Status { get; set; }
        public string UsuarioNome { get; set; }

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
                Status = StatusPedido.Aberto,
                UsuarioNome = "Lucas"
            };
        }

        private PedidoCabecalho PedidoCabecalho2()
        {
            return new PedidoCabecalho()
            {
                Id = 2,
                Status = StatusPedido.Entregue,
                UsuarioNome = "Lucas"
            };
        }

        private PedidoCabecalho PedidoCabecalho3()
        {
            return new PedidoCabecalho()
            {
                Id = 3,
                Status = StatusPedido.Realizado,
                UsuarioNome = "Lucas"
            };
        }
    }
}
