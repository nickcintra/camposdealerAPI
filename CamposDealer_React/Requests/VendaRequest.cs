using CamposDealer_React.Data.DTO_s;

namespace CamposDealer_React.Requests
{
    public class VendaRequest
    {
        public int ClienteId { get; set; }
        public List<ProdutoVendaDTO> Produtos { get; set; }
    }
}
