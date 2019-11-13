using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.sakila
{
    public class Identificacion
    {
        public int Id { get; set; }
        public string Calificacion { get; set; }
        public string Descripcion { get; set; }

        public string Impacto { get; set; }
        public string Probabilidad { get; set; }

        public int IdentificacionBaseId { get; set; }   //Control Base : Agrupara los controles , con el Id del primer Control creado
        public int IdentificacionMainId { get; set; }  //Control Main : contendra   el Id del ultimo control actualizado
        public bool IsMain { get; set; }              // IsMain : Acceso rapido al Control Actual , dentro de un grupo

        public int UsersId { get; set; }
        public Users Users { get; set; }

        public int CaracteristicaId { get; set; }
        public Caracteristica Caracteristica { get; set; }
    }
}
