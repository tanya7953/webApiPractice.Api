using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiPractice.Dto.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 Characters")]
        [MaxLength(280, ErrorMessage = "Too Long Title")]

        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 Characters")]
        [MaxLength(580, ErrorMessage = "Too Long Content")]

        public string Content { get; set; } = string.Empty;
    }
}
