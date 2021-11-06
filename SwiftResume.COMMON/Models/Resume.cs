using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models
{
    public class Resume : BaseEntity
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }
        [Required]
        [MinLength(2)]
        public string Nombres { get; set; }
        [Required]
        [MinLength(2)]
        public string Apellidos { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public string Lenguaje { get; set; }
        public byte[] Foto { get; set; }
    }
}
