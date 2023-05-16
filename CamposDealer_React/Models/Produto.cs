namespace CamposDealer_React.Models;

public class Produto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public List<VendaProduto> VendaProdutos { get; set; }
}