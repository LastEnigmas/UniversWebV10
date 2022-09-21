using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Body")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "PictureTitle")]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        public string LinkUrl { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "SubjectId")]
        public int SubjectId { get; set; }

        public List<Star> Stars { get; set; }

        public List<Comment> Comments { get; set; }



        #region Relation

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        #endregion

    }
}
