using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.AtualizarPedido;
using GestaoLivraria.Dados.Modelos.CriarPedido;
using GestaoLivraria.Dados.Modelos.ListarPedidos;
using GestaoLivraria.Negocio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GestaoLivraria.Controllers
{
    /// <summary>
    /// Controller de pedidos
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoNegocio _pedidoNegocio = new PedidoNegocio();

        /// <summary>
        /// Lista todos os pedidos a partir dos filtros passados
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Retorno<Pedido>> Get([FromQuery]Requisicao<ListarPedidosRequisicao> requisicao)
        {
            var retorno = new Retorno<Pedido>()
            {
                Itens = _pedidoNegocio.ListarPedidos(requisicao)
            };

            if (retorno.Itens == null || retorno.Itens.Count() == 0)
                return NoContent();

            return Ok(retorno.RetornarPaginadoOrdenado
                (requisicao.PropriedadeOrdenacao,
                 requisicao.OrdemDecrescente,
                 requisicao.Deslocamento,
                 requisicao.TamanhoPagina));
        }

        /// <summary>
        /// Cria um pedido
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Pedido> Post([FromBody]CriarPedidoRequisicao requisicao)
        {
            return Ok(_pedidoNegocio.CriarPedido(requisicao));
        }

        /// <summary>
        /// Atualiza um pedido
        /// </summary>
        /// <param name="requisicao"></param>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        [HttpPut("{pedidoId}")]
        public ActionResult<Pedido> Put([FromBody]AtualizarPedidoRequisicao requisicao, int pedidoId)
        {
            return Ok(_pedidoNegocio.AtualizarPedido(requisicao, pedidoId));
        }
    }
}
