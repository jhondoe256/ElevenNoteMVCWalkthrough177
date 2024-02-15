using ElevenNote.Data.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.Categories
{
    public class CategoryCreate
    {

        [Required]
        [MaxLength(50)]
        public string CategoryTitle { get; set; }
    
    }
}
