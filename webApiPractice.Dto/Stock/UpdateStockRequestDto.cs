using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiPractice.Dto.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10,ErrorMessage ="Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10,ErrorMessage ="Company name cannot be over 10 charcaters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,10000000)]
        [Column(TypeName = "decimal(18,2)")]

        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001,100)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage ="Industry name cannot be over 10 charcters")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 50000000)]
        public long MarketCap { get; set; }
    }
}
