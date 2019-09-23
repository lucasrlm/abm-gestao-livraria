namespace GestaoLivraria.Dados.Modelos
{
    public class ListarLivrosRequisicao : Requisicao
    {
        public int? Id { get; set; }
        public string TextoAutocomplete { get; set; }
    }
}
