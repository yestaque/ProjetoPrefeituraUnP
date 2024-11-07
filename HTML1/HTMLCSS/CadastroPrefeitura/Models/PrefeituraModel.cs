using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CadastroPrefeitura.Models
{
    public class PrefeituraModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório.")]
        public required string CNPJ { get; set; }

        [Required(ErrorMessage = "CNES é obrigatório.")]
        public required string CNES { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório.")]
        public required string Endereco { get; set; }

        [Required(ErrorMessage = "Especialidades são obrigatórias.")]
        public required string Especialidades { get; set; }

        [Required(ErrorMessage = "Número do convênio é obrigatório.")]
        public required string NumeroConvenio { get; set; }

        [Required(ErrorMessage = "Responsável é obrigatório.")]
        public required string Responsavel { get; set; }

        [Required(ErrorMessage = "E-mail do responsável é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public required string EmailResponsavel { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        public required string Telefone { get; set; }
    }
}
