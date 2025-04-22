using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models
{
    public class AppSettings
    {
        [Required]
        public ApiSettings API { get; set; }
        [Required]
        public Swagger Swagger { get; set; }
    }

    public class ApiSettings
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class Swagger
    {
        [Required]
        public bool Enabled { get; set; }
    }
}
