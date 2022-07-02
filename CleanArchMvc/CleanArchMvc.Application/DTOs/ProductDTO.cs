using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This name is required.")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "This description is required.")]
        [MinLength(5)]
        [MaxLength(200)]
		public string Description { get; set; }

        [Required(ErrorMessage = "This price is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
		public decimal Price { get; set; }

        [Required(ErrorMessage = "This stock is required.")]
        [Range(1, 9999)]
        [DisplayName("Stock")]
		public int Stock { get; set; }

        [MaxLength(250)]
        [DisplayName("Stock")]
		public string Image { get; set; }
        
        [DisplayName("Categories")]
		public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category {get; set;}
    }
}