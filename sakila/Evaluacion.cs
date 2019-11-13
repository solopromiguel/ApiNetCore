using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.sakila
{
    public class Evaluacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreate { get; set; }
        public string Impacto { get; set; }
        public string Probabilidad { get; set; }
        public int Puntaje { get; set; }
        public string RiesgoInherente { get; set; }
        public string RiesgoResiual { get; set; }

        public int UsersId { get; set; }
        public Users Users { get; set; }

        //[ForeignKey("AnalisisProducto")]
        //public int AnalisisProductoId { get; set; }
        //public AnalisisProducto AnalisisProducto { get; set; }

        public int FactorId { get; set; }
        public Factor Factor { get; set; }
        public int CaracteristicaId { get; set; }
        public Caracteristica Caracteristica { get; set; }

        public ICollection<Control> Controls { get; set; }
    }
}
