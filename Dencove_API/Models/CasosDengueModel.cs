using System.ComponentModel.DataAnnotations.Schema;

namespace Dencove_API.Models
{
    [Table("CasosDengue")]
    public class CasosDengueModel
    {
        public int Id { get; set; }
        public string Nome_Pessoa { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateOnly Data_Caso { get; set; }

        [Column("BairroId")]
        public int BairroId { get; set; }
        public BairroModel Bairro { get; set; }

    }
}
