using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public string? Description { get; set; }

    }
}
