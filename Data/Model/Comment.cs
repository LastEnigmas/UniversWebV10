using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [Display(Name = "Username")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter Body")]
        [Display(Name = "Body And Text")]
        public string CommentText { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public List<Star> Stars { get; set; }



        #region Relation

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        #endregion
    }
}
