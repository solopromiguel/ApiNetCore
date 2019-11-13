using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.sakila
{
    public class Caracteristica
    {
        public int Id { get; set; }
        public string Cod { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreate { get; set; }

        public int UsersId { get; set; }
        public Users Users { get; set; }
        public int FactorId { get; set; }
        public Factor Factor { get; set; }

        public ICollection<Identificacion> Identificacions { get; set; }
    }
}
