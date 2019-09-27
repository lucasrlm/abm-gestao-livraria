using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.AtualizarPedido;
using GestaoLivraria.Dados.Modelos.CriarPedido;
using GestaoLivraria.Dados.Modelos.ListarPedidos;
using GestaoLivraria.Repositorio;
using GestaoLivraria.Util.Enum;
using System.Collections.Generic;

namespace GestaoLivraria.Negocio
{
    public class PedidoNegocio
    {
        private readonly PedidoRepositorio _pedidoRepositorio = new PedidoRepositorio();

        public IEnumerable<Pedido> ListarPedidos(Requisicao<ListarPedidosRequisicao> requisicao)
        {
            return _pedidoRepositorio.ListarPedidos(requisicao);
        }

        public Pedido CriarPedido(CriarPedidoRequisicao requisicao)
        {
            var pedidoDetalhes = new List<PedidoDetalhes>();
            var pedidoDetalhesId = 1;

            foreach (var livro in requisicao.LivrosId)
            {
                pedidoDetalhes.Add(new PedidoDetalhes()
                {
                    Id = pedidoDetalhesId,
                    LivroId = livro,
                    PedidoCabecalhoId = 1
                });

                pedidoDetalhesId += 1;
            }

            return new Pedido()
            {
                PedidoCabecalho = new PedidoCabecalho()
                {
                    Id = 1,
                    StatusPedido = StatusPedido.Aberto,
                    UsuarioNome = requisicao.UsuarioNome
                },
                PedidoDetalhes = pedidoDetalhes
            };
        }

        public Pedido AtualizarPedido(AtualizarPedidoRequisicao requisicao, int pedidoId)
        {
            var pedidoDetalhes = new List<PedidoDetalhes>();
            var pedidoDetalhesId = 1;

            foreach (var livro in requisicao.LivrosId)
            {
                pedidoDetalhes.Add(new PedidoDetalhes()
                {
                    Id = pedidoDetalhesId,
                    LivroId = livro,
                    PedidoCabecalhoId = pedidoId
                });

                pedidoDetalhesId += 1;
            }

            return new Pedido()
            {
                PedidoCabecalho = new PedidoCabecalho()
                {
                    Id = pedidoId,
                    StatusPedido = StatusPedido.Aberto,
                    UsuarioNome = requisicao.UsuarioNome
                },
                PedidoDetalhes = pedidoDetalhes
            };
        }
    }
}
