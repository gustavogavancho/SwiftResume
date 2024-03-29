﻿using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class Resume : BaseEntity
{
    public string Username { get; set; }
    [Required ,MinLength(2)] public string Nombres { get; set; }
    [Required, MinLength(2)] public string Apellidos { get; set; }
    [Required] public string Genero { get; set; }
    [Required] public string Lenguaje { get; set; }
    public string FotoString { get; set; }
    public Perfil Perfil { get; set; }
    public List<Habilidad> Habilidades { get; set; }
    public List<Educacion> Educacion { get; set; }
    public List<Experiencia> Experiencia { get; set; }
    public List<Certificacion> Certificacion { get; set;}
    public List<Proyecto> Proyectos { get; set; }
    public List<InfoAdicional> InfoAdicional { get; set; }
}