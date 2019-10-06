using System.Collections.Generic;
using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.AtualizarPedido;
using GestaoLivraria.Dados.Modelos.CriarPedido;
using GestaoLivraria.Dados.Modelos.ListarPedidos;
using GestaoLivraria.Negocio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GestaoLivraria.Util.Enum;
using transacao_cartao_api.Entities;

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
        private static readonly HttpClient client = new HttpClient();
        private readonly string _URLPagamento = "https://localhost:5001/v1/payments";

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

        /// <summary>
        /// Realiza pagamento
        /// </summary>
        /// <param name="requisicao"></param>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        [HttpPut("{pedidoId}/pagamentos")]
        public async Task<Pedido> Put(int pedidoId)
        {
            //gerar pagamento através do pedido (normalmente haveria alguma função para calcular o valor a se pagar com base no pedido)
            Card card = new Card
            {
                Id = 1,
                Brand = "Visa",
                CardHolderName = "José das Graças",
                ExpirationMonth = 8,
                ExpirationYear = 23,
                UserId = 1,
                SecurityCode = 145
            };
            
            Pagamento pagamento = new Pagamento
            {
                Id = 1,
                Amount = 500,
                Currency = "BRL",
                UserId = 1,
                Card = card
            };

//            var data = new FormUrlEncodedContent(pagamento.ToDictionary<string>().AsEnumerable());
            
            var jsonPagamento = Newtonsoft.Json.JsonConvert.SerializeObject(pagamento);

            //enviar o pagamento para a api de transação de cartão de crédito
            var response = await client.PostAsJsonAsync(_URLPagamento, jsonPagamento);
            var responseString = await response.Content.ReadAsStringAsync();
            
            //atualizar a requisição (status do pedido)
            AtualizarPedidoRequisicao requisicao = new AtualizarPedidoRequisicao
            {
                StatusPedido = StatusPedido.Realizado,
                UsuarioNome = "José das Graças",
                LivrosId = new List<int>
                {
                    1,2,3,4,5
                }
                
            };
            
            return _pedidoNegocio.AtualizarPedido(requisicao, pedidoId);
        }
    }
}
