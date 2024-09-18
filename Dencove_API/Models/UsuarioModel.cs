using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dencove_API.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Nome { get; set; }
        [MaxLength(11)]
        public int CPF { get; set; }
        [Column(TypeName = "date")]
        public DateOnly DataNascimento { get; set; }
        [MaxLength(11)]
        public int Telefone { get; set; }
        [MaxLength(50)]
        public string Endereco { get; set; }
        [MaxLength(30)]
        [Column(TypeName = "char(1)")]
        public string Sexo { get; set; }
        public DateOnly DataCadastro { get; set; }
    }
}
