using CamposDealer_React.Data.DTO_s;
using CamposDealer_React.Models;

namespace CamposDealer_React.Response
{
    public class VendaResponse
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public double ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public List<ProdutoResponse> Produtos { get; set; }
    }
}
