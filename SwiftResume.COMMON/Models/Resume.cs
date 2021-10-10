namespace SwiftResume.COMMON.Models
{
    public class Resume : BaseEntity
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Language { get; set; }
        public byte[] Foto { get; set; }
    }
}
