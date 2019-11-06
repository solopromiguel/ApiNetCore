using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace WebApplication21.Dtos
{
    public class FileDto
    {
        [Required]
        public string FileName { get; set; }

        public string param1 { get; set; }

        public string param2 { get; set; }
    }

    public class File2Dto
    {
        public string QuestionText { get; set; }
        public IFormFile File { get; set; }
    }
}
