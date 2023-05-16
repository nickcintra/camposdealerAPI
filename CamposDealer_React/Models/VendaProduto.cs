using System.ComponentModel.DataAnnotations;

namespace CamposDealer_React.Models;

public class VendaProduto
{
    [Key]   
    [Required]
    public int Id { get; set; }
    public int VendaId { get; set; }
    public Venda Venda { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
}