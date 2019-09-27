using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.AtualizarPedido;
using GestaoLivraria.Dados.Modelos.CriarPedido;
using GestaoLivraria.Dados.Modelos.ListarPedidos;
using GestaoLivraria.Negocio;
using GestaoLivraria.Util.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoNegocio _pedidoNegocio = new PedidoNegocio();

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

        [HttpPost]
        public ActionResult<Pedido> Post([FromBody]CriarPedidoRequisicao requisicao)
        {
            return Ok(_pedidoNegocio.CriarPedido(requisicao));
        }

        [HttpPut("{pedidoId}")]
        public ActionResult<Pedido> Put([FromBody]AtualizarPedidoRequisicao requisicao, int pedidoId)
        {
            return Ok(_pedidoNegocio.AtualizarPedido(requisicao, pedidoId));
        }
    }
}
