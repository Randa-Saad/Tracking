using System.ComponentModel.DataAnnotations;

namespace Tracking.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "9999999999999999")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        public string RowVersion { get; set; } = string.Empty;
    }
}
