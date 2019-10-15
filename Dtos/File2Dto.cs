using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Dtos
{
    public class File2Dto
    {
        public string QuestionText { get; set; }
        public IFormFile File { get; set; }
    }
}
