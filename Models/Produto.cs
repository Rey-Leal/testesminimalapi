using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MinimalAPI.Models
{
    public class Produto
    {
        public Produto() { }

        public Produto(int id, int grupoId, string nome, string especificacao, string unidade, decimal preco, decimal quantidade, Grupo? grupo)
        {
            Id = id;
            GrupoId = grupoId;
            Nome = nome;
            Especificacao = especificacao;
            Unidade = unidade;
            Preco = preco;
            Quantidade = quantidade;
            Grupo = grupo;
        }

        [Key, Display(Name = "Id")]
        public int Id { get; set; }

        [ForeignKey("Grupo"), Display(Name = "Grupo")]
        public int GrupoId { get; set; }

        [Required, StringLength(50), Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required, StringLength(250), Display(Name = "Especificação")]
        public string Especificacao { get; set; }

        [Required, StringLength(20), Display(Name = "Unidade")]
        public string Unidade { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true), Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true), Display(Name = "Quantidade")]
        public decimal Quantidade { get; set; }

        [JsonIgnore]
        public virtual Grupo? Grupo { get; set; } = null!; // Referência de navegação do pai
    }
}