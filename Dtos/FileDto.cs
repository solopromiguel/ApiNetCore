using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Dtos
{
    public class FileDto
    {
        [Required]
        public string FileName { get; set; }

        public string param1 { get; set; }

        public string param2 { get; set; }
    }
}
