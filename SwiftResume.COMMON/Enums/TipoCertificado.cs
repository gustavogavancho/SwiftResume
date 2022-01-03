using System.ComponentModel;

namespace SwiftResume.COMMON.Enums;

public enum TipoCertificado : short
{
    [Description("Certificado de Aprobación")]
    CertificadoDeAprobacion = 1,
    [Description("Certificado de Finalización")]
    CertificadoDeFinalizacion = 2,
    [Description("Certificado de Reconocimiento")]
    CertificadoDeReconocimiento = 3,
    [Description("Certificación Progresiva")]
    CertificacionProgresiva = 4,
    [Description("Certificado de Participación")]
    CertiiicadoDeParticipacion = 5,
}