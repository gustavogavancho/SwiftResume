namespace SwiftResume.COMMON.Models
{
    public class Resume : BaseEntity
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public string Lenguaje { get; set; }
        public byte[] Foto { get; set; }
    }
}
