using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MinimalAPI.Models
{
    public class Grupo
    {
        public Grupo() { }

        public Grupo(int id, string descricao, string especificacao, string tipo, ICollection<Produto> produtos)
        {
            Id = id;
            Descricao = descricao;
            Especificacao = especificacao;
            Tipo = tipo;
            Produtos = produtos;
        }

        [Key, Display(Name = "Id")]
        public int Id { get; set; }

        [Required, StringLength(50), Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required, StringLength(250), Display(Name = "Especificação")]
        public string Especificacao { get; set; }

        [Required, StringLength(1), Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>(); // Coleção de navegação filha
    }
}