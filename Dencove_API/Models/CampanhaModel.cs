using System.ComponentModel.DataAnnotations.Schema;

namespace Dencove_API.Models
{
    [Table("Campanha")]
    public class CampanhaModel
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime DataPublicacao { get; set; } = DateTime.Now;

        public bool Is_Principal { get; set; } = false;

    }
}
