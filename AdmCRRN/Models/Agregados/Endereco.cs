using System.ComponentModel.DataAnnotations;
namespace AdmCRRN.Models.Agregados
{
    [Table("Enderecos")]
    public class Endereco
    {
        public int Id { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
    }
}