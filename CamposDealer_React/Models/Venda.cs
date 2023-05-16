namespace CamposDealer_React.Models;

public class Venda
{
    public int Id { get; set; }
    public DateTime DataVenda { get; set; }
    public double ValorTotal { get; set; }
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }
    public virtual List<VendaProduto> VendaProdutos { get; set; }

}