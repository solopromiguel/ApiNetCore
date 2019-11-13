using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.sakila
{
    public class Control
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

        public int ControlBaseId { get; set; }   //Control Base : Agrupara los controles , con el Id del primer Control creado
        public int ControlMainId { get; set; }  //Control Main : contendra   el Id del ultimo control actualizado
        public bool IsMain { get; set; }        // IsMain : Acceso rapido al Control Actual , dentro de un grupo

        public int UsersId { get; set; }
        public Users Users { get; set; }


    }
}
