using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication21.sakila
{
    public class EtapaIdentificacion
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreate { get; set; }
        public string Nombre { get; set; }

        public ICollection<Riesgo> Riesgos { get; set; }

        public int UsersId { get; set; }
        public Users Users { get; set; }
    }
    public class Riesgo
    {
        public int Id { get; set; }
        public string Calificacion { get; set; }
        public string Descripcion { get; set; }

        public string Impacto { get; set; }
        public string Probabilidad { get; set; }

        public string RiesgoInherente { get; set; }
        public string RiesgoResidual { get; set; }

        public int UsersId { get; set; }
        public Users Users { get; set; }

        [ForeignKey("Caracteristica")]
        public int CaracteristicaId { get; set; }
        public Caracteristica Caracteristica { get; set; }

        public ICollection<ControlRiesgo> Controles;
    }

    public class ControlRiesgo
    {

        public int Id { get; set; }
        public string Calificacion { get; set; }
        public string Cargo { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }

        public string Formalizacion { get; set; }
        public string Grado { get; set; }
        public string Oportunidad { get; set; }
        public string Periodicidad { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("Control")]
        public int ControlId { get; set; }
        public Control Control { get; set; }

        public int UsersId { get; set; }
        public Users Users { get; set; }
    }
}
