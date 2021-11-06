using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models
{
    public class Resume : BaseEntity
    {
        public string Username { get; set; }
        [Required]
        [MinLength(2)]
        public string Nombres { get; set; }
        [Required]
        [MinLength(2)]
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public string Lenguaje { get; set; }
        public byte[] Foto { get; set; }
    }
}
