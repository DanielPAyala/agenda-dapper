using Dapper.Contrib.Extensions;

namespace AgendaDapper.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public string Pais { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
