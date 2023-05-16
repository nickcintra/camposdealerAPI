namespace CamposDealer_React.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cidade { get; set; }
    public List<Venda> Vendas { get; set; }
}