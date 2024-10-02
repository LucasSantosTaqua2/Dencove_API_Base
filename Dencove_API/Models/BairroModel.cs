using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dencove_API.Models
{
    [Table("Bairro")]
    public class BairroModel
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Nome { get; set; }
        public int QtdAlertaMax { get; set; }
        public int CasosConfirmados { get; set; }
        public int GrauAtencao { get; set; }

    }
}
