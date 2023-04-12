using System.ComponentModel.DataAnnotations;

namespace Time.Sheet.Domain.Models
{
    public class Momento
    {
        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$")]
        public string DataHora { get; set; }

    }
}
