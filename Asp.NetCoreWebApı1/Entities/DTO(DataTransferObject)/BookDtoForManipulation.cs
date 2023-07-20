using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_DataTransferObject_
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage = "Title is a requered field")]
        [MinLength(2,ErrorMessage ="mın 2 karakterlı olabılır")]
        [MaxLength(50)]
        public string Title { get; init; }

        [Required(ErrorMessage = "Title is a requered field")] //bos geılemez
        [Range(10, 1000)] //aralık verdık 
        public decimal Price { get; init; }

    }
}
